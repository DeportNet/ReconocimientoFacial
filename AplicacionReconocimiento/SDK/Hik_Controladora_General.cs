using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.SDK
{
    public class Hik_Controladora_General
    {
        /*
         
             public static int m_UserID = -1;
        public Int32 m_lGetCardCfgHandle = -1;
        public Int32 m_lSetCardCfgHandle = -1;
        public Int32 m_lGetAllCardsCfgHandle = -1;
        public Int32 m_lDelCardCfgHandle = -1;
        public System.Timers.Timer timerTimeout;
   

        public bool complete = false;
        public bool getAllCardSuccess = false;

        public ResultHC Initialize()
        {
            ResultHC resultHC = new ResultHC();

            if (HCNetSDK_Events.NET_DVR_Init() == false)
            {
                System.Console.WriteLine("NET_DVR_Init error");
                resultHC.ErrorMessage = "NET_DVR_Init error";
                resultHC.Success = false;

                return resultHC;
            }

            HCNetSDK_Events.NET_DVR_SetLogToFile(3, "", false);

            resultHC.SuccessMessage = "NET_DVR_Init éxito";
            resultHC.Success = true;

            return resultHC;

        }


        public int UserID()
        {
            return m_UserID;
        }

        public ResultHC Login(string user, string password, string port, string ip)
        {

            ResultHC resultHC = new ResultHC();

            if (m_UserID >= 0)
            {
                HCNetSDK_Events.NET_DVR_Logout_V30(m_UserID);
                m_UserID = -1;
            }
            HCNetSDK_Events.NET_DVR_USER_LOGIN_INFO struLoginInfo = new HCNetSDK_Events.NET_DVR_USER_LOGIN_INFO();
            HCNetSDK_Events.NET_DVR_DEVICEINFO_V40 struDeviceInfoV40 = new HCNetSDK_Events.NET_DVR_DEVICEINFO_V40();
            struDeviceInfoV40.struDeviceV30.sSerialNumber = new byte[HCNetSDK_Events.SERIALNO_LEN];

            struLoginInfo.sDeviceAddress = ip;
            struLoginInfo.sUserName = user;
            struLoginInfo.sPassword = password;
            ushort.TryParse(port, out struLoginInfo.wPort);

            int lUserID = -1;
            lUserID = HCNetSDK_Events.NET_DVR_Login_V40(ref struLoginInfo, ref struDeviceInfoV40);

            if (lUserID >= 0)
            {
                m_UserID = lUserID;
                Console.WriteLine("Se inicio sesión con exito");
                resultHC.SuccessMessage = "Se inicio sesión con exito";
                resultHC.Success = true;
            }
            else
            {
                uint nErr = HCNetSDK_Events.NET_DVR_GetLastError();
                if (nErr == HCNetSDK_Events.NET_DVR_PASSWORD_ERROR)
                {
                    Console.WriteLine("Usuaro o contraseña invalidos");
                    resultHC.Success = false;
                    resultHC.ErrorMessage = "Usuaro o contraseña invalidos";
                    if (1 == struDeviceInfoV40.bySupportLock)
                    {
                        string strTemp1 = string.Format("Left {0} try opportunity", struDeviceInfoV40.byRetryLoginTime);
                        Console.WriteLine(strTemp1);
                    }
                }
                else if (nErr == HCNetSDK_Events.NET_DVR_USER_LOCKED)
                {
                    if (1 == struDeviceInfoV40.bySupportLock)
                    {
                        resultHC.Success = false;
                        string strTemp1 = string.Format("Usuario bloqueado, el tiempo restante de bloqueo es de {0}", struDeviceInfoV40.dwSurplusLockTime);
                        resultHC.ErrorMessage = strTemp1;
                        Console.WriteLine(strTemp1);
                    }
                }
                else
                {
                    resultHC.Success = false;
                    resultHC.ErrorMessage = "Error de red o el panel esta ocupado";
                    Console.WriteLine("Error de red o el panel esta ocupado");
                }
            }

            return resultHC;
        }

        public void Close()
        {
            if (m_UserID >= 0)
            {
                HCNetSDK_Events.NET_DVR_Logout_V30(m_UserID);
                m_UserID = -1;
            }
            HCNetSDK_Events.NET_DVR_Cleanup();

        }
         */





    }
}
