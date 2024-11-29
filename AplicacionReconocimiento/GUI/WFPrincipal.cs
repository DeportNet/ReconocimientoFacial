using DeportNetReconocimiento.Modelo;
using DeportNetReconocimiento.SDK;
using DeportNetReconocimiento.SDKHikvision;
using DeportNetReconocimiento.Utils;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows.Forms;

namespace DeportNetReconocimiento.GUI
{
    public partial class WFPrincipal : Form
    {
        private Hik_Controladora_General? hik_Controladora_General;
        private System.Windows.Forms.Timer? timer;
        private static WFPrincipal? instancia;
        private bool ignorarCierre = false;
        private static readonly object lockObj = new object();
        private ConfiguracionEstilos configuracionEstilos;

        
        public WFPrincipal()
        {
            InitializeComponent();

            //TODO: Estos estilos se leen de un archivo de configuración o de la base de datos
            configuracionEstilos = new ConfiguracionEstilos();
            AplicarConfiguracion(configuracionEstilos);

            //InstanciarPrograma(); //Instanciamos el programa con los datos de la camara
            //Escuchador_Directorio.InicializarEscuchadorEnHilo();
            //ConfigurarTimer(); //configuramos el timer para que cada un tiempo determinado verifique el estado del dispositivo
        }

        //propiedades

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
            get { return hik_Controladora_General; }
            set { hik_Controladora_General = value; }
        }

        //funciones

        public void AplicarConfiguracion(ConfiguracionEstilos config)
        {
            ConfiguracionEstilos = config;

            BackColor = config.ColorFondo;
            Font = config.FuenteTexto;
            fondoMensajeAcceso.BackColor = config.ColorFondoMensajeAcceso;
            
            HeaderLabel.BackColor = config.ColorFondoMensajeAcceso;

            actividadLabel.ForeColor = config.ColorCampoActividad;
            valorFechaVtoLabel.ForeColor = config.ColorVencimiento;
            valorClasesRestLabel.ForeColor = config.ColorClasesRestantes;
            valorMensajeLabel.ForeColor = config.ColorMensaje;
            pictureBox1.BackColor = config.ColorFondoImagen;


            //TODO: Falta el logo
        }



        public Hik_Resultado InstanciarPrograma()
        {

            Hik_Resultado resultado = new Hik_Resultado();

            //ip , puerto, usuario, contraseña en ese orden
            string[] credenciales = LeerCredenciales();


            if (credenciales.Length == 0)
            {
                //Si no hubo exito mostrar ventana con el error. Un modal 
                resultado.Exito = false;
                resultado.Mensaje = "No se pudieron leer las credenciales";
                MessageBox.Show("Error no se pudo iniciar sesion con exito... Vuelva a intentarlo");
                //this.Close();
            }


            if (credenciales.Length > 0)
            {
                Instancia_Controladora_General = Hik_Controladora_General.InstanciaControladoraGeneral;
                resultado = Instancia_Controladora_General.InicializarPrograma(credenciales[2], credenciales[3], credenciales[1], credenciales[0]);

            }




            return resultado;
        }

        private void cerrarFormulario(object sender, FormClosingEventArgs e)
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

        public string[] LeerCredenciales()
        {
            var listaDatos = new System.Collections.Generic.List<string>();
            string rutaArchivo = "credenciales.bin";


            //si el archivo no existe, se abre la ventana para registrar el dispositivo
            if (!File.Exists(rutaArchivo))
            {
                //this.Hide();
                WFRgistrarDispositivo wFRgistrarDispositivo = new WFRgistrarDispositivo();
                wFRgistrarDispositivo.ShowDialog();
            }
            else
            {

                // Leer desde un archivo binario
                using (BinaryReader reader = new BinaryReader(File.Open(rutaArchivo, FileMode.Open)))
                {
                    while (reader.BaseStream.Position != reader.BaseStream.Length) // Lee hasta el final del archivo
                    {
                        string unDato = reader.ReadString(); // Lee cada string
                        listaDatos.Add(unDato);

                        Console.WriteLine($"Leído: {unDato}");
                    }
                }
            }

            return listaDatos.ToArray();
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
                    Console.WriteLine("sin conexion");
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
                return false;
        }

        //Se crea un objeto de tipo Task para que la función se ejecute en un hilo distinto al principal
        //Se usa async await para manejar la asincronía 
        public async Task<bool> verificarEstadoDispositivoAsync()
        {
            //Se espera al resultado de la función verificarEstadoDispositivo 
            //Mientras se pone a correr un hilo secundario para que no se bloquee el hilo principal
            return await Task.Run(() => VerificarEstadoDispositivo());
        }

        //Timer para verificar la conexión
        private void ConfigurarTimer()
        {
            Hik_Resultado resultado = new Hik_Resultado();
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 20000;
            timer.Tick += async (s, e) =>
            {
                resultado.Exito = await verificarEstadoDispositivoAsync();

                //Verificar estado de internet
                //El objetivo es saber si los datos reconocidos se almacenan o no en la base de datos local
                if (!Hik_Controladora_General.VerificarConexionInternet())
                {
                    //TODO: en realidad habria que mostrar en la GUI una ventana que diga que no hay conexion a internet (verificar que exista asi no se pone multiples veces)
                    MessageBox.Show("No hay conexion a internet");
                }



                if (!resultado.Exito)
                {
                    InstanciarPrograma();
                }

            };
            timer.Start();
        }


        //función para actualizar los datos en el hilo principal
        public async void ActualizarDatos(int nroLector, string json)
        {
            //Si el hilo que llama a la función no es el principal, se llama a la función de nuevo en el hilo principal
            if (InvokeRequired)
            {
                Invoke(new Action<int, string>(ActualizarDatos), nroLector, json);
                return;
            }

            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }

            //Se convierte el json a un objeto de tipo Persona
            Persona persona = JSONtoPersona(json);

            string respuesta = "Acceso concedido " + persona.Nombre;
            HeaderLabel.ForeColor = Color.Green;

            //Se actualizan los labels con los datos de la persona
            HeaderLabel.Text = respuesta;
            actividadLabel.Text = persona.Actividad;
            valorFechaVtoLabel.Text = persona.Vencimiento;
            valorClasesRestLabel.Text = persona.ClasesRestantes;
            valorMensajeLabel.Text = persona.Mensaje;

            pictureBox1.Image = ObtenerFotoCliente(nroLector, persona.Id);

            //Esperamos 5 segundos para borrar los datos
            //TODO: el tiempo sera variable
            await Task.Delay(5000);
            LimpiarInterfaz();
        }

        Image ObtenerFotoCliente(int nroLector, string idCliente)
        {
            Image imagen = null;
            //Se obtiene la foto del cliente
            Hik_Resultado resultado = Hik_Controladora_Facial.ObtenerInstancia.ObtenerCara(nroLector, idCliente);
            if (resultado.Exito)
            {
                String ruta = Path.Combine(Directory.GetCurrentDirectory(), "FacePicture.jpg");
                imagen = Image.FromFile(ruta);
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

        public void LimpiarInterfaz()
        {
            if (InvokeRequired)
            {
                Invoke(LimpiarInterfaz);
                return;
            }

            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            HeaderLabel.Text = "";
            //valorNombreHeaderLabel.Text = "";
            actividadLabel.Text = "";
            valorFechaVtoLabel.Text = "";
            valorClasesRestLabel.Text = "";
            valorMensajeLabel.Text = "";
        }

        public static Persona JSONtoPersona(string json)
        {

            Persona persona = new Persona();

            using (JsonDocument doc = JsonDocument.Parse(json))
            {
                JsonElement root = doc.RootElement;

                // Acceder a cada campo del objeto JSON
                persona.Id = root.GetProperty("Id").GetString();
                persona.Nombre = root.GetProperty("Nombre").GetString();
                persona.Apellido = root.GetProperty("Apellido").GetString();
                persona.Actividad = root.GetProperty("Actividad").GetString();
                persona.Vencimiento = root.GetProperty("Vencimiento").GetString();
                persona.ClasesRestantes = root.GetProperty("ClasesRestantes").GetString();
                persona.Rta = root.GetProperty("Rta").GetString();
                persona.Mensaje = root.GetProperty("Mensaje").GetString();
                persona.Fecha = root.GetProperty("Fecha").GetString();
                persona.Hora = root.GetProperty("Hora").GetString();
            }
            return persona;
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
                this.Hide(); // Oculta la ventana principal

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

        // pictureBox1.Image = ObtenerFotoCliente(1, textBoxId.Text);


        private void WFPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void actividadLabel_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void HeaderLabel_Click(object sender, EventArgs e)
        {

        }

        private void valorNombreHeaderLabel_Click(object sender, EventArgs e)
        {

        }

        private void mensajeOpcionalLabel_Click(object sender, EventArgs e)
        {

        }

        private void valorMensajeLabel_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
