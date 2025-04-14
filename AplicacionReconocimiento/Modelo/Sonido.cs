using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms.Design;
using static DeportNetReconocimiento.Modelo.BooleanToggleEditor;

namespace DeportNetReconocimiento.Modelo
{
    //Esto para que aparezca como un objeto expandible en el PropertyGrid
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Sonido
    {
        [DisplayName("Ruta del archivo")]
        [Description("Seleccione el archivo de sonido.")]
        [Editor(typeof(SoundFileEditor), typeof(UITypeEditor))]
        public string RutaArchivo { get; set; }



        [Category("Configuración")]
        [DisplayName("Estado")]
        [Description("Indica si el sonido está activado o no.")]
        [Editor(typeof(BooleanToggleEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(BooleanToActivoInactivoConverter))]
        public bool Estado { get; set; } = true;


        public Sonido()
        {
            this.RutaArchivo = "";
            this.Estado = false;
        }

        public Sonido(string rutaArchivo) 
        { 
            this.RutaArchivo = rutaArchivo;
            this.Estado = true;
        }

        private string? GuardarSonidoEnDirectorio(string rutaArchivoOriginal)
        {
            // Obtener el directorio actual de la aplicación
            string directorioActual = AppDomain.CurrentDomain.BaseDirectory;

            // Crear el directorio "sonidos" si no existe
            string directorioSonidos = Path.Combine(directorioActual, "sonidos");
            if (!Directory.Exists(directorioSonidos))
            {
                Directory.CreateDirectory(directorioSonidos);
            }

            try
            {
                // Obtener el nombre del archivo
                string nombreArchivo = Path.GetFileName(rutaArchivoOriginal);

                // Ruta completa donde se guardará el archivo
                string rutaDestino = Path.Combine(directorioSonidos, nombreArchivo);

                // Verificar si el archivo ya existe y reemplazarlo
                if (File.Exists(rutaDestino))
                {
                    File.Delete(rutaDestino);
                }

                // Copiar el archivo al directorio "sonidos"
                File.Copy(rutaArchivoOriginal, rutaDestino);

                // Retornar la ruta relativa
                return Path.Combine("sonidos", nombreArchivo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el sonido: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }


        public override string ToString()
        {
            string mostrarActivo = Estado ? "Activo" : "Inactivo";
            string mostrarNombreArchivo = string.IsNullOrEmpty(RutaArchivo) ? "No seleccionado" : Path.GetFileName(RutaArchivo);


            return $"Sonido: {mostrarNombreArchivo}; Estado: {mostrarActivo}";
        }


    }


    public class SoundFileEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            // Define que este editor mostrará un diálogo modal
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                var editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

                if (editorService != null)
                {
                    // Crear y configurar el diálogo de selección de archivos
                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Filter = "Archivos de Sonido|*.mp3;*.wav;*.wma;*.aac;*.flac;*.ogg|Todos los archivos|*.*";
                        openFileDialog.Title = "Seleccionar Archivo de Sonido";

                        // Si ya hay un valor asignado, úsalo como ruta inicial
                        if (value is string currentPath && !string.IsNullOrEmpty(currentPath))
                        {
                            openFileDialog.FileName = currentPath;
                        }

                        // Mostrar el diálogo y devolver el archivo seleccionado
                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            return openFileDialog.FileName;
                        }
                    }
                }
            }

            // Si no se selecciona ningún archivo, devolver el valor original
            return value;
        }
    }


    public class BooleanToggleEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            // Define el estilo como DropDown
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            // Abre un editor de valores booleanos (Activo/Inactivo)
            if (provider.GetService(typeof(IWindowsFormsEditorService)) is IWindowsFormsEditorService editorService)
            {
                // Crea un control de tipo ListBox para alternar los valores
                var listBox = new ListBox();
                listBox.Items.Add("Activo");
                listBox.Items.Add("Inactivo");

                // Selecciona el estado actual
                listBox.SelectedItem = (bool)value ? "Activo" : "Inactivo";

                // Maneja la selección
                listBox.SelectedValueChanged += (sender, args) =>
                {
                    value = listBox.SelectedItem.ToString() == "Activo";
                    editorService.CloseDropDown();
                };

                // Muestra el control
                editorService.DropDownControl(listBox);
            }

            return value;
        }

        public class BooleanToActivoInactivoConverter : BooleanConverter
        {
            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string))
                {
                    return (bool)value ? "Activo" : "Inactivo";
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                if (value is string strValue)
                {
                    if (strValue == "Activo") return true;
                    if (strValue == "Inactivo") return false;
                }
                return base.ConvertFrom(context, culture, value);
            }
        }
    }
}
