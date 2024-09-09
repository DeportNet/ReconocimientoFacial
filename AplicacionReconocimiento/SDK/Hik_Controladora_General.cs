using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DeportNetReconocimiento.SDK
{
    public class Hik_Controladora_General
    {
        //atributos
         
        public static int idUsuario = -1; //esta bien que sea estatico ya que solo puede haber solo un user_ID

        //propiedades (getters y setters)
        public int IdUsuario
        {
            get{ return idUsuario; }
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
         
    }
}
