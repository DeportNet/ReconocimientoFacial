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


        public int GetAcsEventHandle = -2;
        private string CsTemp = null;
        private int m_lLogNum = 0;
        Thread m_pDisplayListThread = null;

        public  Hik_Resultado CapturarEvento()
        {
            Hik_Resultado resultado = new Hik_Resultado();
            Console.WriteLine("Entro 1");
            m_lLogNum = 0;


            Hik_SDK.NET_DVR_ACS_EVENT_COND struCond = new Hik_SDK.NET_DVR_ACS_EVENT_COND();
            IntPtr ptrCond = IntPtr.Zero;
            uint dwSize = 0;
            InicializarEventCond(ref struCond,ref ptrCond,ref dwSize);

            


            GetAcsEventHandle = Hik_SDK.NET_DVR_StartRemoteConfig(Hik_Controladora_General.IdUsuario, Hik_SDK.NET_DVR_GET_ACS_EVENT, ptrCond, (int)dwSize, null, IntPtr.Zero);

            if (-1 == GetAcsEventHandle)
            {
                resultado.Exito =  false;
                resultado.Codigo =Hik_SDK.NET_DVR_GetLastError().ToString();
                resultado.Mensaje= "Error al obtener el evento";
               
            } else
            {

                VerificarEstadoEvento();

               // m_pDisplayListThread = new Thread(VerificarEstadoEvento);
               // m_pDisplayListThread.Start();
            }

            Marshal.FreeHGlobal(ptrCond);
            return resultado;
        }

        public void InicializarEventCond(ref Hik_SDK.NET_DVR_ACS_EVENT_COND struCond, ref IntPtr ptrCond, ref uint dwSize)
        {
            struCond.Init();
            struCond.dwSize = (uint)Marshal.SizeOf(struCond);


            struCond.dwMajor = 0;
            struCond.dwMinor = 0;


            //Si no le pongo fecha me trae todos los eventos 
            /*
            struCond.struStartTime.dwYear = 2024;
            struCond.struStartTime.dwMonth = 11;
            struCond.struStartTime.dwDay = 14;
            struCond.struStartTime.dwHour = 1;
            struCond.struStartTime.dwMinute = 1;
            struCond.struStartTime.dwSecond = 1;

            struCond.struEndTime.dwYear = 2024;
            struCond.struEndTime.dwMonth = 11;
            struCond.struEndTime.dwDay = 14;
            struCond.struEndTime.dwHour = 20;
            struCond.struEndTime.dwMinute = 1;
            struCond.struEndTime.dwSecond = 1;

            struCond.byPicEnable = 0;
            struCond.szMonitorID = "";
            struCond.wInductiveEventType = 65535;
            */

            dwSize = struCond.dwSize;

            ptrCond= Marshal.AllocHGlobal((int)dwSize);
            Marshal.StructureToPtr(struCond, ptrCond, false);
        }

        public void InicializarEventCfg(ref Hik_SDK.NET_DVR_ACS_EVENT_CFG struCfg,ref int dwOutBuffSize)
        {
            struCfg.dwSize = (uint)Marshal.SizeOf(struCfg);
            dwOutBuffSize = (int)struCfg.dwSize;
            struCfg.Init();
        }

        public void VerificarEstadoEvento() 
        {
            Console.WriteLine("Entro 6 Estoy dentro de verificar estado");

            Hik_Resultado resultado = new Hik_Resultado();  
            int dwStatus = 0;
            bool flag = true;

            Hik_SDK.NET_DVR_ACS_EVENT_CFG struCfg = new Hik_SDK.NET_DVR_ACS_EVENT_CFG();

            struCfg.dwSize = (uint)Marshal.SizeOf(struCfg);
            int dwOutBuffSize = 0;
            InicializarEventCfg(ref struCfg, ref dwOutBuffSize);

            Console.WriteLine("Entro 7 - Cargó todo (WTF)");

            while (flag)
            {
                dwStatus = Hik_SDK.NET_DVR_GetNextRemoteConfig_AcsEventCgf(GetAcsEventHandle, ref struCfg, dwOutBuffSize);
                switch (dwStatus)
                {
                    case Hik_SDK.NET_SDK_GET_NEXT_STATUS_SUCCESS:
                        ProcesarAcsEvent(ref struCfg, ref flag);

                        break;
                    case Hik_SDK.NET_SDK_GET_NEXT_STATUS_NEED_WAIT:
                        Thread.Sleep(200);
                        break;
                    case Hik_SDK.NET_SDK_GET_NEXT_STATUS_FAILED:
                        Hik_SDK.NET_DVR_StopRemoteConfig(GetAcsEventHandle);
                        Console.WriteLine("NET_SDK_GET_NEXT_STATUS_FAILED" + Hik_SDK.NET_DVR_GetLastError().ToString());

                        flag = false;
                        break;
                    case Hik_SDK.NET_SDK_GET_NEXT_STATUS_FINISH:
                        Hik_SDK.NET_DVR_StopRemoteConfig(GetAcsEventHandle);
                        Console.WriteLine("Termino el proceso de estado");
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("NET_SDK_GET_NEXT_STATUS_UNKNOWN" + Hik_SDK.NET_DVR_GetLastError().ToString());
                        flag = false;
                        Hik_SDK.NET_DVR_StopRemoteConfig(GetAcsEventHandle);
                        break;
                }
            }

        }


        /*------Eventos----*/


        public void ConfigurarAlarm()
        {
            Hik_SDK.NET_DVR_SETUPALARM_PARAM struSetupAlarmParam = new Hik_SDK.NET_DVR_SETUPALARM_PARAM();
            struSetupAlarmParam.dwSize = (uint)Marshal.SizeOf(struSetupAlarmParam);
            struSetupAlarmParam.byLevel = 1;
            struSetupAlarmParam.byAlarmInfoType = 1;
            struSetupAlarmParam.byDeployType = (byte)0;

            Hik_SDK.NET_DVR_SetupAlarmChan_V41(Hik_Controladora_General.IdUsuario, ref struSetupAlarmParam);
        }


        public Evento ProcesarAlarm(int lCommand, ref Hik_SDK.NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser)
            
        {

            switch (lCommand)
            {
                case Hik_SDK.COMM_ALARM_ACS:
                    return AlarmInfoAEvento(ref pAlarmer, pAlarmInfo, dwBufLen, pUser);

                default:
                    Evento EventoInfo = new Evento();
                    EventoInfo.Exception = "NO_COMM_ALARM_ACS_FOUND";
                    EventoInfo.Success = false;
                    return EventoInfo;
            }
        }

        private Evento AlarmInfoAEvento(ref Hik_SDK.NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser)
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





        private Hik_Resultado ProcesarAcsEvent(ref Hik_SDK.NET_DVR_ACS_EVENT_CFG struCFG, ref bool flag)
        {

            Hik_Resultado resultado = new Hik_Resultado();
            try
            {
                Console.WriteLine("El dia del evento es: " + struCFG.struTime.dwDay);
                resultado.Exito = true;
                resultado.Mensaje = ("Se obtuvo el evento con la siguiente estructura " + struCFG.ToString());
            }
            catch
            {
                resultado.Exito= false;
                resultado.Mensaje = "Error al procesar la cara";
                resultado.Codigo = Hik_SDK.NET_DVR_GetLastError().ToString();
                flag = false;
            }

            return resultado;
        }


        public static void MessageCallback(int lCommand, ref NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser)
        {
            Console.WriteLine("Hubo un evento");
            Console.WriteLine($"Evento recibido: Comando={lCommand}, Tamaño={dwBufLen}");

            // Procesar los datos del evento según el comando
            if (lCommand == Hik_SDK.NET_DVR_GET_ACS_EVENT)
            {
                // Manejar eventos de control de acceso
                Console.WriteLine("Evento de control de acceso recibido.");
            }
            else
            {
                Console.WriteLine("Otro evento recibido.");
            }
        }

    }
}
