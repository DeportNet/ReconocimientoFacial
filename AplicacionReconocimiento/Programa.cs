using DeportNetReconocimiento.Api;
using DeportNetReconocimiento.GUI;
using System.Diagnostics;


namespace DeportNetReconocimiento
{
    internal class Programa
    {
        public static ApiServer apiServer;

        [STAThread]
        static void Main(string[] args)
        {

            if (ProgramaCorriendo())
            {
                MessageBox.Show("El programa ya esta abierto en otra ventana", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            /*API*/
            InicializarApi();


            /*Cargar BD*/
            apiServer.CargarBd();



            //iniciazamos la ventana principal de acceso
            //modulo de acceso
            Application.Run(new WFSeleccionarUsuario());


            // Detener el servidor cuando la aplicacion cierre
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

        private static void InicializarApi()
        {
            //Instanciamos y arrancamos el servidor
            apiServer = new ApiServer();
            apiServer.Start();
            
        }
    }
}