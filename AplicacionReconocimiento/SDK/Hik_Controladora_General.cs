using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DeportNetReconocimiento.SDK
{
    public class Hik_Controladora_General
    {
        
         
        public static int m_UserID = -1; //esta bien que sea estatico ya que solo puede haber solo un user_ID


        public int m_lGetCardCfgHandle = -1;
        public int m_lSetCardCfgHandle = -1;
        public int m_lGetAllCardsCfgHandle = -1;
        public int m_lDelCardCfgHandle = -1;
        public System.Timers.Timer timerTimeout;
   

        public bool complete = false;
        public bool getAllCardSuccess = false;

        public Hik_Resultado Initialize()
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

            Hik_SDK.NET_DVR_SetLogToFile(3, "..\\LogsAplicacion", false);
        
            return resultado;
        }


        public int UserID
        {
            get{ return m_UserID; }
        }



        public Hik_Resultado Login(string user, string password, string port, string ip)
        {

            Hik_Resultado resultHC = new Hik_Resultado();

            if (m_UserID >= 0)
            {
                Hik_SDK.NET_DVR_Logout_V30(m_UserID);
                m_UserID = -1;
            }
            Hik_SDK  .NET_DVR_USER_LOGIN_INFO struLoginInfo = new Hik_SDK.NET_DVR_USER_LOGIN_INFO();
            Hik_SDK.NET_DVR_DEVICEINFO_V40 struDeviceInfoV40 = new Hik_SDK.NET_DVR_DEVICEINFO_V40();
            struDeviceInfoV40.struDeviceV30.sSerialNumber = new byte[Hik_SDK.SERIALNO_LEN];

            struLoginInfo.sDeviceAddress = ip;
            struLoginInfo.sUserName = user;
            struLoginInfo.sPassword = password;
            ushort.TryParse(port, out struLoginInfo.wPort);

            int lUserID = -1;
            lUserID = Hik_SDK.NET_DVR_Login_V40(ref struLoginInfo, ref struDeviceInfoV40);

            if (lUserID >= 0)
            {
                m_UserID = lUserID;
                Console.WriteLine("Se inicio sesión con exito");
                resultHC.MensajeDeExito = "Se inicio sesión con exito";
                resultHC.Exito= true;
            }
            else
            {
                uint nErr = Hik_SDK.NET_DVR_GetLastError();
                if (nErr == Hik_SDK.NET_DVR_PASSWORD_ERROR)
                {
                    Console.WriteLine("Usuaro o contraseña invalidos");
                    resultHC.Exito = false;
                    resultHC.MensajeDeError= "Usuaro o contraseña invalidos";
                    if (1 == struDeviceInfoV40.bySupportLock)
                    {
                        string strTemp1 = string.Format("Left {0} try opportunity", struDeviceInfoV40.byRetryLoginTime);
                        Console.WriteLine(strTemp1);
                    }
                }
                else if (nErr == Hik_SDK.NET_DVR_USER_LOCKED)
                {
                    if (1 == struDeviceInfoV40.bySupportLock)
                    {
                        resultHC.Exito = false;
                        string strTemp1 = string.Format("Usuario bloqueado, el tiempo restante de bloqueo es de {0}", struDeviceInfoV40.dwSurplusLockTime);
                        resultHC.MensajeDeError= strTemp1;
                        Console.WriteLine(strTemp1);
                    }
                }
                else
                {
                    resultHC.Exito = false;
                    resultHC.MensajeDeError= "Error de red o el panel esta ocupado";
                    Console.WriteLine("Error de red o el panel esta ocupado");
                }
            }

            return resultHC;
        }

        public void Close()
        {
            if (m_UserID >= 0)
            {
                Hik_SDK.NET_DVR_Logout_V30(m_UserID);
                m_UserID = -1;
            }
            Hik_SDK.NET_DVR_Cleanup();

        }
         
    }
}
