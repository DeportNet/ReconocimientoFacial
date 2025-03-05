using DeportNetReconocimiento.GUI;
using DeportNetReconocimiento.SDKHikvision;
using DeportNetReconocimiento.Utils;
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

            Hik_Resultado.EscribirLog();
            
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
                SoportaFacial = VarificarCapacidad(resultadoXML, "//FaceParam");
                SoportaHuella = VarificarCapacidad(resultadoXML, "//FingerPrint");
                SoportaTarjeta = VarificarCapacidad(resultadoXML, "//Card");

                // Dar valor a resultado
                resultado.ActualizarResultado(true, $"Soporta reconocimiento facial: {SoportaFacial} \nSoporta huella digital: {SoportaHuella} \nSoporta tarjeta: {SoportaTarjeta}", Hik_SDK.NET_DVR_GetLastError().ToString());

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
        

        private bool VarificarCapacidad(XmlDocument resultadoXML, string capacidad)
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
            ConfiguracionEstilos configuracion= ConfiguracionEstilos.LeerJsonConfiguracion("configuracionEstilos");
            Hik_Resultado resultado = new Hik_Resultado();

            resultado = InicializarNet_DVR();

            if (!resultado.Exito)
            {
                //si no se pudo inicializar
                return resultado;
            }

            //nos loggeamos
            resultado = Login(user, password, port, ip);
            resultado.EscribirResultado("Login");

            if (!resultado.Exito)
            {
                //si no se pudo Loggear
                return resultado;
            }

            //obtenemos las capacidades
            resultado = ObtenerTripleCapacidadDelDispositivo();

            if (!resultado.Exito)
            {
                //si no hubo exito, signfica que directamente el dispositivo no soporta acceso
                return resultado;
            }

            configuracion.ActualizarCapacidadMaxima();

            //setteamos el callback para obtener los ids de los usuarios
            hik_Controladora_Eventos = Hik_Controladora_Eventos.InstanciaControladoraEventos;
            hik_Controladora_Facial = Hik_Controladora_Facial.ObtenerInstancia;
            hik_Controladora_Tarjetas = Hik_Controladora_Tarjetas.ObtenerInstancia;
            
            return resultado;
        }


        //Verificar conexión a internet o en general
        public static bool ComprobarConexionInternet()
        {
            //ponemos flag en false como predeterminado
            bool flag = false;

            Ping pingSender = new Ping();
            string direccion = "8.8.8.8"; // IP de Google

            try
            {
                //respuesta que nos da el enviador de ping
                PingReply reply = pingSender.Send(direccion);

                if (reply.Status == IPStatus.Success)
                {
                    flag = true;
                    Console.WriteLine("Tenemos conexion a internet; Tiempo: " + reply.RoundtripTime + " ms");
                    //Console.WriteLine("Dirección: " + reply.Address.ToString());
                    
                }
                else
                {

                    Console.WriteLine("No se pudo conectar: " + reply.Status);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            return flag;
        }


        public Hik_Resultado AltaCliente(string idCliente, string nombre)
        {
            Hik_Resultado resultado = new Hik_Resultado();
            ConfiguracionEstilos configuracion = ConfiguracionEstilos.LeerJsonConfiguracion("configuracionEstilos");

            //Busco si la tarjeta existe
            resultado = Hik_Controladora_Tarjetas.ObtenerInstancia.ObtenerUnaTarjeta(int.Parse(idCliente));
            if (resultado.Exito)
            {
               // MessageBox.Show("Error de obtener la tarjeta");
                resultado.Mensaje = "Error de obtener la tarjeta";
                return resultado;
            }
            
            //Pauso el hilo para que no se cargue el dispositivo
            Thread.Sleep(1000);


            //Capturo la foto
            resultado = Hik_Controladora_Facial.ObtenerInstancia.CapturarCara();
            if (!resultado.Exito)
            {
                //MessageBox.Show("Error de obtener la cara");
                resultado.Mensaje = "Error de obtener la cara";
                return resultado;
            }

            //Creo la tarjeta
            resultado = Hik_Controladora_Tarjetas.ObtenerInstancia.EstablecerUnaTarjeta(int.Parse(idCliente), nombre);
            if (!resultado.Exito)
            {
                //MessageBox.Show("Error de crear una tarjeta");
                resultado.Mensaje = "Error de crear una tarjeta";
                return resultado;
            }

            //Asigno la cara a la tarjeta
            resultado = Hik_Controladora_Facial.ObtenerInstancia.EstablecerUnaCara(1, idCliente);
            if (!resultado.Exito)
            {
                //MessageBox.Show("Error de establecer una cara");
                resultado.Mensaje = "Error de establecer una cara";
                return resultado;
            }

            ConservarImagenSocio(configuracion, nombre, idCliente);
            configuracion.SumarRegistroCara();

            return resultado;
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
                string nuevoNombre = CambiarNombreFoto(nombreCompletoSocio, idSocio);
                string rutaDestino = Path.Combine(rutaNueva, nuevoNombre);

                //Hago la copia de un directorio a otro
                File.Copy(rutaOriginal, rutaDestino, overwrite: true);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public Hik_Resultado BajaCliente(string id)
        {
            ConfiguracionEstilos configuracion = ConfiguracionEstilos.LeerJsonConfiguracion("configuracionEstilos");
            Hik_Resultado resultado = new Hik_Resultado();

            //Buscar tarjeta
            resultado = Hik_Controladora_Tarjetas.ObtenerInstancia.ObtenerUnaTarjeta(int.Parse(id));
            if (!resultado.Exito)
            {
                return resultado;
            }

            //Pausa el  hilo para no sobrecargar el dispositivo
            Thread.Sleep(1000);

            //Eliminar cara de la tarjeta
            resultado = Hik_Controladora_Facial.ObtenerInstancia.EliminarCara(1, id);
            if (!resultado.Exito)
            {
                return resultado;
            }

            //Eliminar tarjeta
            resultado = Hik_Controladora_Tarjetas.ObtenerInstancia.EliminarTarjetaPorId(int.Parse(id));
            if (!resultado.Exito)
            {
                return resultado;
            }


            configuracion.RestarRegistroCara();
            return resultado;

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
