using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace DeportNetReconocimiento.Utils
{
    public class ConfiguracionEstilos
    {

        /* - - - - - - General - - - - - */

        [Category("General")]
        [DisplayName("Color de Fondo")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color ColorFondo { get; set; }

        [Category("General")]
        [DisplayName("Logo de la pantalla de bienvenida")]
        public string LogoRuta { get; set; }

        [Category("General")]
        [DisplayName("Fuente texto")]
        [JsonConverter(typeof(FontJsonConverter))]
        public Font FuenteTexto { get; set; }

        [Category("General")]
        [DisplayName("Tiempo de muestra de datos")]
        public int TiempoMuestraDatos { get; set; }

        /* - - - - - Mensaje de acceso - - - - - */

        [Category("Mensaje de acceso")]
        [DisplayName("Color del fondo mensaje de acceso")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color ColorFondoMensajeAcceso { get; set; }

        [Category("Mensaje de acceso")]
        [DisplayName("Color de mensaje de bienvenida")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color ColorMensajeBienvenida { get; set; }

        [Category("Mensaje de acceso")]
        [DisplayName("Color de mensaje de acceso denegado")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color ColorMensajeAccesoDenegado { get; set; }


        /* - - - - - Campos de informacion - - - - - - */


        [Category("Campos de informacion")]
        [DisplayName("Color del campo actividad")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color ColorCampoActividad { get; set; }



        [Category("Campos de informacion")]
        [DisplayName("Color del campo vencimiento")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color ColorVencimiento { get; set; }

        [Category("Campos de informacion")]
        [DisplayName("Color del campo clases restantes")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color ColorClasesRestantes { get; set; }

        [Category("Campos de informacion")]
        [DisplayName("Color del campo mensaje")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color ColorMensaje { get; set; }

        [Category("Campos de informacion")]
        [DisplayName("Color de fondo imagen")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color ColorFondoImagen { get; set; }




        // Constructor predeterminado
        public ConfiguracionEstilos()
        {
            // General
            ColorFondo = Color.Silver;
            FuenteTexto = new Font("Arial Rounded MT Bold", 11, FontStyle.Regular);
            LogoRuta = @"D:\DeportNet\DeportNetReconocimiento\AplicacionReconocimiento\Recursos\logo_deportnet_1.jpg"; ; // Logo deportnet por defecto
            TiempoMuestraDatos = 3;

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
            string rutaJson = "configuracionEstilos";
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true // JSON legible
                };

                // Serializar la configuración
                string json = JsonSerializer.Serialize(configuracion, options);
                File.WriteAllText($"{rutaJson}.json", json);

                Console.WriteLine("Configuración guardada correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar el archivo de configuración: {ex.Message}");
            }
        }

        public static ConfiguracionEstilos LeerJsonConfiguracion(string rutaJson)
        {
            //"configuracionEstilos.json"

            ConfiguracionEstilos configuracionEstilos = new ConfiguracionEstilos();

            if (File.Exists($"{rutaJson}.json"))
            {
                try
                {
                    var options = new JsonSerializerOptions
                    {
                        Converters = { new ColorJsonConverter() } // Agregar el convertidor personalizado
                    };

                    // Leer el contenido del archivo
                    string jsonContent = File.ReadAllText($"{rutaJson}.json");

                    // Deserializar el contenido
                    configuracionEstilos = JsonSerializer.Deserialize<ConfiguracionEstilos>(jsonContent, options);

                    Console.WriteLine("Configuración cargada correctamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"No se pudo leer el JSON de configuración: {ex.Message}");
                }
            }

            return configuracionEstilos;
        }



    }

    // Convertidor personalizado para la clase Color
    public class ColorJsonConverter : JsonConverter<Color>
    {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string colorData = reader.GetString();

            // Verificar si el valor es un KnownColor
            if (Enum.TryParse(typeof(KnownColor), colorData, out var knownColor))
            {
                return Color.FromKnownColor((KnownColor)knownColor);
            }

            // Si no, asume que es un hexadecimal y conviértelo
            return ColorTranslator.FromHtml(colorData);
        }

        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        {
            // Siempre usar el nombre si es un KnownColor
            if (value.IsKnownColor)
            {
                writer.WriteStringValue(value.Name);
            }
            else
            {
                // Guardar como hexadecimal solo para colores personalizados
                writer.WriteStringValue(ColorTranslator.ToHtml(value));
            }
        }
    }

    //Convertidor personalizado para la clase Font

    public class FontJsonConverter : JsonConverter<Font>
    {
        public override Font Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Leer la fuente como una cadena
            string fontData = reader.GetString();
            string[] parts = fontData.Split('/');

            if (parts.Length != 3)
            {
                throw new JsonException("Formato de fuente inválido.");
            }

            // Extraer los componentes
            string fontName = parts[0];
            float fontSize = float.Parse(parts[1]);
            FontStyle fontStyle = (FontStyle)Enum.Parse(typeof(FontStyle), parts[2]);

            return new Font(fontName, fontSize, fontStyle);
        }

        public override void Write(Utf8JsonWriter writer, Font value, JsonSerializerOptions options)
        {
            // Convertir la fuente a una cadena
            string fontData = $"{value.Name}/{value.Size}/{value.Style}";
            writer.WriteStringValue(fontData);
        }
    }

}

