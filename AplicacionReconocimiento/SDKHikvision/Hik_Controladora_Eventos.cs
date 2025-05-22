using DeportNetReconocimiento.Api.Dtos.Response;
using DeportNetReconocimiento.Api.Services;
using DeportNetReconocimiento.BD;
using DeportNetReconocimiento.GUI;
using DeportNetReconocimiento.Modelo;
using DeportNetReconocimiento.SDK;
using DeportNetReconocimiento.Utils;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Web;
using static DeportNetReconocimiento.SDK.Hik_SDK;

namespace DeportNetReconocimiento.SDKHikvision
{

    public class Hik_Controladora_Eventos
    {
        //Defino el delegado ( A quien el voy a pasar el evento cuando lo reciba)
        private static Hik_Controladora_Eventos? instanciaControladoraEventos;

        private MSGCallBack msgCallback;

        public static bool libre = true;

        //public int GetAcsEventHandle = -1;
        //private string CsTemp = null;
        //private int m_lLogNum = 0;
        
     
        private Hik_Controladora_Eventos(){
            this.SetupAlarm();
            
            msgCallback = new MSGCallBack(MsgCallback);

            if (!Hik_SDK.NET_DVR_SetDVRMessageCallBack_V50(0, msgCallback, IntPtr.Zero))
            {
                Console.WriteLine("Error al asociar callback");
            }
        }


        public static Hik_Controladora_Eventos InstanciaControladoraEventos
        {
            get
            {
                if (instanciaControladoraEventos == null)
                {
                    instanciaControladoraEventos = new Hik_Controladora_Eventos();
                }
                return instanciaControladoraEventos;
            }
        }


        public void ReinstanciarMsgCallback() {
            Console.WriteLine("Reinstanciamos msgCallback");


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

            //validamos si hay espera luego de un alta
            if (ReconocimientoService.EstaEsperandoLuegoDeUnAlta)
            {
                Console.WriteLine("Esperando cierto tiempo luego de un alta");
                return;
            }

            
            //si esta clase esta instanciada
            if(this == null)
            {
                Console.WriteLine("Clase Hik_Controladora_Eventos no instanciada. La instancio de nuevo");
                Hik_Controladora_Eventos instancia = InstanciaControladoraEventos;
                return;
            }

            
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

            DateTime? tiempoDisp = Hik_Controladora_General.InstanciaControladoraGeneral.ObtenerTiempoDispositivo();

            if (!tiempoDisp.HasValue)
            {
                Console.WriteLine("Tiempo del disp null");
            }
            
            DateTime tiempoActual = DateTime.Now.AddSeconds(-10);
            
            Console.WriteLine("Tiempo del dispositivo: "+ tiempoDisp + "Tiempo Actual: " + tiempoActual + ". Tiempo del evento: "+ infoEvento.Time);

            //si el evento es exitoso y el tiempo del evento es mayorIgual a la hora actual

            if (infoEvento.Success && infoEvento.Time >= tiempoActual)
            {
                Console.WriteLine("Evento Hikvision: \n"+
                infoEvento.Time.ToString() + " " + infoEvento.Minor_Type_Description +
                " Tarjeta: " + infoEvento.Card_Number +
                " Puerta: " + infoEvento.Door_Number);

                if (infoEvento.Card_Number != null && infoEvento.Minor_Type == MINOR_FACE_VERIFY_PASS)
                {
                    //Si no tenemos conexion a internet, hay que guardar el evento en la base de datos
                    if (!WFPrincipal.ObtenerInstancia.ConexionInternet)
                    {
                        Console.WriteLine("Guardo al cliente en bd y no dx");
                        int.TryParse(infoEvento.Card_Number, out int nroTarjeta);

                        BdClientes.InsertarCliente(nroTarjeta, "Cliente", infoEvento.Time);
                    }
                    else
                    {
                        ObtenerDatosClienteDeportNet(infoEvento.Card_Number);
                    }
                }
            }
            else
            {

                if (!string.IsNullOrEmpty(infoEvento.Exception))
                {
                    Console.WriteLine("Excepcion evento hikvision: "+infoEvento.Exception);
                }

            }

        }

        //todo verificar si es necesario el nroReader realmente
        public static async void ObtenerDatosClienteDeportNet(string numeroTarjeta)
        {

           

            if (!libre)
            {
                Console.WriteLine("Se está procesando un evento, dispositivo no esta libre.");
                return;
            }

            if(string.IsNullOrWhiteSpace(numeroTarjeta))
            {
                Console.WriteLine("El evento no tiene numero de tarjeta.");
                return;
            }

            
            libre = false;
            

            string[] credenciales = CredencialesUtils.LeerCredenciales();
            string idSucursal = credenciales[4];
            

            /*Logica para conectar con deportNet y traer todos los datos del cliente que le mandamos con el numero de tarjeta*/
            string response = await WebServicesDeportnet.ControlDeAcceso(numeroTarjeta,idSucursal);
            
            ProcesarRespuestaAcceso(response, numeroTarjeta, idSucursal);
            


            libre = true;

        }

        public static void ProcesarRespuestaAcceso(string response, string nroTarjeta, string idSucursal)
        {

            using JsonDocument doc = JsonDocument.Parse(response);
            JsonElement root = doc.RootElement;

            //Busco la propiedad branchAcces y digo que el elemento  es de tipo arreglo
            if (root.TryGetProperty("branchAccess", out JsonElement branchAccess) && branchAccess.ValueKind == JsonValueKind.Array)
            {
                ValidarAccesoResponse jsonDeportnet = new ValidarAccesoResponse();

                jsonDeportnet.IdCliente = nroTarjeta;
                jsonDeportnet.IdSucursal = idSucursal;

                if (branchAccess[1].ToString() == "U")
                {
                    
                    //MessageBox.Show(branchAccess[0].ToString(),
                    //"Error",
                    //MessageBoxButtons.OK,
                    //MessageBoxIcon.Error
                    //);
                    //verificar si existe en el dispositivo eliminarlo

                    jsonDeportnet.MensajeCrudo = branchAccess[0].ToString();
                    jsonDeportnet.Estado = "U";

                    //return;
                }


                //verificamos el estado del acceso, si es pregunta
                if (branchAccess[1].ToString() == "Q")
                {
                    jsonDeportnet.MensajeCrudo = branchAccess[0].ToString();
                    jsonDeportnet.Estado = "Q";

                }

                //Verificamos el jsonObject en la pos 2 que serian los datos del cliente
                if (branchAccess[2].ValueKind != JsonValueKind.Null)
                {

                    //jsonDeportnet.Id = branchAccess[2].GetProperty("id").ToString();
                    jsonDeportnet.Nombre = branchAccess[2].GetProperty("firstName").ToString();
                    jsonDeportnet.Apellido = branchAccess[2].GetProperty("lastName").ToString();
                    jsonDeportnet.NombreCompleto = branchAccess[2].GetProperty("name").ToString();
                    jsonDeportnet.MensajeCrudo = branchAccess[2].GetProperty("accessStatus").ToString();
                    jsonDeportnet.Mostrarcumpleanios = branchAccess[2].GetProperty("showBirthday").ToString();
                    
                    jsonDeportnet.Estado = branchAccess[2].GetProperty("status").ToString(); 

                    if(jsonDeportnet.Estado == "T")
                    {
                        jsonDeportnet.MensajeAcceso = branchAccess[2].GetProperty("accessOk").ToString();
                    }
                    else if (jsonDeportnet.Estado == "F")
                    {
                        jsonDeportnet.MensajeAcceso = branchAccess[2].GetProperty("accessError").ToString();
                    }

                    
                }

                WFPrincipal.ObtenerInstancia.ActualizarDatos(jsonDeportnet);
            }
            else
            {
                Console.WriteLine("No está la propiedad branch access.");
            }
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
                //obtenemos el evento
                Hik_SDK.NET_DVR_ACS_ALARM_INFO struAcsAlarmInfo = new Hik_SDK.NET_DVR_ACS_ALARM_INFO();
                struAcsAlarmInfo = (Hik_SDK.NET_DVR_ACS_ALARM_INFO)Marshal.PtrToStructure(pAlarmInfo, typeof(Hik_SDK.NET_DVR_ACS_ALARM_INFO));
                Hik_SDK.NET_DVR_LOG_V30 struFileInfo = new Hik_SDK.NET_DVR_LOG_V30();

                //obtenemos la descripcion del evento, Major y Minor
                struFileInfo.dwMajorType = struAcsAlarmInfo.dwMajor;
                struFileInfo.dwMinorType = struAcsAlarmInfo.dwMinor;
                char[] csTmp = new char[256];

                EventInfo.Major_Type = (int)struFileInfo.dwMajorType;
                EventInfo.Minor_Type = (int)struFileInfo.dwMinorType;

                //Obtenemos la descripcion del evento Major
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
