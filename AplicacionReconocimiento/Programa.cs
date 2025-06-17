using DeportNetReconocimiento.Api;
using DeportNetReconocimiento.GUI;
using DeportNetReconocimiento.SDK;
using DeportNetReconocimiento.SDKHikvision;
using DeportNetReconocimiento.Utils;
using Serilog;
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

            InicializarLogger();

            /*API*/
            apiServer = new ApiServer();
            apiServer.Start();


            Log.Information("Aplicacion iniciada.");

            //iniciazamos la ventana principal de acceso
            Application.Run(WFPrincipal.ObtenerInstancia);
            
            // Detener el servidor cuando la aplicación cierre
            AppDomain.CurrentDomain.ProcessExit += (s, e) => apiServer?.Stop();

            Log.Information("Aplicacion cerrada.");
        }

        private static void InicializarLogger()
        {
            // Configurar Serilog para registrar en la consola y en un archivo

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information() // puedes cambiar a Information para prod
            .WriteTo.Console()
            .WriteTo.File(
                "LogsDeportnetReconocimiento/log-.log",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 60 // mantener solo últimos 60 días
            )
            .CreateLogger();

            
        }

        private static bool ProgramaCorriendo()
        {
            string nombreDeProceso = Process.GetCurrentProcess().ProcessName;
            int cantidadDeInstancias = Process.GetProcessesByName(nombreDeProceso).Length;

            if (cantidadDeInstancias > 1)
            {
                Log.Information("Se intento abrir el programa de nuevo y este ya esta corriendo.");
                return true;
            }

            return false;
        }

    }
}