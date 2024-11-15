using DeportNetReconocimiento.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.SDKHikvision
{
    internal class Hik_Controladora_Eventos
    {

        public int GetAcsEventHandle = -2;
        private string CsTemp = null;
        private int m_lLogNum = 0;
        Thread m_pDisplayListThread = null;

        public  Hik_Resultado CapturarEvento()
        {
            Hik_Resultado resultado = new Hik_Resultado();
            Console.WriteLine("Entro 1");
            m_lLogNum = 0;


            Hik_SDK.NET_DVR_ACS_EVENT_COND struCond = new Hik_SDK.NET_DVR_ACS_EVENT_COND();
            IntPtr ptrCond = IntPtr.Zero;
            uint dwSize = 0;
            InicializarEventCond(ref struCond,ref ptrCond,ref dwSize);

            


            GetAcsEventHandle = Hik_SDK.NET_DVR_StartRemoteConfig(Hik_Controladora_General.IdUsuario, Hik_SDK.NET_DVR_GET_ACS_EVENT, ptrCond, (int)dwSize, null, IntPtr.Zero);

            if (-1 == GetAcsEventHandle)
            {
                Console.WriteLine("Entro 4 - es -1");

                resultado.Exito =  false;
                resultado.Codigo =Hik_SDK.NET_DVR_GetLastError().ToString();
                resultado.Mensaje= "Error al obtener el evento";
               
            } else
            {
                Console.WriteLine("Entro 5 - SIIIII");

                VerificarEstadoEvento();

               // m_pDisplayListThread = new Thread(VerificarEstadoEvento);
               // m_pDisplayListThread.Start();
            }

            Marshal.FreeHGlobal(ptrCond);
            return resultado;
        }


        public void InicializarEventCond(ref Hik_SDK.NET_DVR_ACS_EVENT_COND struCond, ref IntPtr ptrCond, ref uint dwSize)
        {
            struCond.Init();
            struCond.dwSize = (uint)Marshal.SizeOf(struCond);


            struCond.dwMajor = 0;
            struCond.dwMinor = 0;


            struCond.struStartTime.dwYear = 2024;
            struCond.struStartTime.dwMonth = 11;
            struCond.struStartTime.dwDay = 14;
            struCond.struStartTime.dwHour = 1;
            struCond.struStartTime.dwMinute = 1;
            struCond.struStartTime.dwSecond = 1;

            struCond.struEndTime.dwYear = 2024;
            struCond.struEndTime.dwMonth = 11;
            struCond.struEndTime.dwDay = 14;
            struCond.struEndTime.dwHour = 20;
            struCond.struEndTime.dwMinute = 1;
            struCond.struEndTime.dwSecond = 1;

            struCond.byPicEnable = 0;
            struCond.szMonitorID = "";
            struCond.wInductiveEventType = 65535;

            dwSize = struCond.dwSize;

            ptrCond= Marshal.AllocHGlobal((int)dwSize);
            Marshal.StructureToPtr(struCond, ptrCond, false);
        }

        public void InicializarEventCfg(ref Hik_SDK.NET_DVR_ACS_EVENT_CFG struCfg,ref int dwOutBuffSize)
        {
            struCfg.dwSize = (uint)Marshal.SizeOf(struCfg);

            dwOutBuffSize = (int)struCfg.dwSize;

            struCfg.Init();
        }

        public void VerificarEstadoEvento() 
        {
            Console.WriteLine("Entro 6 Estoy dentro de verificar estado");

            Hik_Resultado resultado = new Hik_Resultado();  
            int dwStatus = 0;
            bool flag = true;

            Hik_SDK.NET_DVR_ACS_EVENT_CFG struCfg = new Hik_SDK.NET_DVR_ACS_EVENT_CFG();

            struCfg.dwSize = (uint)Marshal.SizeOf(struCfg);
            int dwOutBuffSize = 0;
            InicializarEventCfg(ref struCfg, ref dwOutBuffSize);

            Console.WriteLine("Entro 7 - Cargó todo (WTF)");

            while (flag)
            {
                dwStatus = Hik_SDK.NET_DVR_GetNextRemoteConfig_AcsEventCgf(GetAcsEventHandle, ref struCfg, dwOutBuffSize);
                switch (dwStatus)
                {
                    case Hik_SDK.NET_SDK_GET_NEXT_STATUS_SUCCESS:
                        ProcesarAcsEvent(ref struCfg, ref flag);

                        break;
                    case Hik_SDK.NET_SDK_GET_NEXT_STATUS_NEED_WAIT:
                        Thread.Sleep(200);
                        break;
                    case Hik_SDK.NET_SDK_GET_NEXT_STATUS_FAILED:
                        Hik_SDK.NET_DVR_StopRemoteConfig(GetAcsEventHandle);
                        Console.WriteLine("NET_SDK_GET_NEXT_STATUS_FAILED" + Hik_SDK.NET_DVR_GetLastError().ToString());

                        flag = false;
                        break;
                    case Hik_SDK.NET_SDK_GET_NEXT_STATUS_FINISH:
                        Hik_SDK.NET_DVR_StopRemoteConfig(GetAcsEventHandle);
                        Console.WriteLine("Termino el proceso de estado");
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("NET_SDK_GET_NEXT_STATUS_UNKNOWN" + Hik_SDK.NET_DVR_GetLastError().ToString());
                        flag = false;
                        Hik_SDK.NET_DVR_StopRemoteConfig(GetAcsEventHandle);
                        break;
                }
            }

        }


        private Hik_Resultado ProcesarAcsEvent(ref Hik_SDK.NET_DVR_ACS_EVENT_CFG struCFG, ref bool flag)
        {
            Console.WriteLine("Entro 8 - Me zarpe mal");

            Hik_Resultado resultado = new Hik_Resultado();
            try
            {
                Console.WriteLine("Entro 9 - tienen que cerrar el estadio");

                Console.WriteLine(struCFG.struTime.dwMinute);
                resultado.Exito = true;
                resultado.Mensaje = ("Se obtuvo el evento con la siguiente estructura " + struCFG.ToString());
            }
            catch
            {
                Console.WriteLine("Entro 10 - Llegue bastante lejos ");

                resultado.Exito= false;
                resultado.Mensaje = "Error al procesar la cara";
                resultado.Codigo = Hik_SDK.NET_DVR_GetLastError().ToString();
                flag = false;
            }

            return resultado;
        }


    }
}
