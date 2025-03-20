using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DeportnetOffline
{
    public partial class ModalVentas : Form
    {
        public ModalVentas(string nombreApellidoSocio)
        {
            InitializeComponent();
            labelNombreApelldioCliente.Text = nombreApellidoSocio;
            comboBox1.SelectedIndex = 0;
        }




    }
}
