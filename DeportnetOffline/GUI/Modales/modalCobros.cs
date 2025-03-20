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
    public partial class ModalCobro : Form
    {
        public ModalCobro(string nombreApellidoSocio)
        {
            InitializeComponent();
            labelNombreApelldioCliente.Text = nombreApellidoSocio;
            comboBox1.SelectedIndex = 0;
        }


    }
}
