using DeportNetReconocimiento.Api;
using DeportNetReconocimiento.GUI;
using DeportNetReconocimiento.SDK;
using DeportNetReconocimiento.SDKHikvision;
using DeportNetReconocimiento.Utils;
using System.Diagnostics;
using static DeportNetReconocimiento.SDK.Hik_SDK;

namespace DeportNetReconocimiento
{
    internal class Programa
    {
        private static ApiServer apiServer;

        [STAThread]
        static void Main(string[] args)
        {
            apiServer = new ApiServer();
            apiServer.Start();
            
            ApplicationConfiguration.Initialize();

            Application.Run(WFPrincipal.ObtenerInstancia);

            //Nuestro: "admin", "Facundo2024*", "8000", "192.168.0.207"
            //Level : "admin", "020921Levelgym", "8000", "192.168.0.214"

            // Detener el servidor cuando la aplicación cierre
            AppDomain.CurrentDomain.ProcessExit += (s, e) => apiServer?.Stop();
        }



    }
}