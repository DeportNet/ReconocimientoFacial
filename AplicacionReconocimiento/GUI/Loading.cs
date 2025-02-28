using DeportNetReconocimiento.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeportNetReconocimiento.GUI
{
    public partial class Loading : Form
    {
        
        public Loading()
        {
            InitializeComponent();
        }

        public void CambiarTexto(string texto)
        {
            label1.Text = texto;
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
