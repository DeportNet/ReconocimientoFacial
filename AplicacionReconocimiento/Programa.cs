using DeportNetReconocimiento.Api;
using DeportNetReconocimiento.GUI;
using System.Diagnostics;


namespace DeportNetReconocimiento
{
    internal class Programa
    {
        private static ApiServer apiServer;

        [STAThread]
        static void Main(string[] args)
        {

            string nombreDeProceso = Process.GetCurrentProcess().ProcessName;
            int cantidadDeInstancias = Process.GetProcessesByName(nombreDeProceso).Length;

            if (cantidadDeInstancias > 1)
            {
                MessageBox.Show("El programa ya est� corriendo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            /*API*/
            InicializarApi();

            /*Cargar BD*/


            //iniciazamos la ventana principal de acceso
            Application.Run(WFPrincipal.ObtenerInstancia);

           
            // Detener el servidor cuando la aplicaci�n cierre
            AppDomain.CurrentDomain.ProcessExit += (s, e) => apiServer?.Stop();

        }

        private static void InicializarApi()
        {
            //Instanciamos y arrancamos el servidor
            apiServer = new ApiServer();
            apiServer.Start();

        }
    }
}