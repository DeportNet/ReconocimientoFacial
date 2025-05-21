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
            if (ProgramaCorriendo())
            {
                MessageBox.Show("El programa ya esta abierto en otra ventana", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            /*API*/
            apiServer = new ApiServer();
            apiServer.Start();

           

            //iniciazamos la ventana principal de acceso
            Application.Run(WFPrincipal.ObtenerInstancia);

            // Detener el servidor cuando la aplicación cierre
            AppDomain.CurrentDomain.ProcessExit += (s, e) => apiServer?.Stop();

        }

        private static bool ProgramaCorriendo()
        {
            string nombreDeProceso = Process.GetCurrentProcess().ProcessName;
            int cantidadDeInstancias = Process.GetProcessesByName(nombreDeProceso).Length;

            if (cantidadDeInstancias > 1)
            {
                return true;
            }

            return false;
        }

    }
}