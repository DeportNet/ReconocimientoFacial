using DeportNetReconocimiento.Modelo;
using DeportNetReconocimiento.SDK;
using DeportNetReconocimiento.SDKHikvision;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DeportNetReconocimiento.SDK.Hik_SDK;

namespace DeportNetReconocimiento.GUI
{


    public partial class WFPanelOffline : Form
    {
        public WFPanelOffline()
        {
            InitializeComponent();

        }



        private void WFPanelOffline_Load(object sender, EventArgs e)
        {

        }

        // 0-close, 1-open, 2-stay open, 3-stay close
        private void CerrarPuerta_Click(object sender, EventArgs e)
        {
            Hik_Controladora_Puertas.OperadorPuerta(0);
        }

        private void AbrirPuerta_Click(object sender, EventArgs e)
        {
            Hik_Controladora_Puertas.OperadorPuerta(1);
        }

        private void MantenerAbiertaPuerta_Click(object sender, EventArgs e)
        {
            Hik_Controladora_Puertas.OperadorPuerta(2);
        }

        private void ManterCerradaPuerta_Click(object sender, EventArgs e)
        {
            Hik_Controladora_Puertas.OperadorPuerta(3);
        }
    }
}
