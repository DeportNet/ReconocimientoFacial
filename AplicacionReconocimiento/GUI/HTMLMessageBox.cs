using DeportNetReconocimiento.Api.Dtos.Response;
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

            richTextBox1.Rtf = MensajeCrudoARtf(accesoResponse.MensajeCrudo);


            panel1.Controls.Add(BotonNo);
            panel1.Controls.Add(botonSi);
            this.Controls.Add(panel1);

        }

        private static string MensajeCrudoARtf(string mensajeCrudo)
        {

            string limpiarEscapes = LimpiarCaracteresEscape(mensajeCrudo);
            string limpiarFormatoUnicode = SacarFormaToUnicode(limpiarEscapes);
            return ConvertirHtmlToRtf(limpiarFormatoUnicode);
        }

        private static string ConvertirHtmlToRtf(string html)
        {
            // Reemplazar etiquetas HTML por RTF
            html = html.Replace("<strong>", @"\b ").Replace("</strong>", @"\b0 ");
            html = html.Replace("<br>", @"\line ");
            html = html.Replace("<div>", @"\line ");
            html = html.Replace("</div>", "");
            html = html.Replace("\n", @"\line ");

            // Darle el formato RTF a lo demas 
            string rtfHeader = @"{\rtf1\ansi\deff0 {\fonttbl {\f0 Arial;}} ";
            string rtfFooter = "}";

            return rtfHeader + html + rtfFooter;
        }

        // Método para eliminar caracteres de escape innecesarios
        private static string LimpiarCaracteresEscape(string input)
        {
            // Reemplazar \/ por /
            input = input.Replace(@"\/", "/");

            // Reemplazar \n por un salto de línea
            input = input.Replace(@"\n", "\n");

            // Devolver el texto limpio
            return input;

        }

        private static string SacarFormaToUnicode(string input)
        {
            // Reemplaza las secuencias de escape Unicode con los caracteres correspondientes
            return Regex.Replace(input, @"\\u([0-9A-Fa-f]{4})", match =>
            {
                // Convierte el código Unicode en el carácter correspondiente
                return char.ConvertFromUtf32(int.Parse(match.Groups[1].Value, System.Globalization.NumberStyles.HexNumber));
            });

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
