using DeportNetReconocimiento.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Utils
{

    public  class ManejarVentanaUtils
    {

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public const int SW_SHOW = 5;
        public const int SW_RESTORE = 9;
       public static ConfiguracionEstilos configuracionEstilos = ConfiguracionEstilos.LeerJsonConfiguracion();

        private CancellationTokenSource tokenCancelarTiempoMuestraDeDatos = new CancellationTokenSource();


        private static CancellationTokenSource CancelarTokenYGenerarNuevoHilos(CancellationTokenSource tokenSource)
        {
            //token para limpiar interfaz
            tokenSource.Cancel(); // Cancelar cualquier tarea previa
            return new CancellationTokenSource(); // Crear un nuevo token

        }  

        public static void AnalizarMaximizarVentana(string estado)
        {
            switch (estado)
            {
                case "T":
                    if (configuracionEstilos.MaximizarAccesoConcedido)
                        MaximizarVentana();
                    break;
                case "F":
                    if (configuracionEstilos.MaximizarAccesoDenegado)
                        MaximizarVentana();
                    break;
                case "Q":
                    if (configuracionEstilos.MaximizarPregunta)
                        MaximizarVentana();
                    break;
            }

        }


        public static void MaximizarVentana()
        {
            var handle = WFPrincipal.ObtenerInstancia.Handle; // "this" es tu formulario

            // Restaurar y mostrar si está minimizado u oculto
            ShowWindow(handle,SW_RESTORE);

            // Traer al frente del resto de las ventanas
            SetForegroundWindow(handle);

            // Luego la maximizás
            WFPrincipal.ObtenerInstancia.WindowState = FormWindowState.Maximized;
        }

        private static CancellationTokenSource tokenCancelarTimerMinimizar = new CancellationTokenSource();
        public static async void AnalizarMinimizarVentana()
        {

            if (configuracionEstilos.EstadoMinimizar)
            {
                try
                {
                    CancellationTokenSource tokenDeCancelacion = CancelarTokenYGenerarNuevoHilos(tokenCancelarTimerMinimizar);

                    await Task.Delay((int)(configuracionEstilos.SegundosMinimizar * 1000), tokenDeCancelacion.Token);

                    MinimizarVentana();

                }
                catch (TaskCanceledException ex)
                {
                    Console.WriteLine("Se cancelo el timer minimizar");
                }
            }

        }

        public static void MinimizarVentana()
        {

            if (WFPrincipal.ObtenerInstancia.InvokeRequired)
            {
                WFPrincipal.ObtenerInstancia.Invoke(new Action(MinimizarVentana)); //Invocamos el metodo en el hilo principal
            }

            if (WFPrincipal.ObtenerInstancia.WindowState == FormWindowState.Maximized)
            {
                WFPrincipal.ObtenerInstancia.Hide();
                WFPrincipal.ObtenerInstancia.WindowState = FormWindowState.Minimized;
            }
        }
    }
}
