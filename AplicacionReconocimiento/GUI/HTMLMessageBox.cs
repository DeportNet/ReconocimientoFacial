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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace DeportNetReconocimiento.GUI
{
    public partial class HTMLMessageBox : Form
    {

        public event Action<bool>? OpcionSeleccionada;

        private string preguntaHtml;
        public HTMLMessageBox(string htmlContent)
        {
           
            InitializeComponent();

            preguntaHtml = htmlContent;

            panel1.Controls.Add(BotonNo);
            panel1.Controls.Add(botonSi);
            this.Controls.Add(webView21);
            this.Controls.Add(panel1);


        
            
        }

        private void InicializarWebView2(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            // Inicializar WebView2 Core
            Console.WriteLine("Entro a inicializar");
            try
            {
                Console.WriteLine("Ensure Core web 2 async, adentro del try");
                webView21.EnsureCoreWebView2Async();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problema con inicializar, adentro del catch");
                MessageBox.Show($"Error al inicializar WebView2: {ex.Message}");
            }

            if (!e.IsSuccess)
            {
                Console.WriteLine("no hubo succes");
                MessageBox.Show("Error al inicializar WebView2.");
                return;
            }


            Console.WriteLine("MOstramos el string");
            // Cargar el contenido HTML
            webView21.NavigateToString(preguntaHtml);
        }


        private void BotonNo_Click(object sender, EventArgs e)
        {
            OpcionSeleccionada?.Invoke(false);
            this.Close();
        }

        private void BotonSi_Click(object sender, EventArgs e)
        {
            OpcionSeleccionada?.Invoke(true);
            this.Close();
        }


        private async void HTMLMessageBox_Load(object sender, EventArgs e)
        {
           
        }
    }
}
