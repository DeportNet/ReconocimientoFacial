using DeportNetReconocimiento.Api;
using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Services;
using DeportNetReconocimiento.GUI;
using DeportNetReconocimiento.Utils;
using Microsoft.EntityFrameworkCore;
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
            //InicializarApi();

            //CredencialesUtils.EscribirCredencialesBd(new Credenciales("192.168.1.10", "8080", "admin", "123456", "23", "H7gVA3r89jvaMuDd"));


            using (var context = BdContext.CrearContexto())
            {
                Console.WriteLine("Entro aca");
                context.Database.EnsureCreated();
            }

            /*Cargar BD*/

           apiServer.CargarBd();
            


            //iniciazamos la ventana principal de acceso
            Application.Run(new WFSeleccionarUsuario());


            // Detener el servidor cuando la aplicaci�n cierre
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