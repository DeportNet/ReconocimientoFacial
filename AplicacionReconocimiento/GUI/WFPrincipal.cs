using DeportNetReconocimiento.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace DeportNetReconocimiento.GUI
{
    public partial class WFPrincipal : Form
    {
        public WFPrincipal()
        {
            InitializeComponent();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void WFPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void apellido_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void nombreText_Click(object sender, EventArgs e)
        {

        }

        private void actividadText_Click(object sender, EventArgs e)
        {

        }

        private void clasesRestaintesText_Click(object sender, EventArgs e)
        {

        }

        private void mensajeText_Click(object sender, EventArgs e)
        {

        }

        private void clasesRestantesText(object sender, EventArgs e)
        {

        }

        public void actualizarDatos(string json)
        {

            Persona persona = JSONtoPersona(json);


            apellidoText.Text = persona.Apellido;
            nombreText.Text = persona.Nombre;
            actividadText.Text = persona.Actividad;
            clasesRestaintesText.Text = persona.ClasesRestantes;
            mensajeText.Text = persona.Mensaje;



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
