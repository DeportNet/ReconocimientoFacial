using DeportNetReconocimiento.Modelo;
using DeportNetReconocimiento.SDKHikvision;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using static DeportNetReconocimiento.SDK.Hik_SDK;


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
        private Hik_Controladora_Facial? hik_Controladora_Facial;
        private Hik_Controladora_Tarjetas? hik_Controladora_Tarjetas;
        private Hik_Controladora_Eventos? hik_Controladora_Eventos;

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
            get{ return idUsuario; }
            set{ idUsuario = value; }
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
                resultado.Mensaje = "NET_DVR_Init éxito";
                resultado.Exito= true;

            } catch
            { 
                resultado.Exito = false;
                resultado.Mensaje = "NET_DVR_Init error";
            }

            Hik_Resultado.EscribirLog();
        
            return resultado;
        }

        public Hik_Resultado Login(string user, string password, string port, string ip)
        {
            Hik_Resultado loginResultado = new Hik_Resultado();

            
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
                loginResultado.Mensaje = "Se inicio sesión con exito";
                loginResultado.Exito= true;
            }
            else
            {
                ProcesarErrorDeLogin(struDeviceInfoV40);
            }

            Hik_Resultado.EscribirLog();

            return loginResultado;
        }

        public Hik_Resultado ProcesarErrorDeLogin(Hik_SDK.NET_DVR_DEVICEINFO_V40 struDeviceInfoV40)
        {
            Hik_Resultado loginResultado = new Hik_Resultado();
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

            return loginResultado;
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
                    string filePath = @"archivoXML.xml";
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
                resultado.Codigo = "1000";
                resultado.Mensaje= GetDescripcionErrorDeviceAbility(1000);
                resultado.Exito = false;
            }
            else
            {
                //leer nodos <FaceParam> <Card> <FingerPrint> del resultadoXML
                SoportaFacial = VerificarCapacidad(resultadoXML, "//FaceParam");
                SoportaHuella = VerificarCapacidad(resultadoXML, "//FingerPrint");
                SoportaTarjeta = VerificarCapacidad(resultadoXML, "//Card");

                // Dar valor a resultado
                resultado.Exito = true;
                resultado.Mensaje = $"Soporta reconocimiento facial: {SoportaFacial} \nSoporta huella digital: {SoportaHuella} \nSoporta tarjeta: {SoportaTarjeta}";

            }

            Hik_Resultado.EscribirLog();

            return resultado;
        }

        private bool VerificarCapacidad(XmlDocument resultadoXML, string capacidad)
        {
            bool soporta = false;
            XmlNode? nodoBuscado = resultadoXML.SelectSingleNode(capacidad);

            if(nodoBuscado!= null)
            {
                soporta = true;
            }

            return soporta;

        }
        
        //INICIALIZAMOS TODO
        public Hik_Resultado InicializarPrograma(string user, string password, string port, string ip)
        {
            Hik_Resultado resultadoGeneral = new Hik_Resultado();
            
            resultadoGeneral = InicializarNet_DVR();

            if (!resultadoGeneral.Exito)
            {
                //si no se pudo inicializar
                return resultadoGeneral;
            }


            //nos loggeamos
            resultadoGeneral = Login(user, password, port, ip);

            if (!resultadoGeneral.Exito)
            {
                //si no se pudo Loggear
                return resultadoGeneral;
            }

            //obtenemos las capacidades
            resultadoGeneral = ObtenerTripleCapacidadDelDispositivo();

            if (!resultadoGeneral.Exito)
            {
                //si no hubo exito, signfica que directamente el dispositivo no soporta acceso
                return resultadoGeneral;
            }

            //Hik_Resultado res = new Hik_Resultado();
          //  Hik_Controladora_Tarjetas controlador = new Hik_Controladora_Tarjetas();
          //  res = controlador.ObtenerUnaTarjeta(1);
           // Console.WriteLine(res.Mensaje);

            //setteamos el callback para obtener los ids de los usuarios
            this.hik_Controladora_Eventos = new Hik_Controladora_Eventos();

            
            return resultadoGeneral;
        }


        //Verificar conexión a internet o en general
        public static bool VerificarConexionInternet()
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
                    Console.WriteLine("Tenemos conexion a internet");
                    Console.WriteLine("Dirección: " + reply.Address.ToString());
                    Console.WriteLine("Tiempo: " + reply.RoundtripTime + " ms");
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




    }
}
