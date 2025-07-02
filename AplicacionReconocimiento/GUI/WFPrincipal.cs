using DeportnetOffline;
using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Response;
using DeportNetReconocimiento.Api.Services;
using DeportNetReconocimiento.Hikvision.SDKHikvision;
using DeportNetReconocimiento.Properties;
using DeportNetReconocimiento.Utils;
using DeportNetReconocimiento.Utils.Modelo;
using System.Runtime.InteropServices;
using Serilog;
using System.Windows.Forms;


namespace DeportNetReconocimiento.GUI
{
    public partial class WFPrincipal : Form
    {
        private static WFPrincipal? instancia;
        private ConfiguracionEstilos configuracionEstilos;
        private bool ignorarCierre = false;
        private bool conexionInternet = true;
        private static ReproductorSonidos reproductorSonidos;
        private bool ocultarPrincipal = false;
        private static int intentosConexionADispositivo = 0;
        private bool ObligarCerrarPrograma = false;
        private bool buscandoIp = false;
        private bool verificandoEstado = false;

        private WFPrincipal()
        {
            InitializeComponent();
            Hik_Resultado resultadoInicio = InstanciarPrograma(); //Instanciamos el programa con los datos de la camara
            DispositivoEnUsoUtils.Desocupar();

            //estilos se leen de un archivo
            AplicarConfiguracion(ConfiguracionEstilos.LeerJsonConfiguracion());
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

        public Hik_Resultado InstanciarPrograma()
        {

            Hik_Resultado resultado = new Hik_Resultado();

            //ip , puerto, usuario, contraseña en ese orden
            Credenciales? credenciales = CredencialesUtils.LeerCredencialesBd();

            if (credenciales == null)
            {
                resultado.ActualizarResultado(false, "No se pudieron leer las credenciales... Vuelva a intentarlo", "-1");
                return resultado;
            }

            resultado = Hik_Controladora_General.Instancia.InicializarPrograma(credenciales.Username, credenciales.Password, credenciales.Port, credenciales.Ip);

            if (resultado.Exito)
            {
                //una vez exitosa la conexion con el dispositivo, iniciamos el timer, para verificar la conexion con el disp                
                timerConexion.Enabled = true;
            }
            else
            {
                ManejarErrorDispositivo(resultado);
            }


            return resultado;
        }

        private async void ManejarErrorDispositivo(Hik_Resultado resultadoError)
        {

            ConfiguracionEstilos = ConfiguracionEstilos.LeerJsonConfiguracion();

            switch (resultadoError.Codigo)
            {
                case "7":

                    if (ConfiguracionEstilos.BloquearIp)
                    {
                        Log.Error("No se va a buscar nuevas Ips debido a que la configuracion de bloquear IP esta activa. No se pudo conectar con el dispositivo, verifique si la ip es correcta o si el dispositivo esta conectado a la red.");
                        return;
                    }

                    if (!buscandoIp && intentosConexionADispositivo <= 2)
                    {
                        timerConexion.Stop();
                        buscandoIp = true;
                        //Logica mostrar loading y buscar ip
                        Credenciales? credenciales = CredencialesUtils.LeerCredencialesBd();

                        ocultarPrincipal = true; // Ocultamos la vista pri para que no se pueda hacer nada mientras se busca la ip del dispositivo

                        Loading loading = new Loading();

                        loading.Show();
                        Hik_Resultado resultadoLogin = await Task.Run(() => BuscadorIpDispositivo.ObtenerIpDispositivo(credenciales.Port, credenciales.Username, credenciales.Password));
                        loading.Close();

                        this.Visible = true;
                        ocultarPrincipal = false;


                        if (!resultadoLogin.Exito)
                        {
                            //va a mostrar no se encontro la ip
                            Log.Error("No se encontro ninguna IP de Hikvision");
                            buscandoIp = false;
                            timerConexion.Start();
                            return;
                        }

                        credenciales.Ip = resultadoLogin.Mensaje; //El resultado de la busqueda de ip es el mensaje, que es la ip del dispositivo
                        CredencialesUtils.EscribirCredencialesBd(credenciales);
                        MessageBox.Show("Se busco la direccion del dispositivo y se configuro con la correspondiente", "Aviso busqueda de Ip dispositivo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        buscandoIp = false;
                        timerConexion.Start();
                    }

                    break;
                default:

                    Log.Error($"Error al inicializar el programa  Exito: {resultadoError.Exito} Código: {resultadoError.Codigo} Mensaje: {resultadoError.Mensaje} ");
                    ActualizarTextoHeaderLabel("Error De conexión con el dispositivo, verifique la conexión", Color.Red);
                    break;

            }




        }

        private void CerrarFormulario(object sender, FormClosingEventArgs e)
        {
            if (!ignorarCierre)
            {

                DialogResult result = MessageBox.Show("Deportnet dice:\n¿Estás seguro de que quieres cerrar la aplicación de reconocimiento facial?",
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

        //Funcion que se ejecuta en cada TICK del timer
        public async void VerificarEstadoGeneralAsync(object sender, EventArgs e)
        {

            if (verificandoEstado)
                return;

            if (!DispositivoEnUsoUtils.EstaLibre())
                return;

            verificandoEstado = true;

            try
            {
                VerificarConexionInternet();
                VerificarConexionConDispositivo();
            }
            catch (Exception ex)
            {
                Log.Error($"Error en VerificarEstadoDispositivoAsync: {ex.Message}");
            }
            finally
            {
                verificandoEstado = false;
            }

        }

        //intentamos volver a conectarnos

        public async Task VerificarConexionInternet()
        {
            int cantMaxIntentos = 2;

            ConexionInternet = VerificarConexionInternetUtils.Instancia.ComprobarConexionInternet();

            int nroIntentos = VerificarConexionInternetUtils.Instancia.IntentosVelocidadInternet;

            //si tenemos conexion a internet y el panel de conexion esta visible, lo ocultamos
            if (ConexionInternet && PanelSinConexion.Visible == true)
            {
                PanelSinConexion.Visible = false;
                return;
            }

            //si no hay internet, levantamos un panel de offline
            if (!ConexionInternet || nroIntentos >= cantMaxIntentos)
            {

                if (PanelSinConexion.Visible == false)
                {
                    PanelSinConexion.Visible = true;
                }

            }

        }

        public async Task VerificarConexionConDispositivo()
        {
            Hik_Resultado resultadoInstanciar = new Hik_Resultado();

            //Se espera al resultado de la función verificarEstadoDispositivo 
            bool estadoConexionDispositivo = await Task.Run(() => Hik_Controladora_General.Instancia.VerificarEstadoDispositivo());


            //Log.Information("Verificamos el estado de la conexion con el dispositivo. Estado: " + estadoConexionDispositivo);

            //si tenemos conexion con el dispositivo
            if (estadoConexionDispositivo)
            {
                return;
            }

            //si NO hay conexion intentamos volver a conectarnos
            Log.Warning($"No hay conexion con el dispositivo(Estado: {estadoConexionDispositivo}), intentamos reinstanciar programa. nro de intentos: {intentosConexionADispositivo}.");

            resultadoInstanciar = InstanciarPrograma();

            //si el resultado no tuvo exito 
            if (!resultadoInstanciar.Exito)
            {
                intentosConexionADispositivo++;
                ActualizarTextoHeaderLabel($"Intentos de conexión al dispositivo {intentosConexionADispositivo}", Color.Red);
                Log.Error($"Intento de conexión al dispositivo n° {intentosConexionADispositivo}");

            }
            else
            {
                //si hubo conexion exitosa, reiniciamos el contador y volvemos a iniciar el timer si estaba apagado
                Log.Information("Hubo conexion exitosa con el dispositivo, reiniciamos el contador y volvemos a iniciar el timer si estaba apagado");
                intentosConexionADispositivo = 0;

                Hik_Controladora_Eventos.InstanciaControladoraEventos.InstanciarMsgCallback();
                ActualizarTextoHeaderLabel(ConfiguracionEstilos.LeerJsonConfiguracion().MensajeBienvenida, ConfiguracionEstilos.LeerJsonConfiguracion().ColorFondoMensajeBienvenida);
                ReactivarTimer();
            }


        }


        public void VerificarPanelAlmacenamiento()
        {
            Hik_Resultado? hayAlmacenamiento = VerificarAlmacenamientoUtils.VerificarHayAlmacenamiento();


            if (hayAlmacenamiento == null)
            {
                Console.WriteLine("No se pudo verificar el almacenamiento en WfPrincipal.");
                return;
            }

            //cambiamos los colores del mensaje, dependiendo si supero o no la alerta de almacenamiento
            if (hayAlmacenamiento.Exito)
            {
                TextoAlmacenamiento.ForeColor = Color.Green;
            }
            else
            {
                TextoAlmacenamiento.ForeColor = Color.Red;
            }


            TextoAlmacenamiento.Text = hayAlmacenamiento.Mensaje;



        }

        //Codigo para identificar un hilo secundario, se utiliza en ActualizarDatos
        private CancellationTokenSource tokenCancelarTiempoMuestraDeDatos = new CancellationTokenSource();


        private CancellationTokenSource CancelarTokenYGenerarNuevoHilos(CancellationTokenSource tokenSource)
        {
            //token para limpiar interfaz
            tokenSource.Cancel(); // Cancelar cualquier tarea previa
            return new CancellationTokenSource(); // Crear un nuevo token

        }

        //función para actualizar los datos en el hilo principal
        public async void ActualizarDatos(ValidarAccesoResponse json)
        {
            CancellationTokenSource tokenDeCancelacion = CancelarTokenYGenerarNuevoHilos(tokenCancelarTiempoMuestraDeDatos);
            int nroLector = 1;

            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<ValidarAccesoResponse>(ActualizarDatos), json);
                    return;
                }

                LimpiarInterfaz();

                pictureBox1.Image = ObtenerFotoCliente(nroLector, json.IdCliente);

                EvaluarMensajeAcceso(json);

                AnalizarMinimizarVentana();

                //tiempo de muestra de datos
                await Task.Delay((int)(ConfiguracionEstilos.TiempoDeMuestraDeDatos * 1000), tokenDeCancelacion.Token);
                LimpiarInterfaz();
            }
            catch (TaskCanceledException)
            {
                // Ignorar si la tarea fue cancelada
                Console.WriteLine("Limpiar interfaz cancelada. Hubo otra lectura.");
            }
        }

        public async void ActualizarTextoHeaderLabel(string texto, Color color)
        {
            CancellationTokenSource tokenDeCancelacion = CancelarTokenYGenerarNuevoHilos(tokenCancelarTiempoMuestraDeDatos);
            int nroLector = 1;

            try
            {
                if (InvokeRequired)
                {
                    Invoke(ActualizarTextoHeaderLabel, texto);
                    return;
                }

                LimpiarInterfaz();

                HeaderLabel.Text = texto;
                HeaderLabel.ForeColor = color;

                //tiempo de muestra de datos
                await Task.Delay((int)(ConfiguracionEstilos.TiempoDeMuestraDeDatos * 1000), tokenDeCancelacion.Token);
            }
            catch (TaskCanceledException)
            {
                // Ignorar si la tarea fue cancelada
                Console.WriteLine("Limpiar interfaz cancelada. Hubo otra lectura.");
            }
        }


        public void EvaluarMensajeAcceso(ValidarAccesoResponse json)
        {
            string titulo = "";
            string mensaje = "";
            Hik_Resultado resultado = new Hik_Resultado();

            AnalizarMaximizarVentana(json.Estado);

            switch (json.Estado)
            {
                case "U":
                    titulo = "Usuario no registrado en Deportnet";
                    mensaje = ConvertidorTextoUtils.LimpiarTextoHtml(json.MensajeAcceso);
                    break;
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
                        Hik_Controladora_Puertas.EjecutarExe(ConfiguracionEstilos.RutaMetodoApertura);
                    }
                    else if (ConfiguracionEstilos.MetodoApertura == "Hikvision")
                    {
                        Console.WriteLine("Abro con Hikvision");
                        resultado = Hik_Controladora_Puertas.OperadorPuerta(1);
                        Log.Information("Resultado de apertura con Hikvision: " + resultado.Exito);
                    }



                    titulo = "Bienvenido/a " + ConvertidorTextoUtils.PrimerLetraMayuscula(json.Nombre) + " " + ConvertidorTextoUtils.PrimerLetraMayuscula(json.Apellido);
                    mensaje = ConvertidorTextoUtils.LimpiarTextoHtml(json.MensajeAcceso);


                    break;
                case "F":
                    ReproducirSonido(ConfiguracionEstilos.AccesoDenegado);
                    HeaderLabel.ForeColor = ConfiguracionEstilos.ColorMensajeAccesoDenegado;

                    titulo = "Acceso denegado " + ConvertidorTextoUtils.PrimerLetraMayuscula(json.Nombre) + " " + ConvertidorTextoUtils.PrimerLetraMayuscula(json.Apellido);
                    mensaje = ConvertidorTextoUtils.LimpiarTextoHtml(json.MensajeAcceso);

                    break;
            }


            HeaderLabel.Text = titulo;
            textoInformacionCliente.Text = mensaje;

            if (json.Estado != "Q")
            {
                //Fin evento de acceso, por lo tanto lo desocupo
                DispositivoEnUsoUtils.Desocupar();
            }
        }


        public void ReactivarTimer()
        {
            if (timerConexion == null)
            {
                int intervalo = 20000;//20 segundos
                timerConexion = new System.Windows.Forms.Timer();
                timerConexion.Interval = intervalo;
                timerConexion.Tick += VerificarEstadoGeneralAsync;
                Log.Information("Se crea un timer que verifica la conexión con el dispositivo cada 20 segundos");

            }

            if (!timerConexion.Enabled)
            {
                Log.Information("Iniciamos el timer que verifica la conexion con el disp.");
                timerConexion.Start();
            }

        }

        private void CalcularPosicion(HTMLMessageBox popupPregunta)
        {
            //Ajustar la posicón para que no tape la imagen 
            int x, y;
            x = this.Right - ObtenerInstancia.Width + (this.Width / 3); // 33% desde el borde derecho del formulario
            y = 280;
            popupPregunta.Location = new Point(x, y);
        }


        // Método que maneja la respuesta del formulario
        public async void OnProcesarRespuesta(RespuestaAccesoManual response)
        {
            string? idEmpleado = CredencialesUtils.LeerCredencialEspecifica(6);

            string mensaje = await WebServicesDeportnet.ControlDeAcceso(response.MemberId, response.ActiveBranchId, response.IsSuccessful, idEmpleado, ConfiguracionGeneralUtils.ObtenerLectorActual());
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

        public Image ObtenerFotoCliente(int nroLector, string idCliente)
        {
            Image imagen = Resources.avatarPredeterminado;
            //Se obtiene la foto del cliente
            Hik_Resultado resultado = Hik_Controladora_Facial.Instancia.ObtenerCara(nroLector, idCliente);


            if (resultado.Exito)
            {
                try
                {
                    string ruta = Path.Combine(Directory.GetCurrentDirectory(), "FacePicture.jpg");
                    using (FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.Read))
                    {
                        imagen = Image.FromStream(fs);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("No se pudo obtener foto cliente: " + ex.Message);
                    imagen = Resources.avatarPredeterminado;
                }
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
            if (sonido == null || string.IsNullOrEmpty(sonido.RutaArchivo) || !sonido.Estado)
            {
                return;
            }
            Console.WriteLine("Reproducimos sonido");

            ReproductorSonidos.InstanciaReproductorSonidos.ReproducirSonido(sonido);

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

        /* - - - - - - Maximizar Minimizar Ventana - - - - - - */

        public void AnalizarMaximizarVentana(string estado)
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


        public void MaximizarVentana()
        {
            if (this.WindowState != FormWindowState.Maximized)
            {
                this.Show(); // Muestra el formulario principal
                this.WindowState = FormWindowState.Maximized; // Restaura el estado de la ventana
            }

            this.TopMost = true;
            this.BringToFront();
            this.Activate();
            this.TopMost = false;
        }

        private CancellationTokenSource tokenCancelarTimerMinimizar = new CancellationTokenSource();
        public async void AnalizarMinimizarVentana()
        {

            if (configuracionEstilos.EstadoMinimizar)
            {
                try
                {
                    CancellationTokenSource tokenDeCancelacion = CancelarTokenYGenerarNuevoHilos(tokenCancelarTimerMinimizar);

                    await Task.Delay((int)(ConfiguracionEstilos.SegundosMinimizar * 1000), tokenDeCancelacion.Token);

                    MinimizarVentana();

                }
                catch (TaskCanceledException ex)
                {
                    Console.WriteLine("Se cancelo el timer minimizar: " + ex.Message);
                }
            }

        }

        public void MinimizarVentana()
        {

            if (InvokeRequired)
            {
                Invoke(new Action(MinimizarVentana)); //Invocamos el metodo en el hilo principal
            }

            if (this.WindowState != FormWindowState.Minimized)
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

            VerificarPanelAlmacenamiento();
        }
        private void botonPersonalizar_Click(object sender, EventArgs e)
        {

            WFConfiguracion wFConfiguracion = new WFConfiguracion(ConfiguracionEstilos, this);

            wFConfiguracion.ShowDialog();
        }

        private void textoInformacionCliente_Click(object sender, EventArgs e)
        {

        }

        private void botonDeportnetOffline_Click(object sender, EventArgs e)
        {
            WFDeportnetOffline wFDeportnetOffline = WFDeportnetOffline.ObtenerInstancia;

            if (wFDeportnetOffline.Visible)
            {
                Console.WriteLine("Esta visible");
                wFDeportnetOffline.BringToFront();
                return;
            }


            Console.WriteLine("No está visible");
            ConfiguracionGeneralUtils.CambiarEstadoModuloActivo();
            wFDeportnetOffline.Show();

        }
    }
}
