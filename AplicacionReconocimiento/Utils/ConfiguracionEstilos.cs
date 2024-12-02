using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DeportNetReconocimiento.Utils
{
    public class ConfiguracionEstilos
    {

        /* - - - - - - General - - - - - */

        [Category("General")]
        [DisplayName("Color de Fondo")]
        public Color ColorFondo { get; set; }

        [Category("General")]
        [DisplayName("Logo de la pantalla de bienvenida")]
        public string LogoRuta { get; set; }

        [Category("General")]
        [DisplayName("Fuente texto")]
        public Font FuenteTexto { get; set; }

        /* - - - - - Mensaje de acceso - - - - - */

        [Category("Mensaje de acceso")]
        [DisplayName("Color del fondo mensaje de acceso")]
        public Color ColorFondoMensajeAcceso { get; set; }

        [Category("Mensaje de acceso")]
        [DisplayName("Color de mensaje de bienvenida")]
        public Color ColorMensajeBienvenida { get; set; }

        [Category("Mensaje de acceso")]
        [DisplayName("Color de mensaje de acceso denegado")]
        public Color ColorMensajeAccesoDenegado { get; set; }


        /* - - - - - Campos de informacion - - - - - - */


        [Category("Campos de informacion")]
        [DisplayName("Color del campo actividad")]
        public Color ColorCampoActividad { get; set; }



        [Category("Campos de informacion")]
        [DisplayName("Color del campo vencimiento")]
        public Color ColorVencimiento { get; set; }

        [Category("Campos de informacion")]
        [DisplayName("Color del campo clases restantes")]
        public Color ColorClasesRestantes { get; set; }

        [Category("Campos de informacion")]
        [DisplayName("Color del campo mensaje")]
        public Color ColorMensaje { get; set; }

        [Category("Campos de informacion")]
        [DisplayName("Color de fondo imagen")]
        public Color ColorFondoImagen { get; set; }

        //[Category("Labels")]
        //[DisplayName("Fuente del Título")]
        //public Font FuenteTitulo { get; set; }



        // Constructor predeterminado
        public ConfiguracionEstilos()
        {
            // General
            ColorFondo = Color.Silver;
            FuenteTexto = new Font("Arial Rounded MT Bold", 36);
            LogoRuta = @"D:\DeportNet\DeportNetReconocimiento\AplicacionReconocimiento\Recursos\logo_deportnet_1.jpg"; ; // Logo deportnet por defecto
        

            // Mensaje de acceso
            ColorFondoMensajeAcceso = Color.DarkGray;
            ColorMensajeBienvenida = Color.Green;
            ColorMensajeAccesoDenegado = Color.Red;

            // Campos de informacion
            ColorCampoActividad = Color.Black;
            ColorVencimiento = Color.Black;
            ColorClasesRestantes = Color.Black;
            ColorMensaje = Color.Black;
            ColorFondoImagen = Color.DarkGray;
        }


        public static void GuardarJsonConfiguracion(ConfiguracionEstilos configuracion)
        {
            string json = JsonSerializer.Serialize(configuracion);
            try
            {
                File.WriteAllText("configuracionEstilos.json", json);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error al guardar el archivo de configuración: " + e.Message);
            }

            
        }

        public static ConfiguracionEstilos LeerJsonConfiguracion(string rutaJson)
        {
            //"configuracionEstilos.json"
            
            ConfiguracionEstilos configuracionEstilos = new ConfiguracionEstilos();

            if (File.Exists($"{rutaJson}.json"))
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new FontConverter());

                string json = File.ReadAllText($"{rutaJson}.json");
                    configuracionEstilos = JsonSerializer.Deserialize<ConfiguracionEstilos>(json);

                //try
                //{
                //}
                //catch
                //{
                //    Console.WriteLine("No se pudo leer el json de configuracion");
                //    configuracionEstilos = new ConfiguracionEstilos(); // Configuración predeterminada
                //}
            }

            return configuracionEstilos;

        }
    }
}

public class FontConverter : JsonConverter<Font>
{
    private readonly TypeConverter _typeConverter = TypeDescriptor.GetConverter(typeof(Font));

    public override Font Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var fontString = reader.GetString();
        return fontString != null ? (Font)_typeConverter.ConvertFromString(fontString) : new Font("Arial", 12);
    }

    public override void Write(Utf8JsonWriter writer, Font value, JsonSerializerOptions options)
    {
        var fontString = _typeConverter.ConvertToString(value);
        writer.WriteStringValue(fontString);
    }


}