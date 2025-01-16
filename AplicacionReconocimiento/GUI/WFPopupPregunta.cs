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
    //public event Action<bool> OpcionSeleccionada;

    public partial class WFPopupPregunta : Form
    {
        private string textoPreguntaHtml;

        public WFPopupPregunta(string preguntaFormatoHtml)
        {
            InitializeComponent();
            this.textoPreguntaHtml = preguntaFormatoHtml;
        }

        private async void WFPopupPregunta_LoadAsync(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(WFPopupPregunta_LoadAsync);
                return;
            }

            try
            {
                // Asegúrate de inicializar CoreWebView2
                await webView21.EnsureCoreWebView2Async();

                // Cargar el contenido HTML
                webView21.NavigateToString(textoPreguntaHtml);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar WebView2: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


    }
}
