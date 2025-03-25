using DeportNetReconocimiento.Utils;
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
    public partial class WFSeleccionarUsuario : Form
    {
        public WFSeleccionarUsuario()
        {
            InitializeComponent();
            CredencialesUtils.LeerCredenciales();
            
        }







        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
