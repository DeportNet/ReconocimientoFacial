using DeportNetReconocimiento.Properties;
using Microsoft.VisualBasic.Logging;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms.Design;


namespace DeportNetReconocimiento.Utils
{
    public class ConfiguracionEstilos
    {


        /* - - - - - - General - - - - - */

        [Category("General")]
        [DisplayName("Color de Fondo")]
        [Description("Define el color de fondo principal de la pantalla.")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color ColorFondo { get; set; }

        [Category("General")]
        [DisplayName("Logo de la pantalla de bienvenida")]
        [Description("Establece el logo que se mostrará en la pantalla de bienvenida. Debe ser una imagen en formato PNG, JPG, BMP, etc.")]
        [JsonConverter(typeof(ImageToPathJsonConverter))]
        public Image Logo { get; set; }
        

        [Category("General")]
        [DisplayName("Color de Fondo del Logo")]
        [Description("Especifica el color de fondo detrás del logo en la pantalla de bienvenida.")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color ColorFondoLogo { get; set; }

        [Category("General")]
        [DisplayName("Tiempo de muestra de datos en pantalla")]
        [Description("Indica la duración (en segundos) que se mostrarán los datos en la pantalla. No puede ser menor a 2 segundos.")]
        public float TiempoDeMuestraDeDatos
        {
            get => tiempoDeMuestraDeDatos;
            set
            {
                if (value < 2)
                {
                    MessageBox.Show(
                    "El tiempo de muestra de datos no puede ser menor a 2 seg.", // Mensaje
                    "Error de Validación",                                  // Título
                    MessageBoxButtons.OK,                                   // Botones (OK)
                    MessageBoxIcon.Error                                    // Ícono (Error)
                    );
                }
                else
                {
                    tiempoDeMuestraDeDatos = value;
                }
            }
        }
        private float tiempoDeMuestraDeDatos; // Valor predeterminado



        /* - - - - - Mensaje de acceso - - - - - */

        [Category("Mensaje de acceso")]
        [DisplayName("Fuente texto mensajes de acceso")]
        [Description("Selecciona la fuente utilizada para los mensajes de acceso, como el mensaje de bienvenida o acceso denegado.")]
        [JsonConverter(typeof(FontJsonConverter))]
        public Font FuenteTextoMensajeAcceso { get; set; }

        [Category("Mensaje de acceso")]
        [DisplayName("Color del fondo mensaje de acceso")]
        [Description("Define el color de fondo que se mostrará detrás del mensaje de acceso.")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color ColorFondoMensajeAcceso { get; set; }

        [Category("Mensaje de acceso")]
        [DisplayName("Color de mensaje de bienvenida")]
        [Description("Establece el color del texto para el mensaje de bienvenida.")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color ColorMensajeBienvenida { get; set; }

        [Category("Mensaje de acceso")]
        [DisplayName("Color de mensaje de acceso denegado")]
        [Description("Define el color del texto que se mostrará en el mensaje de acceso denegado.")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color ColorMensajeAccesoDenegado { get; set; }

        [Category("Mensaje de acceso")]
        [DisplayName("Mensaje predeterminado de bienvenida")]
        [Description("Texto que se mostrará como mensaje de bienvenida predeterminado.")]
        public string MensajeBienvenida { get; set; }

        [Category("Mensaje de acceso")]
        [DisplayName("Color del texto de mensaje de bienvenida")]
        [Description("Selecciona el color del texto del mensaje de bienvenida.")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color ColorTextoMensajeAcceso { get; set; }

        /* - - - - - Campos de informacion - - - - - - */

        [Category("Campos de informacion")]
        [DisplayName("Color del campo actividad")]
        [Description("Establece el color del texto para el campo de actividad del usuario.")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color ColorCampoActividad { get; set; }

        [Category("Campos de informacion")]
        [DisplayName("Fuente texto campos de informacion")]
        [Description("Selecciona la fuente utilizada en los campos de información del usuario.")]
        [JsonConverter(typeof(FontJsonConverter))]
        public Font FuenteTextoCamposInformacion { get; set; }

        [Category("Campos de informacion")]
        [DisplayName("Color del campo vencimiento")]
        [Description("Define el color del texto para el campo de vencimiento.")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color ColorVencimiento { get; set; }

        [Category("Campos de informacion")]
        [DisplayName("Color del campo clases restantes")]
        [Description("Establece el color del texto para el campo que muestra las clases restantes del usuario.")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color ColorClasesRestantes { get; set; }

        [Category("Campos de informacion")]
        [DisplayName("Color del campo mensaje")]
        [Description("Selecciona el color del texto para el campo de mensajes personalizados.")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color ColorMensaje { get; set; }

        [Category("Campos de informacion")]
        [DisplayName("Color de fondo imagen")]
        [Description("Especifica el color de fondo que aparecerá detrás de las imágenes en los campos de información.")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color ColorFondoImagen { get; set; }


        // Constructor predeterminado
        public ConfiguracionEstilos()
        {
            // General
            ColorFondo = Color.Silver;
            Logo = Resources.logo_deportnet_1;
            ColorFondoLogo = Color.DimGray;

            //Logo = @"D:\DeportNet\DeportNetReconocimiento\AplicacionReconocimiento\Recursos\logo_deportnet_1.jpg";  // Logo deportnet por defecto
            TiempoDeMuestraDeDatos = 3.0f;

            // Mensaje de acceso
            ColorFondoMensajeAcceso = Color.DarkGray;
            ColorMensajeBienvenida = Color.Green;
            ColorMensajeAccesoDenegado = Color.Red;
            MensajeBienvenida = "Bienvenido a DeportNet!";
            ColorTextoMensajeAcceso = Color.Black;

            FuenteTextoMensajeAcceso = new Font("Arial Rounded MT Bold", 36, FontStyle.Italic);

            // Campos de informacion
            ColorCampoActividad = Color.Black;
            ColorVencimiento = Color.Black;
            ColorClasesRestantes = Color.Black;
            ColorMensaje = Color.Black;
            ColorFondoImagen = Color.DarkGray;

            FuenteTextoCamposInformacion = new Font("Arial Rounded MT Bold", 20, FontStyle.Regular);
        }



        public static void GuardarJsonConfiguracion(ConfiguracionEstilos configuracion)
        {
            string rutaJson = "configuracionEstilos.json";
            string tempRutaJson = "configuracionEstilos_temp.json";

            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true // JSON legible
                };

                // Serializar la configuración
                string json = JsonSerializer.Serialize(configuracion, options);
                File.WriteAllText(tempRutaJson, json);

                // Validar el archivo temporal
                ConfiguracionEstilos configuracionValidada = LeerJsonConfiguracion(tempRutaJson);
                if (configuracionValidada != null)
                {
                    // Reemplazar el archivo original con el temporal
                    File.Replace(tempRutaJson, rutaJson, null);
                    Console.WriteLine("Configuración guardada correctamente.");
                }
                else
                {
                    // Eliminar el archivo temporal si la validación falla
                    File.Delete(tempRutaJson);
                    Console.WriteLine("Error en la validación de la configuración.");
                }








                //Console.WriteLine("Configuración guardada correctamente.");
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
                    //options es para agregar el convertidor personalizado 
                    var options = new JsonSerializerOptions
                    {
                        Converters = { new ColorJsonConverter() }
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

    // Convertidor personalizado para la clase Image

    public class ImageToPathJsonConverter : JsonConverter<Image>
    {
        private readonly string directorioBase = AppDomain.CurrentDomain.BaseDirectory;

       
        private string GuardarImagen(Image nuevaImagen, string nombreArchivo)
        {
            string directorioBase = AppDomain.CurrentDomain.BaseDirectory;
            
            string rutaGuardar = Path.Combine(directorioBase, nombreArchivo);

            if(!ValidarImagen(nuevaImagen))
            {
                return null;
            }



            try
            {
                

                // Usar un Bitmap temporal para evitar problemas de bloqueo
                using (var bitmap = new Bitmap(nuevaImagen))
                {
                    bitmap.Save(rutaGuardar, System.Drawing.Imaging.ImageFormat.Png);
                }


                return rutaGuardar;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar la imagen: {ex.Message}");
            }
            


            return null;

        }

        private bool ValidarImagen(Image image)
        {


            // Verifica si la propiedad cambiada es "Logo" (u otra propiedad específica)
            if (image == null)
            {
                return false;
            }

            try
            {
                using (var tempStream = new MemoryStream())
                {
                    image.Save(tempStream, System.Drawing.Imaging.ImageFormat.Png); // Intenta guardar para validar
                }
                return true; // Si no lanza excepción, la imagen es válida
            }
            catch
            {
                return false;
            }


        }

        public static void LiberarImagen(Image imagen)
        {
            try
            {
                imagen?.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al liberar la imagen: {ex.Message}");
            }
        }

        public override void Write(Utf8JsonWriter writer, Image value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteStringValue(string.Empty); // Guardar una cadena vacía si la imagen es null
                return;
            }

            try
            {
                // Ruta donde se guardará el archivo
                string nombreArchivo = "logoGimansio.png";

                // Guardar la imagen (se valida internamente)
                var rutaGuardada = GuardarImagen(value, nombreArchivo);

                if(rutaGuardada != null)
                {
                    // Escribir la ruta relativa en el JSON
                    writer.WriteStringValue(nombreArchivo);

                }
                else
                {
                    writer.WriteStringValue(string.Empty); // Guardar cadena vacía si ocurre un error
                }

            }
            catch (Exception ex)
            {
                //LiberarImagen(value);
                //Console.WriteLine($"write Error al guardar la imagen: {ex.Message}");
                writer.WriteStringValue(string.Empty); // Guardar cadena vacía si ocurre un error (string.Empty)
            }
            
        }


       

        public override Image Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Leer la ruta de la imagen desde el JSON
            string rutaRelativa = reader.GetString();

            if (string.IsNullOrEmpty(rutaRelativa))
            {
                return Resources.logo_deportnet_1; //retorno el logo deportnet si no se pudo leer nada
            }

            // Convertir la ruta relativa en absoluta, ya que se lee solo logoGimansio.png
            string rutaAbsoluta = Path.Combine(directorioBase, rutaRelativa);

            // Cargar la imagen desde el archivo
            if (File.Exists(rutaAbsoluta))
            {
                try
                {
                    return Image.FromFile(rutaAbsoluta);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"read Error al cargar la imagen: {ex.Message}");
                }
            }

            return null;
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

