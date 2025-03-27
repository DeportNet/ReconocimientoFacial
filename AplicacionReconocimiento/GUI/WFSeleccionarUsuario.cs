using DeportNetReconocimiento.Api.BD;
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
        private readonly BdContext _contextBd;

        public WFSeleccionarUsuario(BdContext bdContext)
        {
            InitializeComponent();
            _contextBd = bdContext;

        }







        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
