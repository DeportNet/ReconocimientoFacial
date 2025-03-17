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
    public partial class vistaAltaLegajos : UserControl
    {
        public vistaAltaLegajos()
        {
            InitializeComponent();
            int paginaActual = 1;
            int filasPorPagina = 5;
            int registros = 10;
            labelCantPaginas.Text = $"Página {paginaActual} de 50";
        }
    }
}
