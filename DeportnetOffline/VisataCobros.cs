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
    public partial class VisataCobros : UserControl
    {
        public VisataCobros()
        {
            InitializeComponent();
            int paginaActual = 1;
            int filasPorPagina = 5;
            int registros = 10;
            labelCantPaginas.Text = $"Página {paginaActual} de 50";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
