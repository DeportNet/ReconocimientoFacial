using DeportNetReconocimiento.Api.Dtos.Response;
using DeportNetReconocimiento.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace DeportNetReconocimiento.GUI
{
    public partial class HTMLMessageBox : Form
    {

        public event Action<RespuestaAccesoManual>? OpcionSeleccionada;

        private ValidarAccesoResponse accesoResponse;

        public ValidarAccesoResponse AccesoResponse { get => accesoResponse; set => accesoResponse = value; }




        public HTMLMessageBox(ValidarAccesoResponse accesoResponse)
        {
           
            InitializeComponent();

            this.AccesoResponse = accesoResponse;

            richTextBox1.Rtf = ConvertidorTextoUtils.LimpiarTextoEnriquecido(accesoResponse.MensajeCrudo);

            panel1.Controls.Add(BotonNo);
            panel1.Controls.Add(botonSi);
            this.Controls.Add(panel1);

        }


        private void BotonNo_Click(object sender, EventArgs e)
        {
            OpcionSeleccionada?.Invoke(new RespuestaAccesoManual(accesoResponse.IdSucursal, accesoResponse.IdCliente, "F"));

            this.Close();
        }

        private void BotonSi_Click(object sender, EventArgs e)
        {
            OpcionSeleccionada?.Invoke(new RespuestaAccesoManual(accesoResponse.IdSucursal, accesoResponse.IdCliente, "T"));

            this.Close();
        }

    }
}
