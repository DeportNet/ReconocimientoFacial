using DeportNetReconocimiento.Api.Dtos.Response;
using DeportNetReconocimiento.Api.Services;
using DeportNetReconocimiento.GUI;
using DeportNetReconocimiento.Modelo;
using DeportNetReconocimiento.SDK;
using DeportNetReconocimiento.Utils;
using Serilog;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Web;
using static DeportNetReconocimiento.SDK.Hik_SDK;

namespace DeportNetReconocimiento.SDKHikvision
{

    public class Hik_Controladora_Eventos
    {
        //Defino el delegado ( A quien el voy a pasar el evento cuando lo reciba)
        private static Hik_Controladora_Eventos? instancia;

        private MSGCallBack msgCallback;

        private Hik_Controladora_Eventos()
        {
            InstanciarMsgCallback();
        }


        public static Hik_Controladora_Eventos InstanciaControladoraEventos
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new Hik_Controladora_Eventos();
                }
                return instancia;
            }
        }


        public void InstanciarMsgCallback()
        {

            this.SetupAlarm();

            msgCallback = new MSGCallBack(MsgCallback);

            if (!Hik_SDK.NET_DVR_SetDVRMessageCallBack_V50(0, msgCallback, IntPtr.Zero))
            {
                Log.Error("Error al asociar callback");
            }
            else
            {
                Log.Information("Se instancia el Callback");
            }
        }

        private void MsgCallback(int lCommand, ref Hik_SDK.NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser)
        {
            Evento infoEvento = new Evento();

            //validamos si hay espera luego de un alta
            if (ReconocimientoService.EstaEsperandoLuegoDeUnAlta)
            {
                Log.Warning("Se recibio un evento de acceso, pero se esta esperando cierto tiempo luego de un alta.");
                return;
            }



            //si esta clase no esta instanciada
            if (this == null)
            {
                Log.Information("Clase Hik_Controladora_Eventos no instanciada. La instancio de nuevo");
                Hik_Controladora_Eventos instancia = InstanciaControladoraEventos;
                return;
            }

            
            switch (lCommand)
            {
                case Hik_SDK.COMM_ALARM_ACS:
                    infoEvento = AlarmInfoToEvent(ref pAlarmer, pAlarmInfo, dwBufLen, pUser);
                    break;
                default:
                    infoEvento.Exception = "NO_COMM_ALARM_ACS_FOUND";
                    infoEvento.Success = false;
                    break;
            }

            ProcesarNuevoEvento(infoEvento);
           
        }


        private static void ProcesarNuevoEvento(Evento infoEvento)
        {
            

            if (EsTiempoValido(infoEvento))
            {

                EscribirEventos(infoEvento);

                if (EsEventoFacialValido(infoEvento))
                {
                    //validamos si el dispositivo no esta libre
                    if (!DispositivoEnUsoUtils.EstaLibre())
                    {
                        Log.Warning($"Se está realizando una peticion a Dx para obtener datos del cliente, el dispositivo no esta libre. El socio con nro: {infoEvento.Card_Number} no se va a procesar.");

                        //Log.Warning("Se recibio un evento de acceso, pero el dispositivo no esta libre. No se procesara el evento.");
                        return;
                    }


                    DispositivoEnUsoUtils.Ocupar("Procesar evento Facial"); // Ocupa el dispositivo para evitar que se procese otro evento mientras se procesa este

                    if (DobleVerificacionInternet())
                    {
                        ObtenerDatosClienteDeportnet(infoEvento.Card_Number);
                    }
                    else
                    {
                        MostrarErrorSinInternet();
                    }
                }
                else
                {
                    //Si no es facial se desocupa
                    Console.WriteLine("Desocupo de un Acceso");
                     //DispositivoEnUsoUtils.Desocupar();

                }
            }
            else
            {
                //si el error no esta vacio o null, lo loggeamos
                if (!string.IsNullOrEmpty(infoEvento.Exception))
                {
                    Log.Error($"Excepción evento Hikvision: {infoEvento.Exception}");
                }
            }
        }


        private static bool EsTiempoValido(Evento infoEvento)
        {
            DateTime tiempoActual = DateTime.Now.AddSeconds(-10);
            return infoEvento.Success && infoEvento.Time >= tiempoActual;
        }

        private static void EscribirEventos(Evento infoEvento)
        {
            if (infoEvento.Minor_Type != MINOR_LOCK_CLOSE && infoEvento.Minor_Type != MINOR_LOCK_OPEN)
            {
                Log.Information($"Evento Hikvision:  {infoEvento.Time.ToString()}  Tipo: {infoEvento.Minor_Type_Description} Tarjeta: {infoEvento.Card_Number} Puerta: {infoEvento.Device_IP_Address}");
            }
        }

        private static bool EsEventoFacialValido(Evento infoEvento)
        {
            return infoEvento.Card_Number != null && infoEvento.Minor_Type == MINOR_FACE_VERIFY_PASS;
        }


        private static bool DobleVerificacionInternet()
        {
            if (WFPrincipal.ObtenerInstancia.ConexionInternet)
                return true;

            if (VerificarConexionInternetUtils.Instancia.ComprobarConexionInternet())
                return true;

            return false;
        }

        private static void MostrarErrorSinInternet()
        {
            WFPrincipal.ObtenerInstancia.ActualizarTextoHeaderLabel("No hay conexion a internet, revise la conexion y reintente el acceso.", Color.Red);
            Log.Error("No hay conexión a internet y el dispositivo reconoció a un socio. Se mostro el mensaje 'No hay conexion a internet, revise la conexion y reintente el acceso.'");
        }


        public static async void ObtenerDatosClienteDeportnet(string numeroTarjeta)
        {

            if(string.IsNullOrWhiteSpace(numeroTarjeta))
            {
                Log.Error("El evento no tiene numero de tarjeta (es null o vacio) en ObtenerDatosClienteDeportNet.");
                return;
            }

            string[] credenciales = CredencialesUtils.LeerCredenciales();
            string idSucursal = credenciales[4];
            

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            /*Logica para conectar con deportNet y traer todos los datos del cliente que le mandamos con el numero de tarjeta*/
            string response = await WebServicesDeportnet.ControlDeAcceso(numeroTarjeta, idSucursal);


            stopwatch.Stop();
            Log.Information($"Se pidieron los datos del socio {numeroTarjeta} a dx y tardó {stopwatch.ElapsedMilliseconds}ms");

            ProcesarRespuestaAcceso(response, numeroTarjeta, idSucursal);
           
            
        }

        public static void ProcesarRespuestaAcceso(string response, string nroTarjeta, string idSucursal)
        {
            Stopwatch stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();
                if (string.IsNullOrWhiteSpace(response))
                {
                    Log.Error("Respuesta nula o vacia de deportnet. No se pudo procesar la respuesta.");
                    return;
                }

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
                    
                        jsonDeportnet.MensajeCrudo = branchAccess[0].ToString();
                        jsonDeportnet.Estado = "U";

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

                        if (jsonDeportnet.Estado == "T")
                        {
                            jsonDeportnet.MensajeAcceso = branchAccess[2].GetProperty("accessOk").ToString();
                        }
                        else if (jsonDeportnet.Estado == "F")
                        {
                            jsonDeportnet.MensajeAcceso = branchAccess[2].GetProperty("accessError").ToString();
                        }

                    }

                    WFPrincipal.ObtenerInstancia.ActualizarDatos(jsonDeportnet);
                    stopwatch.Stop();

                    Console.WriteLine($"Todo el analisis de la respuesta tardo {stopwatch.ElapsedMilliseconds}");

                }
                else
                {
                    Log.Error("No esta la propiedad branch access en ProcesarRespuestaAcceso.");
                }
            }
            catch (JsonException ex)
            {
                Log.Error($"Error al parsear JSON en ProcesarRespuestaAcceso: {ex.Message}");
                // Podés loggear a un archivo o base de datos para seguimiento
            }
            catch (Exception ex)
            {
                Log.Error($"Error inesperado en ProcesarRespuestaAcceso: {ex.Message}");
            }

            //Fin evento de acceso, por lo tanto lo desocupo
            DispositivoEnUsoUtils.Desocupar();
        }

        public void SetupAlarm()
        {
            Hik_SDK.NET_DVR_SETUPALARM_PARAM struSetupAlarmParam = new Hik_SDK.NET_DVR_SETUPALARM_PARAM();
            struSetupAlarmParam.dwSize = (uint)Marshal.SizeOf(struSetupAlarmParam);
            struSetupAlarmParam.byLevel = 1;
            struSetupAlarmParam.byAlarmInfoType = 1;
            struSetupAlarmParam.byDeployType = (byte)0;

            Hik_SDK.NET_DVR_SetupAlarmChan_V41(Hik_Controladora_General.Instancia.IdUsuario, ref struSetupAlarmParam);
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
                switch (struFileInfo.dwMajorType)
                {

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


        

    }
}
