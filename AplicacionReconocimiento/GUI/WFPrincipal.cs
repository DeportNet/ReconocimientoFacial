using DeportNetReconocimiento.Modelo;
using DeportNetReconocimiento.SDK;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace DeportNetReconocimiento.GUI
{
    public partial class WFPrincipal : Form
    {
        private Hik_Controladora_General hik_Controladora_General;
        private System.Windows.Forms.Timer timer;

        private static WFPrincipal instancia;


        public static WFPrincipal ObtenerInstancia()
        {
            if (instancia == null)
            {
                instancia = new WFPrincipal();
            }
            return instancia;
        }


        public WFPrincipal()
        {
            InitializeComponent();
            ConfigurarTimer(); //configuramos el timer para que cada un tiempo determinado verifique el estado del dispositivo

            
            InstanciarPrograma(); //Instanciamos el programa con los datos de la camara
        }

        public Hik_Resultado InstanciarPrograma()
        {
            Hik_Resultado resultado = new Hik_Resultado();

            //ip , puerto, usuario, contraseña en ese orden
            string[] credenciales = leerCredenciales();

            resultado = Hik_Controladora_General.InstanciaControladoraGeneral.InicializarPrograma(credenciales[2], credenciales[3], credenciales[1], credenciales[0]);

            if (!resultado.Exito)
            {
                //Si no hubo exito mostrar ventana con el error. Un modal 
                MessageBox.Show("Error no se pudo iniciar sesion con exito... Vuelva a intentarlo");
            }
            else
            {
                hik_Controladora_General = Hik_Controladora_General.InstanciaControladoraGeneral;
            }

            return resultado;
        }

        public string[] leerCredenciales()
        {
            var listaDatos = new System.Collections.Generic.List<string>();
            string rutaArchivo = "credenciales.bin";


            //si el archivo no existe, se abre la ventana para registrar el dispositivo
            if (!File.Exists(rutaArchivo))
            {
                this.Close();
                WFRgistrarDispositivo wFRgistrarDispositivo = new WFRgistrarDispositivo();
                wFRgistrarDispositivo.ShowDialog();
            }


           
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

            return listaDatos.ToArray();
        }

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

        public async Task<bool> verificarEstadoDispositivoAsync()
        {
            return await Task.Run(() => VerificarEstadoDispositivo());
        }

        private void ConfigurarTimer()
        {
            Hik_Resultado resultado = new Hik_Resultado();
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 5000;
            timer.Tick += async (s, e) =>
            {
                resultado.Exito = await verificarEstadoDispositivoAsync();

                if (!resultado.Exito)
                {
                    Console.WriteLine("Que onda");
                    //InstanciarPrograma("admin", "Facundo2024*", "8000", "192.168.0.207");
                }

            };
            timer.Start();
        }


        private void ApellidoLabel_Click(object sender, EventArgs e)
        {

        }

        private void ClasesRestantesLabel_Click(object sender, EventArgs e)
        {

        }

        private void NombreLabel_Click(object sender, EventArgs e)
        {

        }

        private void ActividadLabel_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void ActualizarDatos(string json)
        {

            Persona persona = JSONtoPersona(json);

            ApellidoLabel.Text = persona.Apellido;
            NombreLabel.Text = persona.Nombre;
            ActividadLabel.Text = persona.Actividad;
            ClasesRestantesLabel.Text = persona.ClasesRestantes;
            MensajeLabel.Text = persona.Mensaje;

        }


        public static Persona JSONtoPersona(string json)
        {

            Persona persona = new Persona();

            using (JsonDocument doc = JsonDocument.Parse(json))
            {
                JsonElement root = doc.RootElement;

                // Acceder a cada campo del objeto JSON
                persona.Id = root.GetProperty("Id").GetInt32();
                persona.Apellido = root.GetProperty("Apellido").GetString();
                persona.Nombre = root.GetProperty("Nombre").GetString();
                persona.Actividad = root.GetProperty("Actividad").GetString();
                persona.ClasesRestantes = root.GetProperty("ClasesRestantes").GetString();
                persona.Mensaje = root.GetProperty("Mensaje").GetString();

            }
            return persona;
        }




    }
}
