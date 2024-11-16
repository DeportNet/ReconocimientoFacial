using DeportNetReconocimiento.Modelo;
using DeportNetReconocimiento.SDK;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static DeportNetReconocimiento.SDK.Hik_SDK;

namespace DeportNetReconocimiento.SDKHikvision
{

    internal class Hik_Controladora_Eventos
    {

        public static int idUsuario = -1;
        public int GetAcsEventHandle = -1;
        private string CsTemp = null;
        private int m_lLogNum = 0;


        public Hik_Resultado Login(string user, string password, string port, string ip)
        {
            Hik_Resultado loginResultado = new Hik_Resultado();


            //cerramos la sesion que estaba iniciada anteriormente
            if (idUsuario >= 0)
            {
                Hik_SDK.NET_DVR_Logout_V30(idUsuario);
                idUsuario = -1;
            }

            //creamos y cargamos las estructuras de informacion de login y de informacion del dispositivo

            Hik_SDK.NET_DVR_USER_LOGIN_INFO struLoginInfo = new Hik_SDK.NET_DVR_USER_LOGIN_INFO();
            Hik_SDK.NET_DVR_DEVICEINFO_V40 struDeviceInfoV40 = new Hik_SDK.NET_DVR_DEVICEINFO_V40();
            struDeviceInfoV40.struDeviceV30.sSerialNumber = new byte[Hik_SDK.SERIALNO_LEN];

            struLoginInfo.sDeviceAddress = ip;
            struLoginInfo.sUserName = user;
            struLoginInfo.sPassword = password;
            ushort.TryParse(port, out struLoginInfo.wPort);


            //utilizamos metodo de iniciar sesion
            int auxUserID = -1;
            auxUserID = Hik_SDK.NET_DVR_Login_V40(ref struLoginInfo, ref struDeviceInfoV40);

            if (auxUserID >= 0)
            {
                //si da mayor a 0 signfica exito
                idUsuario = auxUserID;
                loginResultado.Mensaje = "Se inicio sesión con exito";
                loginResultado.Exito = true;
            }
            else
            {
                //sino debemos verificar el tipo de error
                uint nroError = Hik_SDK.NET_DVR_GetLastError();
                string mensajeDeSdk = "";


                if (nroError == Hik_SDK.NET_DVR_PASSWORD_ERROR)
                {
                    loginResultado.Exito = false;
                    loginResultado.Mensaje = "Usuario o contraseña invalidos";
                    if (1 == struDeviceInfoV40.bySupportLock)
                    {
                        mensajeDeSdk = string.Format("Te quedan {0} intentos para logearte", struDeviceInfoV40.byRetryLoginTime);
                    }
                }
                else if (nroError == Hik_SDK.NET_DVR_USER_LOCKED)
                {
                    if (1 == struDeviceInfoV40.bySupportLock)
                    {
                        mensajeDeSdk = string.Format("Usuario bloqueado, el tiempo restante de bloqueo es de {0}", struDeviceInfoV40.dwSurplusLockTime);
                        loginResultado.Exito = false;
                        loginResultado.Mensaje = mensajeDeSdk;
                    }
                }
                else
                {
                    loginResultado.Exito = false;
                    loginResultado.Mensaje = "Error de red o el panel esta ocupado";
                }
            }

            Hik_Resultado.EscribirLog();

            return loginResultado;
        }

        public Hik_Resultado Initialize()
        {
            Hik_Resultado resultHC = new Hik_Resultado();

            if (Hik_SDK.NET_DVR_Init() == false)
            {
                System.Console.WriteLine("NET_DVR_Init error");
                resultHC.Mensaje= "NET_DVR_Init error";
                resultHC.Exito = false;

                return resultHC;
            }

            Hik_SDK.NET_DVR_SetLogToFile(3, "", false);

            resultHC.Mensaje= "NET_DVR_Init éxito";
            resultHC.Exito = true;

            return resultHC;

        }



        public void SetupAlarm()
        {
            Hik_SDK.NET_DVR_SETUPALARM_PARAM struSetupAlarmParam = new Hik_SDK.NET_DVR_SETUPALARM_PARAM();
            struSetupAlarmParam.dwSize = (uint)Marshal.SizeOf(struSetupAlarmParam);
            struSetupAlarmParam.byLevel = 1;
            struSetupAlarmParam.byAlarmInfoType = 1;
            struSetupAlarmParam.byDeployType = (byte)0;

            Hik_SDK.NET_DVR_SetupAlarmChan_V41(idUsuario, ref struSetupAlarmParam);
        }

        public Evento ProcessAlarm(int lCommand, ref Hik_SDK.NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser)
        {

            switch (lCommand)
            {
                case Hik_SDK.COMM_ALARM_ACS:
                    return AlarmInfoToEvent(ref pAlarmer, pAlarmInfo, dwBufLen, pUser);

                default:
                    Evento EventInfo = new Evento();
                    EventInfo.Exception = "NO_COMM_ALARM_ACS_FOUND";
                    EventInfo.Success = false;
                    return EventInfo;
            }
        }

        private Evento AlarmInfoToEvent(ref Hik_SDK.NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser)
        {
            Evento EventInfo = new Evento();

            try
            {

                Hik_SDK.NET_DVR_ACS_ALARM_INFO struAcsAlarmInfo = new Hik_SDK.NET_DVR_ACS_ALARM_INFO();
                struAcsAlarmInfo = (Hik_SDK.NET_DVR_ACS_ALARM_INFO)Marshal.PtrToStructure(pAlarmInfo, typeof(Hik_SDK.NET_DVR_ACS_ALARM_INFO));
                Hik_SDK.NET_DVR_LOG_V30 struFileInfo = new Hik_SDK.NET_DVR_LOG_V30();
                struFileInfo.dwMajorType = struAcsAlarmInfo.dwMajor;
                struFileInfo.dwMinorType = struAcsAlarmInfo.dwMinor;
                char[] csTmp = new char[256];

                EventInfo.Major_Type = (int)struFileInfo.dwMajorType;
                EventInfo.Minor_Type = (int)struFileInfo.dwMinorType;

                if (Hik_SDK.MAJOR_ALARM == struFileInfo.dwMajorType)
                {
                    Hik_Evento_Mapper.AlarmMinorTypeMap(struFileInfo, csTmp);
                    EventInfo.Major_Type_Description = "MAJOR_ALARM";
                }
                else if (Hik_SDK.MAJOR_OPERATION == struFileInfo.dwMajorType)
                {
                    Hik_Evento_Mapper.OperationMinorTypeMap(struFileInfo, csTmp);
                    EventInfo.Major_Type_Description = "MAJOR_OPERATION";
                }
                else if (Hik_SDK.MAJOR_EXCEPTION == struFileInfo.dwMajorType)
                {
                    Hik_Evento_Mapper.ExceptionMinorTypeMap(struFileInfo, csTmp);
                    EventInfo.Major_Type_Description = "MAJOR_EXCEPTION";
                }
                else if (Hik_SDK.MAJOR_EVENT == struFileInfo.dwMajorType)
                {
                    Hik_Evento_Mapper.EventMinorTypeMap(struFileInfo, csTmp);
                    EventInfo.Major_Type_Description = "MAJOR_EVENT";
                }

                String szInfo = new String(csTmp).TrimEnd('\0');
                String szInfoBuf = null;
                szInfoBuf = szInfo;

                EventInfo.Minor_Type_Description = szInfo;

                String name = System.Text.Encoding.UTF8.GetString(struAcsAlarmInfo.sNetUser).TrimEnd('\0');
                for (int i = 0; i < struAcsAlarmInfo.sNetUser.Length; i++)
                {
                    if (struAcsAlarmInfo.sNetUser[i] == 0)
                    {
                        name = name.Substring(0, i);
                        break;
                    }
                }

                EventInfo.User = name;

                EventInfo.Remote_IP_Address = struAcsAlarmInfo.struRemoteHostAddr.sIpV4;
                EventInfo.Time = new DateTime(struAcsAlarmInfo.struTime.dwYear, struAcsAlarmInfo.struTime.dwMonth, struAcsAlarmInfo.struTime.dwDay, struAcsAlarmInfo.struTime.dwHour, struAcsAlarmInfo.struTime.dwMinute, struAcsAlarmInfo.struTime.dwSecond);


                if (struAcsAlarmInfo.struAcsEventInfo.byCardNo[0] != 0)
                {
                    EventInfo.Card_Number = System.Text.Encoding.UTF8.GetString(struAcsAlarmInfo.struAcsEventInfo.byCardNo).TrimEnd('\0');
                }
                String[] szCardType = { "normal card", "disabled card", "blocklist card", "night watch card", "stress card", "super card", "guest card" };
                byte byCardType = struAcsAlarmInfo.struAcsEventInfo.byCardType;

                if (byCardType != 0 && byCardType <= szCardType.Length)
                {
                    EventInfo.Card_Type = szCardType[byCardType - 1];
                }

                if (struAcsAlarmInfo.struAcsEventInfo.dwCardReaderNo != 0)
                {
                    EventInfo.Card_Reader_Number = (int)struAcsAlarmInfo.struAcsEventInfo.dwCardReaderNo;
                }
                if (struAcsAlarmInfo.struAcsEventInfo.dwDoorNo != 0)
                {
                    EventInfo.Door_Number = (int)struAcsAlarmInfo.struAcsEventInfo.dwDoorNo;
                }
                if (struAcsAlarmInfo.struAcsEventInfo.dwVerifyNo != 0)
                {
                    EventInfo.Multiple_Card_Authentication_Serial_Number = (int)struAcsAlarmInfo.struAcsEventInfo.dwVerifyNo;
                }
                if (struAcsAlarmInfo.struAcsEventInfo.dwAlarmInNo != 0)
                {
                    EventInfo.Alarm_Input_Number = (int)struAcsAlarmInfo.struAcsEventInfo.dwAlarmInNo;
                }
                if (struAcsAlarmInfo.struAcsEventInfo.dwAlarmOutNo != 0)
                {
                    EventInfo.Alarm_Output_Number = (int)struAcsAlarmInfo.struAcsEventInfo.dwAlarmOutNo;
                }
                if (struAcsAlarmInfo.struAcsEventInfo.dwCaseSensorNo != 0)
                {
                    EventInfo.Event_Trigger_Number = (int)struAcsAlarmInfo.struAcsEventInfo.dwCaseSensorNo;
                }
                if (struAcsAlarmInfo.struAcsEventInfo.dwRs485No != 0)
                {
                    EventInfo.RS485_Channel_Number = (int)struAcsAlarmInfo.struAcsEventInfo.dwRs485No;
                }
                if (struAcsAlarmInfo.struAcsEventInfo.dwMultiCardGroupNo != 0)
                {
                    EventInfo.Multi_Recombinant_Authentication_ID = (int)struAcsAlarmInfo.struAcsEventInfo.dwMultiCardGroupNo;
                }
                if (struAcsAlarmInfo.struAcsEventInfo.byCardReaderKind != 0)
                {
                    EventInfo.Card_Reader_Kind = struAcsAlarmInfo.struAcsEventInfo.byCardReaderKind.ToString();
                }
                if (struAcsAlarmInfo.struAcsEventInfo.wAccessChannel >= 0)
                {
                    EventInfo.Access_Channel = (int)struAcsAlarmInfo.struAcsEventInfo.wAccessChannel;
                }
                if (struAcsAlarmInfo.struAcsEventInfo.dwEmployeeNo != 0)
                {
                    EventInfo.Employee_Number = (int)struAcsAlarmInfo.struAcsEventInfo.dwEmployeeNo;
                }
                if (struAcsAlarmInfo.struAcsEventInfo.byDeviceNo != 0)
                {
                    EventInfo.Device_Number = struAcsAlarmInfo.struAcsEventInfo.byDeviceNo.ToString();
                }
                if (struAcsAlarmInfo.struAcsEventInfo.wLocalControllerID >= 0)
                {
                    EventInfo.Local_Controller_ID = (int)struAcsAlarmInfo.struAcsEventInfo.wLocalControllerID;
                }
                if (struAcsAlarmInfo.struAcsEventInfo.byInternetAccess >= 0)
                {
                    EventInfo.Internet_Access = struAcsAlarmInfo.struAcsEventInfo.byInternetAccess.ToString();
                }
                if (struAcsAlarmInfo.struAcsEventInfo.byType >= 0)
                {
                    EventInfo.Type = struAcsAlarmInfo.struAcsEventInfo.byType.ToString();
                }
                if (struAcsAlarmInfo.struAcsEventInfo.bySwipeCardType != 0)
                {
                    EventInfo.Swipe_Card_Type = struAcsAlarmInfo.struAcsEventInfo.bySwipeCardType.ToString();
                }

                EventInfo.Mac_Address = System.Text.Encoding.UTF8.GetString(struAcsAlarmInfo.struAcsEventInfo.byMACAddr).TrimEnd('\0');

                EventInfo.Device_IP_Address = pAlarmer.sDeviceIP.ToString();

                EventInfo.Success = true;

            }
            catch (Exception e)
            {
                EventInfo.Exception = e.ToString();
                EventInfo.Success = false;
            }



            return EventInfo;

        }


        public bool ObtenerStatusPanel()
        {
            IntPtr pInBuf;
            Int32 nSize;
            int iLastErr = 17;

            pInBuf = IntPtr.Zero;
            nSize = 0;

            int XML_ABILITY_OUT_LEN = 3 * 1024 * 1024;
            IntPtr pOutBuf = Marshal.AllocHGlobal(XML_ABILITY_OUT_LEN);

            if (!Hik_SDK.NET_DVR_GetDeviceAbility(idUsuario, 0, pInBuf, (uint)nSize, pOutBuf, (uint)XML_ABILITY_OUT_LEN))
            {
                iLastErr = (int)Hik_SDK.NET_DVR_GetLastError();

                //si perdio conexión
                if (iLastErr == 17)
                {
                    return false;
                }
            }

            Marshal.FreeHGlobal(pInBuf);
            Marshal.FreeHGlobal(pOutBuf);

            if (iLastErr == 1000)
                return true;
            else
                return false;
        }

    }
}
