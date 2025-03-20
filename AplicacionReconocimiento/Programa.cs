using DeportNetReconocimiento.Api;
using DeportNetReconocimiento.GUI;


namespace DeportNetReconocimiento
{
    internal class Programa
    {
        private static ApiServer apiServer;

        [STAThread]
        static void Main(string[] args)
        {
            

            /*API*/
            InicializarApi();

            /*Cargar BD*/


            //iniciazamos la ventana principal de acceso
            Application.Run(WFPrincipal.ObtenerInstancia);

           
            // Detener el servidor cuando la aplicación cierre
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