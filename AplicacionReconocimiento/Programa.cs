using DeportNetReconocimiento.Api;
using DeportNetReconocimiento.BD;
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
            /*Base de Datos*/
            BdClientes bdClientes = new BdClientes();
            
            /*API*/
            apiServer = new ApiServer();
            apiServer.Start();

            //esto no me acuerdo que era
            //ApplicationConfiguration.Initialize();


            //iniciazamos la ventana principal de acceso
            Application.Run(WFPrincipal.ObtenerInstancia);

            // Detener el servidor cuando la aplicación cierre
            AppDomain.CurrentDomain.ProcessExit += (s, e) => apiServer?.Stop();

        }
            //Nuestro: "admin", "Facundo2024*", "8000", "192.168.0.207"
            //Level : "admin", "020921Levelgym", "8000", "192.168.0.214"
    }
}