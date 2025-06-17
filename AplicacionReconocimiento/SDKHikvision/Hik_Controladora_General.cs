
using DeportNetReconocimiento.GUI;
using DeportNetReconocimiento.SDKHikvision;
using DeportNetReconocimiento.Utils;
using Serilog;
using System.IO;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace DeportNetReconocimiento.SDK
{
    public class Hik_Controladora_General
    {
        //atributos

        //patron singleton, instancia de la propia clase
        private static Hik_Controladora_General? instanciaControladoraGeneral;


        private int idUsuario; // solo puede haber solo un user_ID
        private bool soportaFacial;
        private bool soportaHuella;
        private bool soportaTarjeta;
        private static Hik_Controladora_Facial? hik_Controladora_Facial;
        private static Hik_Controladora_Tarjetas? hik_Controladora_Tarjetas;
        private static Hik_Controladora_Eventos? hik_Controladora_Eventos;
        public Hik_SDK.NET_DVR_TIME m_struTimeCfg;
        public Hik_SDK.NET_DVR_DEVICEINFO_V30 m_struDeviceInfo;
        private int numeroLector = 1;
        //constructores
        private Hik_Controladora_General()
        {
            this.idUsuario = -1;

            this.soportaFacial = false;
            this.soportaHuella = false;
            this.soportaTarjeta = false;

        }


        //propiedades (getters y setters)

        public static Hik_Controladora_General InstanciaControladoraGeneral
        {
            get
            {
                if (instanciaControladoraGeneral == null)
                {
                    instanciaControladoraGeneral = new Hik_Controladora_General();
                }
                return instanciaControladoraGeneral;
            }
        }


        public int IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }

        public bool SoportaFacial
        {
            get { return soportaFacial; }
            set { soportaFacial = value; }
        }

        public bool SoportaHuella
        {
            get { return soportaHuella; }
            set { soportaHuella = value; }
        }

        public bool SoportaTarjeta
        {
            get { return soportaTarjeta; }
            set { soportaTarjeta = value; }
        }


        //metodos
        public static Hik_Resultado InicializarNet_DVR()
        {
            Hik_Resultado resultado = new Hik_Resultado();

            //implementar try catch y si no se puede inicializar no realizar lo demas.
            try
            {
                Hik_SDK.NET_DVR_Init();
                resultado.ActualizarResultado(true, "NET_DVR_Init éxito", Hik_SDK.NET_DVR_GetLastError().ToString());
            }
            catch
            {
                resultado.ActualizarResultado(false, $"Error al inicializar el dispositivo\nNET_DVR_Init error", Hik_SDK.NET_DVR_GetLastError().ToString());
            }

            Hik_Resultado.EscribirLog();

            return resultado;
        }

        public Hik_Resultado Login(string user, string password, string port, string ip)
        {
            Hik_Resultado resultado = new Hik_Resultado();


            //cerramos la sesion que estaba iniciada anteriormente
            if (IdUsuario >= 0)
            {
                Hik_SDK.NET_DVR_Logout_V30(idUsuario);
                IdUsuario = -1;
            }

            //creamos y cargamos las estructuras de informacion de login y de informacion del dispositivo

            Hik_SDK.NET_DVR_USER_LOGIN_INFO struLoginInfo = new Hik_SDK.NET_DVR_USER_LOGIN_INFO();
            Hik_SDK.NET_DVR_DEVICEINFO_V40 struDeviceInfoV40 = new Hik_SDK.NET_DVR_DEVICEINFO_V40();
            struDeviceInfoV40.struDeviceV30.sSerialNumber = new byte[Hik_SDK.SERIALNO_LEN];

            struLoginInfo.sDeviceAddress = ip;
            struLoginInfo.sUserName = user;
            struLoginInfo.sPassword = password;
            ushort.TryParse(port, out struLoginInfo.wPort);

            //struLoginInfo.bUseAsynLogin = false;
            


            //utilizamos metodo de iniciar sesion
            int auxUserID = -1;
            auxUserID = Hik_SDK.NET_DVR_Login_V40(ref struLoginInfo, ref struDeviceInfoV40);

            if (auxUserID >= 0)
            {
                //si da mayor a 0 signfica exito
                IdUsuario = auxUserID;
                resultado.ActualizarResultado(true, "Se inicio sesión con exito", Hik_SDK.NET_DVR_GetLastError().ToString());
            }
            else
            {
                 resultado = ProcesarErrorDeLogin(struDeviceInfoV40);
            }

            
            return resultado;
        }

        public Hik_Resultado ProcesarErrorDeLogin(Hik_SDK.NET_DVR_DEVICEINFO_V40 struDeviceInfoV40)
        {
            Hik_Resultado resultado = new Hik_Resultado();
            //sino debemos verificar el tipo de error
            uint nroError = Hik_SDK.NET_DVR_GetLastError();
            string mensajeDeSdk = "";

            switch (nroError)
            {
                case Hik_SDK.NET_DVR_PASSWORD_ERROR:
                
                    if (1 == struDeviceInfoV40.bySupportLock)
                    {
                        mensajeDeSdk = string.Format($"Te quedan {struDeviceInfoV40.byRetryLoginTime} intentos para logearte" /*struDeviceInfoV40.byRetryLoginTime*/);
                    }
                    resultado.ActualizarResultado(false, $"Usuario o contraseña invalidos \n {mensajeDeSdk}", Hik_SDK.NET_DVR_GetLastError().ToString());
                
                break;
                case Hik_SDK.NET_DVR_USER_LOCKED:

                    if (1 == struDeviceInfoV40.bySupportLock)
                    {
                        mensajeDeSdk = string.Format($"Usuario bloqueado, el tiempo restante de bloqueo es de {struDeviceInfoV40.dwSurplusLockTime}" /* struDeviceInfoV40.dwSurplusLockTime*/);
                    }
                    resultado.ActualizarResultado(false, mensajeDeSdk, Hik_SDK.NET_DVR_GetLastError().ToString());

                break;
                case Hik_SDK.NET_DVR_NETWORK_FAIL_CONNECT:
                    
                    resultado.ActualizarResultado(false, "Error IP Incorrecta o Dispositivo no conectado", Hik_SDK.NET_DVR_GetLastError().ToString());
                    break;
                case Hik_SDK.NET_DVR_DVROPRATEFAILED:
                    resultado.ActualizarResultado(false, "Falló la operación del dispositivo", Hik_SDK.NET_DVR_GetLastError().ToString());
                    break;
                default:
                    resultado.ActualizarResultado(false, "Error de Hikvision", Hik_SDK.NET_DVR_GetLastError().ToString());

                    break;
            }

            return resultado;
        }

        public void CerrarYLimpiar()
        {
            if (IdUsuario >= 0)
            {
                Hik_SDK.NET_DVR_Logout_V30(IdUsuario);
                IdUsuario = -1;
            }
            Hik_SDK.NET_DVR_Cleanup();

        }

        //el dwabilityType es el tipo de capacidad que queremos obtener. En este caso esta fijo en ACS_ABILITY
        //obtenemos un xml con TODAS las capacidades del dispositivo
        private XmlDocument? RetornarXmlConLasCapacidadesDelDispositivo()
        {
            XmlDocument? documentoXml = new XmlDocument();

            //solicitamos habilidades de acceso del dispositvo: huella digital, tarjeta y facial
            //! en caso de que surgan errores a la hora de busqeuda del XML hay que tener en cuenta esta parte.
            string xmlRequest = "<AcsAbility version=\"2.0\"><fingerPrintAbility></fingerPrintAbility><cardAbility></cardAbility><faceAbility></faceAbility></AcsAbility>";

            //Request que ira por referencia a la funcion NET_DVR_GetDeviceAbility
            nint pInBuf;

            //Tamaño del string xmlInput
            int nSize = xmlRequest.Length;

            //Documento xml que vamos a retornar
            pInBuf = Marshal.AllocHGlobal(nSize);
            pInBuf = Marshal.StringToHGlobalAnsi(xmlRequest);


            //xml que nos va a devolver la funcion NET_DVR_GetDeviceAbility
            int XML_ABILITY_OUT_LEN = 3 * 1024 * 1024; //esto seria el tamanio del xml que nos va a devolver la funcion NET_DVR_GetDeviceAbility
            nint pOutBuf = Marshal.AllocHGlobal(XML_ABILITY_OUT_LEN);

            //si nos retorna false, significa que hubo un error
            if (Hik_SDK.NET_DVR_GetDeviceAbility(IdUsuario, Hik_SDK.ACS_ABILITY, pInBuf, (uint)nSize, pOutBuf, (uint)XML_ABILITY_OUT_LEN))
            {
                //si todo salio bien, se crea el xml con el string que nos devolvio la funcion NET_DVR_GetDeviceAbility y lo retornamos
                string strOutBuf = Marshal.PtrToStringAnsi(pOutBuf, XML_ABILITY_OUT_LEN);
                documentoXml.LoadXml(strOutBuf);

                try
                {
                    // Especifica la ruta donde quieres guardar el archivo XML
                    string filePath = @"capacidadesDispositivo.xml";
                    documentoXml.Save(filePath); // Guarda el XML en el archivo 
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar el archivo XML: {ex.Message}");
                }
            }
            else
            {
                documentoXml = null;
            }

            Hik_Resultado.EscribirLog();

            //liberamos memoria
            Marshal.FreeHGlobal(pInBuf);
            Marshal.FreeHGlobal(pOutBuf);

            return documentoXml;
        }

        private string GetDescripcionErrorDeviceAbility(uint iErrCode)
        {
            string strDescription = "";
            switch (iErrCode)
            {
                case 1000:
                    strDescription = "No soportado";
                    break;
                case 1001:
                    strDescription = "Memoria insuficiente";
                    break;
                case 1002:
                    strDescription = "No se pudo encontrar el XML local correspondiente";
                    break;
                case 1003:
                    strDescription = "Error al cargar el XML local";
                    break;
                case 1004:
                    strDescription = "El formato de los datos de capacidad del dispositivo es incorrecto";
                    break;
                case 1005:
                    strDescription = "El tipo de capacidad es incorrecto";
                    break;
                case 1006:
                    strDescription = "El formato del XML de capacidad es incorrecto";
                    break;
                case 1007:
                    strDescription = "El valor del XML de capacidad de entrada es incorrecto";
                    break;
                case 1008:
                    strDescription = "La versión del XML no coincide";
                    break;
                default:
                    break;
            }
            return strDescription;
        }

        //obtenemos las capacidades de acceso del dispositivo
        private Hik_Resultado ObtenerTripleCapacidadDelDispositivo()
        {

            Hik_Resultado resultado = new Hik_Resultado();
            //leer el xml pasado por resultado

            XmlDocument? resultadoXML = RetornarXmlConLasCapacidadesDelDispositivo();

            if (resultadoXML == null)
            {
                //AcsAbility no soportado
                resultado.ActualizarResultado(false, GetDescripcionErrorDeviceAbility(1000), "1000");
            }
            else
            {
                //leer nodos <FaceParam> <Card> <FingerPrint> del resultadoXML
                SoportaFacial = VerificarCapacidad(resultadoXML, "//FaceParam");
                SoportaHuella = VerificarCapacidad(resultadoXML, "//FingerPrint");
                SoportaTarjeta = VerificarCapacidad(resultadoXML, "//Card");

                // Dar valor a resultado
                resultado.ActualizarResultado(true, $"Soporta reconocimiento facial: {SoportaFacial}. Soporta huella digital: {SoportaHuella}. Soporta tarjeta: {SoportaTarjeta}", Hik_SDK.NET_DVR_GetLastError().ToString());

            }

            Hik_Resultado.EscribirLog();

            return resultado;
        }

        
        public int ObtenerCapacidadCarasDispositivo()
        {

            int capacidad = -1;
            XmlDocument? resultadoXML = RetornarXmlConLasCapacidadesDelDispositivo();

            if (resultadoXML != null)
            {
                XmlNode valor = resultadoXML.SelectSingleNode("//maxWhiteFaceNum");

                if (valor != null)
                {
                    capacidad = int.Parse(valor.InnerText.Trim());
                }

            }
            else
            {
                Console.WriteLine("El xml es null");
            }

            return capacidad;
        }
        

        private bool VerificarCapacidad(XmlDocument resultadoXML, string capacidad)
        {
            bool soporta = false;
            XmlNode? nodoBuscado = resultadoXML.SelectSingleNode(capacidad);

            if (nodoBuscado != null)
            {
                soporta = true;
            }

            return soporta;

        }


        //INICIALIZAMOS TODO
        public Hik_Resultado InicializarPrograma(string user, string password, string port, string ip)
        {
            Hik_Resultado resultado = new Hik_Resultado();

            resultado = InicializarDVR();
            if (!resultado.Exito) return resultado;

            resultado = HacerLogin(user, password, port, ip);    
            if (!resultado.Exito) return resultado;

            resultado = ObtenerCapacidades();
            if (!resultado.Exito) return resultado;

            ActualizarCapacidadCarasDispositivo();
            SetTiempoDispositivo(DateTime.Now);
            ConfigurarCallbacks();
            
            return resultado;
        }


        private Hik_Resultado InicializarDVR()
        {
            return InicializarNet_DVR();
        }

        private Hik_Resultado HacerLogin(string user, string password, string port, string ip)
        {
            var resultado = Login(user, password, port, ip);
            Log.Information($"Resultado login: Exito: {resultado.Exito} Mensaje: {resultado.Mensaje} Codigo: {resultado.Codigo}");
            return resultado;
        }

        private Hik_Resultado ObtenerCapacidades()
        {
            var resultado = ObtenerTripleCapacidadDelDispositivo();
            Log.Information($"Capacidades del dispositivo: Exito: {resultado.Exito} Mensaje: {resultado.Mensaje} Codigo: {resultado.Codigo}");
            if (!resultado.Exito)
            {
                Log.Information($"El dipositivo no soporta reconocimiento facial de Dx");
            }

            return resultado;
        }

        private void ActualizarCapacidadCarasDispositivo()
        {
            ConfiguracionEstilos configuracion = ConfiguracionEstilos.LeerJsonConfiguracion();
            configuracion.ActualizarCapacidadMaxima();

        }

        private void ConfigurarCallbacks()
        {
            //setteamos el callback para obtener los ids de los usuarios
            hik_Controladora_Eventos = Hik_Controladora_Eventos.InstanciaControladoraEventos;
            hik_Controladora_Facial = Hik_Controladora_Facial.ObtenerInstancia;
            hik_Controladora_Tarjetas = Hik_Controladora_Tarjetas.ObtenerInstancia;
        }

        public DateTime? ObtenerTiempoDispositivo()
        {
            Hik_Resultado resultado = new Hik_Resultado();
            DateTime? tiempoDisp = null;

            //Inicializo los valores necesarios para obtener el tiempo del dispositivo

            UInt32 dwReturn = 0;
            Int32 nSize = Marshal.SizeOf(m_struTimeCfg);
            IntPtr ptrTimeCfg = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(m_struTimeCfg, ptrTimeCfg, false);

            //obtenemos el tiempo del dispositivo
            if (!Hik_SDK.NET_DVR_GetDVRConfig(idUsuario, Hik_SDK.NET_DVR_GET_TIMECFG, -1, ptrTimeCfg, (UInt32)nSize, ref dwReturn))
            {
       
                resultado.ActualizarResultado(false, 
                    "Error al obtener el tiempo del dispositivo", 
                    Hik_SDK.NET_DVR_GetLastError().ToString()
                );

                resultado.EscribirResultado("Obtener Tiempo Dispositivo");
            }
            else
            {
                m_struTimeCfg = (Hik_SDK.NET_DVR_TIME)Marshal.PtrToStructure(ptrTimeCfg, typeof(Hik_SDK.NET_DVR_TIME));
                 
                tiempoDisp = new DateTime(
                    m_struTimeCfg.dwYear, 
                    m_struTimeCfg.dwMonth,
                    m_struTimeCfg.dwDay,
                    m_struTimeCfg.dwHour,
                    m_struTimeCfg.dwMinute,
                    m_struTimeCfg.dwSecond
                );
                
            }
            Marshal.FreeHGlobal(ptrTimeCfg);
            return tiempoDisp;
        }

        public void SetTiempoDispositivo(DateTime nuevoTiempo)
        {
            Hik_Resultado resultado = new Hik_Resultado();

            // Creamos el tiempo a settear
            m_struTimeCfg.dwYear = nuevoTiempo.Year;
            m_struTimeCfg.dwMonth = nuevoTiempo.Month;
            m_struTimeCfg.dwDay = nuevoTiempo.Day;
            m_struTimeCfg.dwHour = nuevoTiempo.Hour;
            m_struTimeCfg.dwMinute = nuevoTiempo.Minute;
            m_struTimeCfg.dwSecond = nuevoTiempo.Second;

            // Inicializo los valores necesarios para settear el tiempo del dispositivo
            Int32 nSize = Marshal.SizeOf(m_struTimeCfg);
            IntPtr ptrTimeCfg = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(m_struTimeCfg, ptrTimeCfg, false);

            if (!Hik_SDK.NET_DVR_SetDVRConfig(idUsuario, Hik_SDK.NET_DVR_SET_TIMECFG, -1, ptrTimeCfg, (UInt32)nSize))
            {
                resultado.ActualizarResultado(false,
                    "Error al obtener el tiempo del dispositivo",
                    Hik_SDK.NET_DVR_GetLastError().ToString()
                );

            }
            else
            {
                resultado.ActualizarResultado(true,
                    "El tiempo del dispositivo se asigno con exito",
                    "1"
                );

            }

            Log.Information($"Resultado login: Exito: {resultado.Exito} Mensaje: {resultado.Mensaje} Codigo: {resultado.Codigo}");

            Marshal.FreeHGlobal(ptrTimeCfg);
        }


        public Hik_Resultado AltaCliente(string idCliente, string nombre)
        {
            Hik_Resultado resultado = new Hik_Resultado();

            //Busco si la tarjeta existe
            resultado = ObtenerTarjeta(idCliente);
            if (resultado.Exito) return resultado;
            
            //Pauso el hilo para que no se cargue el dispositivo
            Thread.Sleep(1000);

            resultado = CapturarFoto();
            if (!resultado.Exito) return resultado;


            resultado = CrearTarjeta(idCliente, nombre);
            if (!resultado.Exito) return resultado;

            resultado = AsignarCaraATarjeta(idCliente);
            if (!resultado.Exito) return resultado;

            ActualizarCaras(nombre, idCliente);

            return resultado;
        }
        


        private Hik_Resultado ObtenerTarjeta(string idCliente)
        {
            var resultado = Hik_Controladora_Tarjetas.ObtenerInstancia.ObtenerUnaTarjeta(int.Parse(idCliente));
            resultado.Mensaje = "Error de obtener la tarjeta";
            return resultado;
        }

        private Hik_Resultado CapturarFoto()
        {
            var resultado = Hik_Controladora_Facial.ObtenerInstancia.CapturarCara();
            resultado.Mensaje = "Error de obtener la cara";
            return resultado;
        }

        private Hik_Resultado CrearTarjeta(string idCliente, string nombre)
        {
            var resultado = Hik_Controladora_Tarjetas.ObtenerInstancia.EstablecerUnaTarjeta(int.Parse(idCliente), nombre);
            resultado.Mensaje = "Error de crear una tarjeta";
            return resultado;
        }

        private Hik_Resultado AsignarCaraATarjeta(string idCliente)
        {
            var resultado = Hik_Controladora_Facial.ObtenerInstancia.EstablecerUnaCara((uint)numeroLector, idCliente);
            resultado.Mensaje = "Error de establecer una cara";
            return resultado;
        }


        private void ActualizarCaras(string nombre, string idCliente)
        {
            ConfiguracionEstilos configuracion = ConfiguracionEstilos.LeerJsonConfiguracion();
            ConservarImagenSocio(configuracion, nombre, idCliente);
            configuracion.SumarRegistroCara();
        }

        private static string CambiarNombreFoto(string nombreCompletoSocio, string idSocio)
        {
            string aux = Regex.Replace(nombreCompletoSocio, "'", "");
            return Regex.Replace(aux, " ", "_") + "_" + idSocio + ".jpg";
        }



        public void ConservarImagenSocio(ConfiguracionEstilos configuracion, string nombreCompletoSocio, string idSocio)
        {            
            if (!configuracion.AlmacenarFotoSocio)
            {
                return;
            }

            //Obtengo las rutas necesarias
            string rutaOriginal = "captura.jpg";
            string rutaNueva = configuracion.RutaCarpeta;
          

            if (string.IsNullOrEmpty(rutaNueva))
            {
                return;
            }

            try
            {
                //Si no existe el directorio, lo creo 
                if (!Directory.Exists(rutaNueva))
                {
                    Directory.CreateDirectory(rutaNueva);
                }

                //Configuro el nombre de la foto
                string nuevoNombre = BuscarImagenSocioUtils.CambiarNombreFoto(nombreCompletoSocio, idSocio);
                string rutaDestino = Path.Combine(rutaNueva, nuevoNombre);

                //Hago la copia de un directorio a otro
                File.Copy(rutaOriginal, rutaDestino, overwrite: true);

            }
            catch (Exception ex)
            {
                Log.Error("Error al guardar la imagen del socio: "+ex.ToString());
            }
        }

        public Hik_Resultado BajaCliente(string id)
        {
            Hik_Resultado resultado = new Hik_Resultado();

            resultado = ObtenerTarjeta(id);
            if(!resultado.Exito) return resultado;


            EsperarCooldownDispositivo(1000);

            resultado = EliminarCaraDeTarjeta(id);
            if (!resultado.Exito) return resultado;
            

            resultado = EliminarTarjeta(id);
            if (!resultado.Exito) return resultado;

            RestarCara();

            return resultado;

        }

        private Hik_Resultado EliminarCaraDeTarjeta(string id)
        {
            var resultado = Hik_Controladora_Facial.ObtenerInstancia.EliminarCara(numeroLector, id);
            return resultado;
        }

        private Hik_Resultado EliminarTarjeta(string id)
        {
           var resultado = Hik_Controladora_Tarjetas.ObtenerInstancia.EliminarTarjetaPorId(int.Parse(id));
           return resultado;
        }

        private void RestarCara()
        {
            ConfiguracionEstilos configuracion = ConfiguracionEstilos.LeerJsonConfiguracion();
            configuracion.RestarRegistroCara();
        }

        private void EsperarCooldownDispositivo(int ms)
        {
            Thread.Sleep(ms);
        }
        //public Hik_Resultado BajaMasivaClientes(string[] ids)
        //{
        //    Hik_Resultado resultado = new Hik_Resultado();

        //    foreach (string id in ids)
        //    {
        //        resultado = BajaCliente(id);
        //    }

        //    return resultado;
        //}


    }
}
