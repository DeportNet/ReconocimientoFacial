using DeportNetReconocimiento.Modelo;
using DeportNetReconocimiento.SDK;
using System.Text.Json;

namespace DeportNetReconocimiento.GUI
{
    public partial class WFPrincipal : Form
    {
        Hik_Controladora_General hik_Controladora_General;
        public WFPrincipal()
        {
            Hik_Resultado hik_Resultado = new Hik_Resultado();
            InitializeComponent();
            

            hik_Resultado =Hik_Controladora_General.InstanciaControladoraGeneral.InicializarPrograma("admin", "Facundo2024*", "8000", "192.168.0.207");

            if (!hik_Resultado.Exito)
            {
                //Si no hubo exito mostrar ventana con el error. Un modal 
            }
            else
            {
                hik_Controladora_General = Hik_Controladora_General.InstanciaControladoraGeneral;
            }

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
                persona.ClasesRestantes = root.GetProperty("Clases_restantes").GetString();
                persona.Mensaje = root.GetProperty("Mensaje").GetString();

            }
            return persona;


        }


    }
}
