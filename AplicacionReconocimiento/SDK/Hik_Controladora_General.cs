using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace DeportNetReconocimiento.SDK
{
    public class Hik_Controladora_General
    {
        //atributos
         
        public static int idUsuario = -1; //esta bien que sea estatico ya que solo puede haber solo un user_ID
        
        private static bool soportaFacial;
        private static bool soportaHuella;
        private static bool soportaTarjeta;

        //propiedades (getters y setters)
        public static int IdUsuario
        {
            get{ return idUsuario; }
        }       
        public static bool SoportaFacial
        {
            get { return soportaFacial; }
        }

        public static bool SoportaHuella
        {
            get { return soportaHuella; }
        }

        public static bool SoportaTarjeta
        {
            get { return soportaTarjeta; }
        }

        //metodos
        public static Hik_Resultado Inicializar()
        {
            Hik_Resultado resultado = new Hik_Resultado();
           
            bool entrada= Hik_SDK.NET_DVR_Init();

            if (entrada == false)
            {
                System.Console.WriteLine("NET_DVR_Init error");

                resultado.MensajeDeError = "NET_DVR_Init error";
                
                resultado.Exito = false;

            }else{
                System.Console.WriteLine("NET_DVR_Init éxito");

                resultado.MensajeDeExito = "NET_DVR_Init éxito";
                resultado.Exito= true;
               
            }

            Hik_Resultado.EscribirLog();
        
            return resultado;
        }

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
                Console.WriteLine("Se inicio sesión con exito");
                loginResultado.MensajeDeExito = "Se inicio sesión con exito";
                loginResultado.Exito= true;
            }
            else
            {
                //sino debemos verificar el tipo de error
                uint nroError = Hik_SDK.NET_DVR_GetLastError();
                string mensajeDeSdk= "";


                if (nroError == Hik_SDK.NET_DVR_PASSWORD_ERROR)
                {
                    Console.WriteLine("Usuario o contraseña invalidos");
                    loginResultado.Exito = false;
                    loginResultado.MensajeDeError= "Usuario o contraseña invalidos";
                    if (1 == struDeviceInfoV40.bySupportLock)
                    {
                        mensajeDeSdk = string.Format("Te quedan {0} intentos para logearte", struDeviceInfoV40.byRetryLoginTime);
                        Console.WriteLine(mensajeDeSdk);
                    }
                }
                else if (nroError == Hik_SDK.NET_DVR_USER_LOCKED)
                {
                    if (1 == struDeviceInfoV40.bySupportLock)
                    {
                        mensajeDeSdk = string.Format("Usuario bloqueado, el tiempo restante de bloqueo es de {0}", struDeviceInfoV40.dwSurplusLockTime);
                        Console.WriteLine(mensajeDeSdk);


                        loginResultado.Exito = false;
                        loginResultado.MensajeDeError= mensajeDeSdk;
                    }
                }
                else
                {
                    Console.WriteLine("Error de red o el panel esta ocupado");
                    
                    loginResultado.Exito = false;
                    loginResultado.MensajeDeError= "Error de red o el panel esta ocupado";
                }
            }

            return loginResultado;
        }

        public void CerrarYLimpiar()
        {
            if (idUsuario >= 0)
            {
                Hik_SDK.NET_DVR_Logout_V30(idUsuario);
                idUsuario = -1;
            }
            Hik_SDK.NET_DVR_Cleanup();

        }

        //el dwabilityType es el tipo de capacidad que queremos obtener. En este caso esta fijo en ACS_ABILITY
        private XmlDocument? RetornarXmlConLasCapacidadesDeAcceso()
        {
            XmlDocument? documentoXml = null;

            //solicitamos hbailidades de acceso del dispositvo: huella digital, tarjeta y facial
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
            if (Hik_SDK.NET_DVR_GetDeviceAbility(idUsuario, Hik_SDK.ACS_ABILITY, pInBuf, (uint)nSize, pOutBuf, (uint)XML_ABILITY_OUT_LEN))
            {
                //si todo salio bien, se crea el xml con el string que nos devolvio la funcion NET_DVR_GetDeviceAbility y lo retornamos
                string strOutBuf = Marshal.PtrToStringAnsi(pOutBuf, XML_ABILITY_OUT_LEN);
                documentoXml.LoadXml(strOutBuf);
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

        public Hik_Resultado ObtenerTripleCapacidadDelDispositivo()
        {

            XmlDocument? resultadoXML = RetornarXmlConLasCapacidadesDeAcceso();
            //leer el xml pasado por resultado
            Hik_Resultado resultado = new Hik_Resultado();


            if (resultadoXML == null)
            {
                //AcsAbility no soportado
                resultado.NumeroDeError = "1000";
                resultado.MensajeDeError = GetDescripcionErrorDeviceAbility(1000);
                resultado.Exito = false;
            }
            else
            {
                //leer nodos <FaceParam> <Card> <FingerPrint> del resultadoXML
                soportaFacial = VerificarCapacidad(resultadoXML, "//FaceParam");
                soportaHuella = VerificarCapacidad(resultadoXML, "//FingerPrint");
                soportaTarjeta = VerificarCapacidad(resultadoXML, "//Card");

                // Dar valor a resultado
                resultado.Exito = true;
                resultado.MensajeDeExito = $"Soporta reconocimiento facial: {soportaFacial} \nSoporta huella digital: {soportaHuella} \nSoporta tarjeta: {soportaTarjeta}";

            }

            return resultado;
        }
        
        private bool VerificarCapacidad(XmlDocument resultadoXML, string parametro)
        {
            bool soporta = false;
            XmlNode? nodoBuscado = resultadoXML.SelectSingleNode(parametro);

            if(nodoBuscado!= null)
            {
                soporta = true;
            }

            return soporta;

        }


    }
}
