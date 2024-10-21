using DeportNetReconocimiento.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
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
