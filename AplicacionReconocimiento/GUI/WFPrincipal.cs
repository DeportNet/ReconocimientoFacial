using DeportNetReconocimiento.Api.Dtos.Response;
using DeportNetReconocimiento.Api.Services;
using DeportNetReconocimiento.Modelo;
using DeportNetReconocimiento.Properties;
using DeportNetReconocimiento.SDK;
using DeportNetReconocimiento.SDKHikvision;
using DeportNetReconocimiento.Utils;
using System.Timers;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace DeportNetReconocimiento.GUI
{
    public partial class WFPrincipal : Form
    {
        private static Hik_Controladora_General? hik_Controladora_General;


        private static WFPrincipal? instancia;
        private ConfiguracionEstilos configuracionEstilos;
        private bool ignorarCierre = false;
        private bool conexionInternet = true;
        private System.Timers.Timer timerMinimizar;


        private WFPrincipal()
        {
            InitializeComponent();
            
            //estilos se leen de un archivo
            InstanciarPrograma(); //Instanciamos el programa con los datos de la camara
            timerConexion.Enabled = true; //una vez exitosa la conexion con el dispositivo, iniciamos el timer, para verificar la conexion con el disp.
            AplicarConfiguracion(ConfiguracionEstilos.LeerJsonConfiguracion("configuracionEstilos"));

            ReproducirSonido(ConfiguracionEstilos.SonidoBienvenida);

        }

        //propiedades

        public bool ConexionInternet
        {
            get => conexionInternet;
            set => conexionInternet = value;
        }

        public ConfiguracionEstilos ConfiguracionEstilos
        {
            get { return configuracionEstilos; }
            set { configuracionEstilos = value; }
        }

        public static WFPrincipal ObtenerInstancia
        {

            get
            {
                if (instancia == null)
                {
                    instancia = new WFPrincipal();
                }
                return instancia;

            }
        }

        public Hik_Controladora_General? Instancia_Controladora_General
        {
            get {
                if (instancia == null)
                {
                    hik_Controladora_General = Hik_Controladora_General.InstanciaControladoraGeneral;
                }
                return hik_Controladora_General; 
            }
            set { hik_Controladora_General = value; }
        }

        public Hik_Resultado InstanciarPrograma()
        {

            Hik_Resultado resultado = new Hik_Resultado();

            //ip , puerto, usuario, contraseña en ese orden
            string[] credenciales = CredencialesUtils.LeerCredenciales();

            
            if (credenciales.Length == 0)
            {
                //Si no hubo exito mostrar ventana con el error. Un modal 
                resultado.ActualizarResultado(false, "No se pudieron leer las credenciales... Vuelva a intentarlo", "-1");
                resultado.MessageBoxResultado("Error al leer las credenciales");
                //return resultado;
            }


            if (credenciales.Length > 0)
            {
                Instancia_Controladora_General = Hik_Controladora_General.InstanciaControladoraGeneral;
                resultado = Instancia_Controladora_General.InicializarPrograma(credenciales[2], credenciales[3], credenciales[1], credenciales[0]);

                if (resultado.Exito == false)
                {
                    resultado.MessageBoxResultado("Error al inicializar el programa");

                }
            }

            return resultado;
        }

        private void CerrarFormulario(object sender, FormClosingEventArgs e)
        {
            if (!ignorarCierre)
            {

                var result = MessageBox.Show("Deportnet dice:\n¿Estás seguro de que quieres cerrar la aplicación de reconocimiento facial?",
                                             "Confirmación",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Cerrar completamente la aplicación
                    Environment.Exit(0);
                }
                else
                {
                    // Cancelar el cierre
                    e.Cancel = true;
                }
            }
        }




        //función que verifica si el programa tiene conexión con el dispositivo
        public bool VerificarEstadoDispositivo()
        {
            IntPtr pInBuf;
            Int32 nSize;
            int iLastErr = 17;

            pInBuf = IntPtr.Zero;
            nSize = 0;

            int XML_ABILITY_OUT_LEN = 3 * 1024 * 1024;
            IntPtr pOutBuf = Marshal.AllocHGlobal(XML_ABILITY_OUT_LEN);

            if (!Hik_SDK.NET_DVR_GetDeviceAbility(Hik_Controladora_General.InstanciaControladoraGeneral.IdUsuario, 0, pInBuf, (uint)nSize, pOutBuf, (uint)XML_ABILITY_OUT_LEN))
            {
                iLastErr = (int)Hik_SDK.NET_DVR_GetLastError();

                //si perdio conexión
                if (iLastErr == 17)
                {
                    Console.WriteLine("Se perdio la conexion con el dispositivo");
                    return false;
                }

            }

            Marshal.FreeHGlobal(pInBuf);
            Marshal.FreeHGlobal(pOutBuf);

            if (iLastErr == 1000)
            {
                Console.WriteLine("Conectado");
                return true;
            }
            else
                Console.WriteLine("Desconectado");
            return false;
        }

        

        //Se crea un objeto de tipo Task para que la función se ejecute en un hilo distinto al principal
        //Se usa async await para manejar la asincronía 
        public async void VerificarEstadoDispositivoAsync(object sender, EventArgs e)
        {
            VerificarConexionInternet();

            //Se espera al resultado de la función verificarEstadoDispositivo 
            //Mientras se pone a correr un hilo secundario para que no se bloquee el hilo principal
            bool estado = await Task.Run(() => VerificarEstadoDispositivo());

            Console.WriteLine("Verificamos el estado del dispositivo. Estado: " + estado);

            if (!estado)
            {
                InstanciarPrograma();
            }


        }

        public void VerificarConexionInternet()
        {
            //verificamos y asignamos la conexion a internet
            ConexionInternet = Hik_Controladora_General.ComprobarConexionInternet();

            //si no hay internet, levantamos un panel de offline
            if (!ConexionInternet)
            {

                if (PanelSinConexion.Visible == false)
                {
                    PanelSinConexion.Visible = true;

                }

            }
            else if (PanelSinConexion.Visible == true)
            {
                PanelSinConexion.Visible = false;
            }
        }

        public void VerificarAlmacenamiento()
        {

            int capacidadMaxima = configuracionEstilos.CapacidadMaximaDispositivo;
            int carasActuales = configuracionEstilos.CarasRegistradas;
            float porcentaje = configuracionEstilos.PorcentajeAlertaCapacidad;

            float porcentajeActual = (carasActuales * 100) / capacidadMaxima;

            if (porcentajeActual > porcentaje && PanelAlmacenamiento.Visible == false)
            {

                TextoAlmacenamiento.Text = $"- Capacidad al: {porcentajeActual}%     - Socios: {carasActuales}/{capacidadMaxima}";
                PanelAlmacenamiento.Visible = true;
            }
            else if (porcentajeActual < porcentaje && PanelAlmacenamiento.Visible == true)
            {
                PanelAlmacenamiento.Visible = false;
            }
        }

        //Codigo para identificar un hilo secundario, se utiliza en ActualizarDatos
        private CancellationTokenSource _cts = new CancellationTokenSource();


        //función para actualizar los datos en el hilo principal
        public async void ActualizarDatos(ValidarAccesoResponse json)
        {
            _cts.Cancel(); // Cancelar cualquier tarea previa
            _cts = new CancellationTokenSource(); // Crear un nuevo token
            CancellationToken token = _cts.Token;

            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<ValidarAccesoResponse>(ActualizarDatos), json);
                    return;
                }

                LimpiarInterfaz();

                pictureBox1.Image = ObtenerFotoCliente(1, json.IdCliente);

                EvaluarMensajeAcceso(json);
                AnalizarMinimizarVentana();
                ConservarImagenSocio(json.NombreCompleto);


                await Task.Delay((int)(ConfiguracionEstilos.TiempoDeMuestraDeDatos * 1000), token);
                LimpiarInterfaz();
            }
            catch (TaskCanceledException)
            {
                // Ignorar si la tarea fue cancelada
                Console.WriteLine("Limpiar interfaz cancelada. Hubo otra lectura.");
            }
        }



        public void ConservarImagenSocio(string nombreCompletoSocio)
        {
            string rutaOriginal = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "captura.jpg");
            string rutaNueva = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FotosSocios");    

            if (ConfiguracionEstilos.AlmacenarFotoSocio)
            {

                //Si no existe el directorio, lo creo 
                if (Directory.Exists(rutaNueva))
                {
                    Directory.CreateDirectory(rutaNueva);
                }

                //Configuro el nombre de la foto

                string nuevoNombre = CambiarNombreFoto(nombreCompletoSocio);
                string rutaDestino = Path.Combine(rutaNueva, nuevoNombre);

                //Hago la copia de un directorio a otro
                try
                {
                    File.Copy(rutaOriginal, rutaDestino, overwrite: true);
                }
                catch(Exception ex) 
                { 
                    Console.WriteLine(ex.ToString()); 
                }
            }
        }

        private string CambiarNombreFoto(string nombreCompletoSocio)
        {
            return Regex.Replace(nombreCompletoSocio, " ", "_") + ".jpg";
        }


        public void EvaluarMensajeAcceso(ValidarAccesoResponse json)
        {
            string titulo = "";
            string mensaje = "";


            //Console.WriteLine("Estado json:" + json.Estado);
            AnalizarMaximizarVentana(json.Estado);


            switch (json.Estado)
            {
                case "Q":

                    
                    ReproducirSonido(ConfiguracionEstilos.SonidoPregunta);

                    // Crear y mostrar el formulario HTMLMessageBox
                    HTMLMessageBox popupPregunta = new HTMLMessageBox(json);

                    CalcularPosicion(popupPregunta);

                    // Suscribir al evento para recibir la respuesta
                    popupPregunta.OpcionSeleccionada += OnProcesarRespuesta; //Este evento maneja las peticiones 

                    popupPregunta.ShowDialog();

                    break;
                case "T":
                    
                    ReproducirSonido(ConfiguracionEstilos.AccesoConcedido);
                    HeaderLabel.ForeColor = ConfiguracionEstilos.ColorMensajeAccesoConcedido;

                    if (ConfiguracionEstilos.MetodoApertura == ".exe")
                    {
                        Console.WriteLine("Ejecuto el exe");
                        Hik_Controladora_Puertas.EjecutarExe(ConfiguracionEstilos.RutaMetodoApertura);
                    }

                    titulo = "Bienvenido/a " + ConvertidorTextoUtils.PrimerLetraMayuscula(json.Nombre); 
                    mensaje = ConvertidorTextoUtils.LimpiarTextoHtml(json.MensajeAcceso);


                    break;
                case "F":
                    ReproducirSonido(ConfiguracionEstilos.AccesoDenegado);
                    HeaderLabel.ForeColor = ConfiguracionEstilos.ColorMensajeAccesoDenegado;

                    titulo = "Acceso denegado " + ConvertidorTextoUtils.PrimerLetraMayuscula(json.Nombre);
                    mensaje = ConvertidorTextoUtils.LimpiarTextoHtml(json.MensajeAcceso);

                    break;
            }

            HeaderLabel.Text = titulo;
            textoInformacionCliente.Text = mensaje;
        }






        private void CalcularPosicion(HTMLMessageBox popupPregunta)
        {
            //Ajustar la posicón para que no tape la imagen 
            int x, y;
            x = this.Right - instancia.Width + (this.Width / 3); // 33% desde el borde derecho del formulario
            y = 280;
            popupPregunta.Location = new Point(x, y);
        }


        // Método que maneja la respuesta del formulario
        public async void OnProcesarRespuesta(RespuestaAccesoManual response)
        {

            string mensaje = await WebServicesDeportnet.ControlDeAcceso(response.MemberId, response.ActiveBranchId, response.IsSuccessful);
            Console.WriteLine("Mensaje pregunta: " + mensaje);

            Hik_Controladora_Eventos.ProcesarRespuestaAcceso(mensaje, response.MemberId, response.ActiveBranchId);
        }


        public void LimpiarPictureBox()
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
        }

        Image ObtenerFotoCliente(int nroLector, string idCliente)
        {
            Image imagen = null;
            //Se obtiene la foto del cliente
            Hik_Resultado resultado = Hik_Controladora_Facial.ObtenerInstancia.ObtenerCara(nroLector, idCliente);

            if (resultado.Exito)
            {
                string ruta = Path.Combine(Directory.GetCurrentDirectory(), "FacePicture.jpg");
                imagen = Image.FromFile(ruta);
            }
            else
            {
                imagen = Resources.avatarPredeterminado;
            }


            return imagen;
        }

        Image CapturarFotoCliente()
        {
            Image imagen = null;

            Hik_Resultado resultado = Hik_Controladora_Facial.ObtenerInstancia.CapturarCara();
            if (resultado.Exito)
            {
                String ruta = Path.Combine(Directory.GetCurrentDirectory(), "captura.jpg");
                imagen = Image.FromFile(ruta);
            }
            return imagen;
        }


        public async void LimpiarInterfaz()
        {
            if (InvokeRequired)
            {
                Invoke(LimpiarInterfaz);
                return;
            }

            HeaderLabel.Text = configuracionEstilos.MensajeBienvenida;
            HeaderLabel.ForeColor = configuracionEstilos.ColorMensajeBienvenida;
            textoInformacionCliente.Text = "";

            LimpiarPictureBox();

            LimpiarFotosDirectorio();

        }


        public void LimpiarFotosDirectorio()
        {
            String rutaCapturaCara = Path.Combine(Directory.GetCurrentDirectory(), "captura.jpg");
            String rutaCaraAlmacenadaEnDispositivo = Path.Combine(Directory.GetCurrentDirectory(), "FacePicture.jpg");

            File.Delete(rutaCapturaCara);
            File.Delete(rutaCaraAlmacenadaEnDispositivo);

        }




        /* - - - - - - Sonidos - - - - - - */

        public void ReproducirSonido(Sonido sonido)
        {
            if (sonido == null)
            {
                return;
            }

            ReproductorSonidos reproductor = new ReproductorSonidos();
            reproductor.ReproducirSonido(sonido);

        }


        /* - - - - - - Notify Icon / Tray - - - - - - */

        private void trayReconocimiento_MouseClick(object sender, MouseEventArgs e)
        {
            // Restaurar la ventana al hacer doble clic en el icono
            if (e.Button == MouseButtons.Right)
            {
                menuNotifyIcon.Show(Cursor.Position);
            }
            else if (e.Button == MouseButtons.Left)
            {
                this.Show();
                this.WindowState = FormWindowState.Maximized;
            }

        }

        private void WFPrincipal_Resize(object sender, EventArgs e)
        {
            // Si la ventana se minimiza
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide(); // Ocultar la ventana principal

                trayReconocimiento.Visible = true; // Asegurar que el ícono esté visible

            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                trayReconocimiento.Visible = false; // Ocultar el ícono si la ventana está restaurada
            }

        }

        private void ClickAbrirMenuNotifyIcon(object sender, EventArgs e)
        {

            this.Show(); // Muestra el formulario principal
            this.WindowState = FormWindowState.Maximized; // Restaura el estado de la ventana


        }

        private void ClickCerrarMenuNotifyIcon(object sender, EventArgs e)
        {

            Application.Exit(); // Cierra la aplicación


        }

        public void AnalizarMaximizarVentana(string estado)
        {
                switch (estado)
                {
                    case "T":
                    if (configuracionEstilos.MaximizarAccesoConcedidio)
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


        public void MaximizarVentana()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show(); // Muestra el formulario principal
                this.WindowState = FormWindowState.Maximized; // Restaura el estado de la ventana
            }
        }


        public void AnalizarMinimizarVentana()
        {
            if (configuracionEstilos.EstadoMinimizar)
            {
                if( timerMinimizar != null)
                {
                    timerMinimizar.Stop();
                    timerMinimizar.Dispose();
                }

                timerMinimizar = new System.Timers.Timer(configuracionEstilos.SegundosMinimizar * 1000);
                timerMinimizar.Elapsed += (sender, args) =>
                {
                    timerMinimizar.Stop();
                    MinimizarVentana();
                };
                timerMinimizar.AutoReset = false; // Ejecuta solo una vez
                timerMinimizar.Start();

            }
        }

        public void MinimizarVentana()
        {
            if(this.WindowState == FormWindowState.Maximized)
            {
                this.Hide();
                this.WindowState = FormWindowState.Minimized;
            }
        }


        /* - - - - - - Configuracion Estilos - - - - - - */


        public void AplicarConfiguracion(ConfiguracionEstilos config)
        {

            ConfiguracionEstilos = config;

            //header Colores
            BackColor = config.ColorFondo;
            HeaderLabel.BackColor = config.ColorFondoMensajeBienvenida;
            HeaderLabel.ForeColor = config.ColorMensajeBienvenida;

            //Header Texto
            HeaderLabel.Text = config.MensajeBienvenida;
            HeaderLabel.Font = config.FuenteTextoMensajeAcceso;

            //Información Cliente Colores
            textoInformacionCliente.ForeColor = config.ColorTextoInformacionCliente;
            textoInformacionCliente.BackColor = config.ColorFondoInformacionCliente;

            //Información Cliente Fuente
            textoInformacionCliente.Font = config.FuenteTextoInformacionCliente;
            textoInformacionCliente.Cursor = Cursors.Arrow;

            //Logo
            imagenLogo.BackColor = config.ColorFondoLogo;
            imagenLogo.Image = config.Logo;

            //Foto
            pictureBox1.BackColor = config.ColorFondoImagen;

            VerificarAlmacenamiento();
        }
        private void botonPersonalizar_Click(object sender, EventArgs e)
        {

            WFConfiguracion wFConfiguracion = new WFConfiguracion(ConfiguracionEstilos, this);

            wFConfiguracion.ShowDialog();
        }
    }
}
