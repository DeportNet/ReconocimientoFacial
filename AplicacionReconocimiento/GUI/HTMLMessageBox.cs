using Microsoft.Web.WebView2.WinForms;
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

namespace DeportNetReconocimiento.GUI
{
    public partial class HTMLMessageBox : Form
    {
        private WebView2 webView;


        public event Action<bool> OpcionSeleccionada;

        public HTMLMessageBox()
        {

        }

        public HTMLMessageBox(string htmlContent)
        {
            // Configuración del formulario
            this.Size = new System.Drawing.Size(500, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Quitar los botones de minimizar, maximizar y cerrar
            this.ControlBox = false;

            // Inicializar WebView2
            webView = new WebView2
            {
                Dock = DockStyle.Fill
            };

            // Inicializar WebView2 Core
            webView.CoreWebView2InitializationCompleted += async (sender, e) =>
            {
                if (!e.IsSuccess)
                {
                    MessageBox.Show("Error al inicializar WebView2.");
                    return;
                }

                // Preparo el texto para cargarlo como corresponde
                string textoSinCaracteresEscape = LimpiarCaracteresEscape(htmlContent);
                string textoSinUnicode = SacarFormaToUnicode(textoSinCaracteresEscape);
                string textoDecodificado = System.Net.WebUtility.HtmlDecode(textoSinUnicode);

                // Cargar el contenido HTML
                webView.NavigateToString(textoDecodificado);
            };

            // Crear un panel para los botones en la parte inferior
            Panel bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50
            };

            FlowLayoutPanel flowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(130, 2, 20, 0),
                AutoSize = true
            };

            // Botón "Sí"
            Button yesButton = new Button
            {
                Text = "Sí",
                Width = 100,
                Height = 40
            };
            yesButton.Click += (sender, e) =>
            {
                OpcionSeleccionada?.Invoke(true);
                this.Close();
            };

            // Botón "No"
            Button noButton = new Button
            {
                Text = "No",
                Width = 100,
                Height = 40
            };
            noButton.Click += (sender, e) =>
            {
                OpcionSeleccionada?.Invoke(false);
                this.Close();
            };

            // Agregar los botones al FlowLayoutPanel
            flowPanel.Controls.Add(yesButton);
            flowPanel.Controls.Add(noButton);

            // Agregar el FlowLayoutPanel al panel inferior
            bottomPanel.Controls.Add(flowPanel);

            // Agregar controles al formulario
            this.Controls.Add(webView);
            this.Controls.Add(bottomPanel);

            // Inicializar WebView2
            InitializeWebView2Async();
        }

        private async void InitializeWebView2Async()
        {
            try
            {
                await webView.EnsureCoreWebView2Async();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar WebView2: {ex.Message}");
            }
        }


        public void Show(string htmlContent)
        {
            HTMLMessageBox messageBox = new HTMLMessageBox(htmlContent);
            messageBox.ShowDialog();
        }

        static string SacarFormaToUnicode(string input)
        {
            // Reemplaza las secuencias de escape Unicode con los caracteres correspondientes
            return Regex.Replace(input, @"\\u([0-9A-Fa-f]{4})", match =>
            {
                // Convierte el código Unicode en el carácter correspondiente
                return char.ConvertFromUtf32(int.Parse(match.Groups[1].Value, System.Globalization.NumberStyles.HexNumber));
            });
        }


        // Método para eliminar caracteres de escape innecesarios
        static string LimpiarCaracteresEscape(string input)
        {
            // Reemplazar \/ por /
            input = input.Replace(@"\/", "/");

            // Reemplazar \n por un salto de línea
            input = input.Replace(@"\n", "\n");

            // Devolver el texto limpio
            return input;
        }
    }
}
