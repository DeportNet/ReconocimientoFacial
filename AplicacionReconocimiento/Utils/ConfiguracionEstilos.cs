using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Utils
{
    public class ConfiguracionEstilos
    {

        [Category("General")]
        [DisplayName("Color de Fondo")]
        public Color ColorFondo { get; set; }

        //[Category("Labels")]
        //[DisplayName("Color del Título")]
        //public Color ColorTitulo { get; set; }

        //[Category("Labels")]
        //[DisplayName("Fuente del Título")]
        //public Font FuenteTitulo { get; set; }

        //[Category("Logo")]
        //[DisplayName("Logo de la Ventana")]
        //public string LogoPath { get; set; }

        // Constructor predeterminado
        public ConfiguracionEstilos()
        {
            ColorFondo = Color.White;
            //ColorTitulo = Color.Black;
            //FuenteTitulo = new Font("Arial", 16, FontStyle.Bold);
            //LogoPath = string.Empty; // Vacío por defecto
        }

        public ConfiguracionEstilos(Color colorFondo, Color colorTitulo, Font fuenteTitulo, string logoPath)
        {
            ColorFondo = colorFondo;
            //ColorTitulo = colorTitulo;
            //FuenteTitulo = fuenteTitulo;
            //LogoPath = logoPath;
        }
    }
}
