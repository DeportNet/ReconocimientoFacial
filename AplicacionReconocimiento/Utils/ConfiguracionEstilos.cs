using DeportNetReconocimiento.Modelo;
using DeportNetReconocimiento.Properties;
using DeportNetReconocimiento.SDK;
using System.ComponentModel;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;



namespace DeportNetReconocimiento.Utils
{
    public class ConfiguracionEstilos
    {


        /* - - - - - - General - - - - - */

        [Category("General")]
        [DisplayName("Color de Fondo")]
        [Description("Define el color de fondo principal de la pantalla.")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color ColorFondo
        {

            get => colorFondo;
            set
            {
                if (value.Name == "Transparent")
                {
                    MessageBox.Show(
                    "El color de fondo no puede ser transparente.", // Mensaje
                    "Error de Validación",                                  // Título
                    MessageBoxButtons.OK,                                   // Botones (OK)
                    MessageBoxIcon.Error                                    // Ícono (Error)
                    );
                }
                else
                {
                    colorFondo = value;
                }
            }
        }
        private Color colorFondo;
        
        
        


        [Category("General")]
        [DisplayName("Logo de la pantalla de bienvenida")]
        [Description("Establece el logo que se mostrará en la pantalla de bienvenida. Dimensiones: 710x130. Formatos validos: .png, .jpeg, .jpg, .bmp, .gif, .tiff, .tif o .ico. Admite arrastrar y soltar imagen.")]
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
                if (value < 2 || value > 100)
                {
                    MessageBox.Show(
                    "El tiempo de muestra de datos no puede ser menor a 2 seg ni mayor a 100 seg.", // Mensaje
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
        [DisplayName("Color del texto de mensaje de acceso concedido")]
        [Description("Selecciona el color del texto del mensaje de acceso concedido.")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color ColorMensajeAccesoConcedido { get; set; }

        /* - - - - - Campos de informacion - - - - - - */

      
        [Category("Campos de informacion")]
        [DisplayName("Color de fondo de la informacion del cliente")]
        [Description("Selecciona el color de fondo para el campo donde se muestra la informacion del cliente.")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color FondoColorInformacionCliente
        {

            get => fondoColorInformacionCliente;
            set
            {
                if (value.Name == "Transparent")
                {
                    fondoColorInformacionCliente = ColorFondo;
                    //MessageBox.Show(
                    //"El color de fondo no puede ser transparente.", // Mensaje
                    //"Error de Validación",                                  // Título
                    //MessageBoxButtons.OK,                                   // Botones (OK)
                    //MessageBoxIcon.Error                                    // Ícono (Error)
                    //);
                }
                else
                {
                    fondoColorInformacionCliente = value;
                }
            }
        }
        private Color fondoColorInformacionCliente;



        [Category("Campos de informacion")]
        [DisplayName("Color de texto de la informacion del cliente")]
        [Description("Selecciona el color de texto para el campo donde se muestra la informacion del cliente.")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color TextoColorInformacionCliente
        {

            get => textoColorInformacionCliente;
            set
            {
                if (value.Name == "Transparent")
                {
                    //textoColorInformacionCliente = ColorFondo;
                    MessageBox.Show(
                    "El color de fondo no puede ser transparente.", // Mensaje
                    "Error de Validación",                                  // Título
                    MessageBoxButtons.OK,                                   // Botones (OK)
                    MessageBoxIcon.Error                                    // Ícono (Error)
                    );
                }
                else
                {
                    textoColorInformacionCliente = value;
                }
            }
        }
        private Color textoColorInformacionCliente;

        [Category("Campos de informacion")]
        [DisplayName("Fuente de texto de la informacion del cliente")]
        [Description("Selecciona la fuente de texto para el campo donde se muestra la informacion del cliente.")]
        [JsonConverter(typeof(FontJsonConverter))]
        public Font FuenteTextoInformacionCliente { get; set; }

        [Category("Campos de informacion")]
        [DisplayName("Color de fondo imagen")]
        [Description("Especifica el color de fondo que aparecerá detrás de las imágenes en los campos de información.")]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color ColorFondoImagen { get; set; }

        /* - - - - - - - - Sonidos - - - - - - - - */

        [Category("Sonidos")]
        [DisplayName("Acceso Concedido")]
        [Description("Configuración del sonido cuando se concede acceso al cliente.")]
        public Sonido AccesoConcedido { get; set; }


        [Category("Sonidos")]
        [DisplayName("Sonido inicial")]
        [Description("Configuración del sonido cuando se inicia el programa.")]
        public Sonido SonidoBienvenida { get; set; }

        [Category("Sonidos")]
        [DisplayName("Acceso Denegado")]
        [Description("Configuración del sonido cuando se deniega el acceso al cliente.")]
        public Sonido AccesoDenegado { get; set; }

        [Category("Sonidos")]
        [DisplayName("Sonido de pregunta")]
        [Description("Configuración del sonido cuando se abre un pop up de pregunta")]
        public Sonido SonidoPregunta { get; set; }

        /* - - - - - - Campos de estadísticas - - - - - - */

        [Category("Estadísticas")]
        [DisplayName("Socios registrados")]
        [Description("Cantidad de socios que se encuentran registrados en el dispositivo")]
        [ReadOnly(true)]
        public int CarasRegistradas { get; set; }


        [Category("Estadísticas")]
        [DisplayName("Capacidad del dispositivo")]
        [Description("Cantidad maxima de socios que pueden estar registrados en el dispositivo")]
        [ReadOnly(true)]
        public int CapacidadMaximaDispositivo { get; set; }


        [Category("Estadísticas")]
        [DisplayName("Alerta de capacidad (%)")]
        [Description("Indica en que porcentaje (1 a 100) de capacidad de almacenamiento ocupada, muestra un mensaje de aviso")]
        public float PorcentajeAlertaCapacidad
        {
            get => porcentajeAlertaCapacidad;
            set
            {
                if (value < 0 || value > 100)
                {
                    MessageBox.Show(
                        "El porcentaje debe estar entre el rango de 1 y 100",
                        "Error de validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
                }
                else
                { 
                porcentajeAlertaCapacidad = value;
                }
            }

        }

        private float porcentajeAlertaCapacidad;


        [Browsable(false)]
        public string MetodoApertura { get; set; }


        [Browsable(false)]
        public string RutaMetodoApertura { get; set; }

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
            ColorMensajeBienvenida = Color.Black;
            ColorMensajeAccesoDenegado = Color.Red;
            ColorMensajeAccesoConcedido = Color.Green;
            MensajeBienvenida = "Bienvenido a DeportNet!";

            FuenteTextoMensajeAcceso = new Font("Arial Rounded MT Bold", 36, FontStyle.Italic);

            // Campos de informacion
            TextoColorInformacionCliente = Color.Black;
            FondoColorInformacionCliente = Color.WhiteSmoke;
            FuenteTextoInformacionCliente = new Font("Arial Rounded MT Bold", 20, FontStyle.Regular);
        
            //Imagen
            ColorFondoImagen = Color.DarkGray;


            //Sonidos

            // Sonidos predeterminados
            AccesoConcedido = new Sonido();
            AccesoDenegado = new Sonido();
            SonidoPregunta = new Sonido();
            SonidoBienvenida = new Sonido();



            //Campos de estadísticas
            CarasRegistradas = 1;
            CapacidadMaximaDispositivo = 500;
            PorcentajeAlertaCapacidad = 70.0f;

            //Configuraciónes
            MetodoApertura = ".exe";
            RutaMetodoApertura = "";

        }



        public static void GuardarJsonConfiguracion(ConfiguracionEstilos configuracion)
        {
            string rutaJson = "configuracionEstilos.json";
            //string tempRutaJson = "configuracionEstilos_temp.json";

            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true // JSON legible
                };

                // Serializar la configuración
                string json = JsonSerializer.Serialize(configuracion, options);
                File.WriteAllText(rutaJson, json);

                //// Validar el archivo temporal
                //ConfiguracionEstilos configuracionValidada = LeerJsonConfiguracion(tempRutaJson);
                //if (configuracionValidada != null)
                //{
                //    // Reemplazar el archivo original con el temporal
                //    File.Replace(tempRutaJson, rutaJson, null);
                //    Console.WriteLine("Configuración guardada correctamente.");
                //}
                //else
                //{
                //    // Eliminar el archivo temporal si la validación falla
                //    File.Delete(tempRutaJson);
                //    Console.WriteLine("Error en la validación de la configuración.");
                //}

                ConfiguracionManager.ActualizarConfiguracionDesdeJson("configuracionEstilos");

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
                        Converters = {
                            new ColorJsonConverter(),
                            new FontJsonConverter(),
                            new ImageToPathJsonConverter(),
                        }
                    };

                    // Leer el contenido del archivo
                    string jsonContent = File.ReadAllText($"{rutaJson}.json");

                    // Deserializar el contenido
                    configuracionEstilos = JsonSerializer.Deserialize<ConfiguracionEstilos>(jsonContent, options);

                    Console.WriteLine("Configuración leida correctamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"No se pudo leer el JSON de configuración: {ex.Message}");
                }
            }

            return configuracionEstilos;
        }

        public void sumarRegistroCara()
        {
            
            CarasRegistradas += 1;
            GuardarJsonConfiguracion(this);
        }

        public void restarRegistroCara()
        {
            CarasRegistradas -= 1;
            GuardarJsonConfiguracion(this);
        }

        public void ActualizarCapacidadMaxima()
        {
            int capacidad = Hik_Controladora_General.InstanciaControladoraGeneral.ObtenerCapacidadCarasDispositivo();
            CapacidadMaximaDispositivo = capacidad;
            GuardarJsonConfiguracion(this);
        }

        public static BigInteger EncriptadorToken(BigInteger token)
        {

             token = token * 162 + 12 * 60 * 13 - 100;
            return token;
        }

        public static string DesencriptadorToken(string token)
        {

            string tokenLimpio = token.Replace("@", "");

            BigInteger tokn = BigInteger.Parse(tokenLimpio);   

            tokn = tokn + 100;
            tokn = tokn - (13 * 12 * 60);
            tokn = tokn / 162;

            Console.WriteLine(tokn);
            return tokn.ToString();
        }

    }

    // Convertidor personalizado para la clase Image

    public class ImageToPathJsonConverter : JsonConverter<Image>
    {
        private readonly string directorioBase = AppDomain.CurrentDomain.BaseDirectory;

       

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

        private string? GuardarImagen(Image nuevaImagen, string nombreArchivo)
        {

            string rutaGuardar = Path.Combine(directorioBase, nombreArchivo);
            
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


        public override void Write(Utf8JsonWriter writer, Image value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                Console.WriteLine("Img Es null");
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
            //catch(OutOfMemoryException ex)
            //{
            //    Console.WriteLine($"write Out of memory exception: {ex.Message}");

            //}
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

            string rutaAbsoluta = Path.Combine(directorioBase, rutaRelativa);

            // Verificar si la ruta es válida
            if (string.IsNullOrEmpty(rutaRelativa) || !File.Exists(rutaAbsoluta))
            {
                Console.WriteLine("Img predeterminada");
                return Resources.logo_deportnet_1; //retorno el logo deportnet si no se pudo leer nada
            }

            try
            {

                using(Image image = Image.FromFile(rutaAbsoluta))
                {
                    return new Bitmap(image);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"read Error al cargar la imagen: {ex.Message}");
            }

            return Resources.logo_deportnet_1; //retorno el logo deportnet si no se pudo leer nada

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


    public static class ConfiguracionManager
    {
        public static event Action OnConfiguracionActualizada;

        public static void ActualizarConfiguracionDesdeJson(string jsonPath)
        {
            // Lógica para cargar y aplicar cambios al JSON
            ConfiguracionEstilos configuracion = ConfiguracionEstilos.LeerJsonConfiguracion("configuracionEstilos");

            // Notificar a los suscriptores que la configuración ha cambiado
            OnConfiguracionActualizada?.Invoke();
        }
    }

}

