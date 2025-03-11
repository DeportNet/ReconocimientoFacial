using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeportnetOffline
{
    public partial class VistaSocios : UserControl
    {
        public VistaSocios()
        {
            int paginaActual = 1;
            int filasPorPagina = 5;
            int registros = 10;
            InitializeComponent();
            labelCantPaginas.Text = $"Página {paginaActual} de 50";

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void VistaSocios_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Apellido y Nombre")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black; // Color del texto cuando el usuario escribe
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                textBox3.Text = "Apellido y Nombre";
                textBox3.ForeColor = Color.Gray; // Color del placeholder
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Email")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black; // Color del texto cuando el usuario escribe
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.Text = "Email";
                textBox2.ForeColor = Color.Gray; // Color del placeholder
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Nro tarjeta o DNI")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black; // Color del texto cuando el usuario escribe
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Nro Tarjeta o DNI";
                textBox1.ForeColor = Color.Gray; // Color del placeholder
            }
        }

        private void ComboBox1_Enter(object sender, EventArgs e)
        {
            if (comboBox1.ForeColor == Color.Gray)
            {
                comboBox1.Text = "";
                comboBox1.ForeColor = Color.Black;
            }
        }

        private void ComboBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                comboBox1.Text = "Categoría de socio...";
                comboBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void botonSgtPaginacion_Click(object sender, EventArgs e)
        {

        }

        private void botonSgtPaginacion_Click_1(object sender, EventArgs e)
        {

        }
    }
}
