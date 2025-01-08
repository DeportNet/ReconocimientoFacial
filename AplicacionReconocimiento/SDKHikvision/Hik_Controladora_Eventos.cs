using DeportNetReconocimiento.GUI;
using DeportNetReconocimiento.Modelo;
using DeportNetReconocimiento.SDK;
using DeportNetReconocimiento.Service;
using DeportNetReconocimiento.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static DeportNetReconocimiento.SDK.Hik_SDK;

namespace DeportNetReconocimiento.SDKHikvision
{

    public class Hik_Controladora_Eventos
    {
        //Defino el delegado ( A quien el voy a pasar el evento cuando lo reciba)
        private MSGCallBack msgCallback;

        public static  bool libre = true;
        public int GetAcsEventHandle = -1;
        private string CsTemp = null;
        private int m_lLogNum = 0;

     
        public Hik_Controladora_Eventos(){
            this.SetupAlarm();

            msgCallback = new MSGCallBack(MsgCallback);

            if (!Hik_SDK.NET_DVR_SetDVRMessageCallBack_V50(0, msgCallback, IntPtr.Zero))
            {
                Console.WriteLine("Error al asociar callback");
            }
        }

        private void MsgCallback(int lCommand, ref Hik_SDK.NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser)
        {
            Evento infoEvento = new Evento();

            //si esta clase esta instanciada
            if (this != null)
            {
                switch (lCommand)
                {
                    case Hik_SDK.COMM_ALARM_ACS:
                        infoEvento= AlarmInfoToEvent(ref pAlarmer, pAlarmInfo, dwBufLen, pUser);
                        break;
                    default:
                        infoEvento.Exception = "NO_COMM_ALARM_ACS_FOUND";
                        infoEvento.Success = false;
                        break;
                }

            }

            if (infoEvento.Success)
            {
                System.Console.WriteLine(infoEvento.Time.ToString() + " " + infoEvento.Minor_Type_Description + " Tarjeta: " + infoEvento.Card_Number + " Puerta: " + infoEvento.Door_Number);
                if(infoEvento.Card_Number != null && infoEvento.Minor_Type == MINOR_FACE_VERIFY_PASS)
                {


                    obtenerDatosClienteDeportNet(infoEvento.Card_Reader_Number, infoEvento.Card_Number);
                }
            }
            else
            {
                System.Console.WriteLine(infoEvento.Exception);
            }
        }

        public static async void obtenerDatosClienteDeportNet(int nroReader, string numeroTarjeta)
        {

            if (!libre)
            {
                Console.WriteLine("Se está procesando un evento");
                return;
            }

            if(string.IsNullOrWhiteSpace(numeroTarjeta))
            {
                Console.WriteLine("El evento no tiene numero de tajeta");
                return;
            }


            libre = false;
            /*Logica para conectar con deportNet y traer todos los datos del cliente que le mandamos con el numero de tarjeta*/
            //string jsonDeDeportnet = await DxService.ValidacionAperturaAsync(numeroTarjeta);

            string[] credenciales = WFPrincipal.ObtenerInstancia.LeerCredenciales();

            string idSucursal = credenciales[4];

            var data = new { memberId = numeroTarjeta, activeBranchId = idSucursal };
            var jsonRequest = JsonSerializer.Serialize(data);


            await DxService.manejarReconocimientoSociosAsync(jsonRequest);

            libre = true;

        }

        public async  static  Task<string> hacerPeticion()
        {
            await Task.Delay(1000); // Demora de 1 segundos (1000 ms)

            string respuesta  =  "{ \"Id\": \"1\", \"Nombre\": \"Juan\", \"Actividad\": \"Gimnasio\", \"Apellido\": \"Doe\", \"ClasesRestantes\": \"5\", \"Mensaje\": \"Habrá descuentos especiales la semana que viene\", \"Vencimiento\": \"12/09/2024\", \"Rta\" : \"S\", \"Fecha\" : \"12/08/2024\", \"Hora\" : \"19:00:32\", \"Pregunta\" : \"El cliente no paga la cuota hace 1 mes\"}";
            
            return respuesta;
        }


        public void SetupAlarm()
        {
            Hik_SDK.NET_DVR_SETUPALARM_PARAM struSetupAlarmParam = new Hik_SDK.NET_DVR_SETUPALARM_PARAM();
            struSetupAlarmParam.dwSize = (uint)Marshal.SizeOf(struSetupAlarmParam);
            struSetupAlarmParam.byLevel = 1;
            struSetupAlarmParam.byAlarmInfoType = 1;
            struSetupAlarmParam.byDeployType = (byte)0;

            Hik_SDK.NET_DVR_SetupAlarmChan_V41(Hik_Controladora_General.InstanciaControladoraGeneral.IdUsuario, ref struSetupAlarmParam);
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


                switch (struFileInfo.dwMajorType) {

                    case Hik_SDK.MAJOR_ALARM:
                        Hik_Evento_Mapper.AlarmMinorTypeMap(struFileInfo, csTmp);
                        EventInfo.Major_Type_Description = "MAJOR_ALARM";
                        break;
                    case Hik_SDK.MAJOR_OPERATION:
                        Hik_Evento_Mapper.OperationMinorTypeMap(struFileInfo, csTmp);
                        EventInfo.Major_Type_Description = "MAJOR_OPERATION";
                        break;
                    case Hik_SDK.MAJOR_EXCEPTION:
                        Hik_Evento_Mapper.ExceptionMinorTypeMap(struFileInfo, csTmp);
                        EventInfo.Major_Type_Description = "MAJOR_EXCEPTION";
                        break;
                    case Hik_SDK.MAJOR_EVENT:
                        Hik_Evento_Mapper.EventMinorTypeMap(struFileInfo, csTmp);
                        EventInfo.Major_Type_Description = "MAJOR_EVENT";
                        break;
                    default:
                        EventInfo.Major_Type_Description = "ERROR";
                        break;
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
                    EventInfo.Card_Number =System.Text.Encoding.UTF8.GetString(struAcsAlarmInfo.struAcsEventInfo.byCardNo).TrimEnd('\0');
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


        

    }
}
