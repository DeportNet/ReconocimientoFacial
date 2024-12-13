using DeportNetReconocimiento.Properties;
using DeportNetReconocimiento.Utils;
using System.Text.Json;
using System.Windows.Forms;
using System.Xml;


namespace DeportNetReconocimiento.GUI
{
    public partial class WFConfiguracion : Form
    {
        private ConfiguracionEstilos configuracion;
        private WFPrincipal principal;


        public WFConfiguracion(ConfiguracionEstilos configuracionEstilos, WFPrincipal principal)
        {
            InitializeComponent();
            this.configuracion = configuracionEstilos;
            this.principal = principal;

            // Asignar el objeto de configuración al PropertyGrid (para que se vea lo que se puede configurar)
            propertyGrid1.SelectedObject = configuracion;
        }

        //propiedades



        private void WFConfiguracion_Load(object sender, EventArgs e)
        {

        }

        private void PropertyGrid1_Click(object sender, EventArgs e)
        {

        }
        
        // - - - - -  Guardar cambios - - - - - -//

        private void GuardarCambiosButton_Click(object sender, EventArgs e)
        {
            this.Close();
            principal.AplicarConfiguracion(configuracion);


            ConfiguracionEstilos.GuardarJsonConfiguracion(configuracion);
        }

        private void WFConfiguracion_FormClosing(object sender, FormClosingEventArgs e)
        {



            //TODO: modal con preguntar si desear salir sin guardar cambios
            ConfiguracionEstilos.GuardarJsonConfiguracion(configuracion);
        }


        private void PropertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            
           
                principal.AplicarConfiguracion(configuracion);

                ConfiguracionEstilos.GuardarJsonConfiguracion(configuracion);

                propertyGrid1.Refresh();

          

        }


        // - - - - -  Drag and Drop de Logo (imagen) - - - - - -//


        



        private void PropertyGrid1_DragDrop(object sender, DragEventArgs e)
        {

         
            if (e.Data == null)
            {
                return;
            }


            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                MessageBox.Show("Los datos arrastrados no son archivos válidos.");
                return;
            }

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            
            //solo un archivo
            if (files.Length != 1)
            {
                MessageBox.Show("Por favor, arrastra solo una imagen.");
                return;
            }

            string filePath = files[0]; //se obtiene la ruta del archivo

            // Verifica si el archivo tiene una extensión de imagen válida
            string[] extensionesValidas = { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff", ".tif", ".ico" };
            if (!Array.Exists(extensionesValidas, ext => filePath.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Por favor, arrastra un archivo de imagen válido (.jpg, .png, .jpeg, .bmp, .gif, .tiff, .tif o .ico).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Limitamos a 100MB el tamaño de la imagen
            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Length > 100 * 1024 * 1024) 
            {
                MessageBox.Show("El archivo es demasiado grande, limite 100 MB.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            // Intenta cargarlo como imagen
            try
            {
                Console.WriteLine("Cargando imagen");
                configuracion.Logo = Image.FromFile(filePath); // Carga la imagen en memoria 
                
                Console.WriteLine(filePath);
                // Notificar a la ventana principal que debe actualizarse
                principal.AplicarConfiguracion(configuracion);
                

                // Refrescar el PropertyGrid
                propertyGrid1.Refresh();
                

            }
            catch (ArgumentException)
            {
                MessageBox.Show("El archivo no contiene una imagen válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("El archivo no es una imagen válida o está corrupto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la imagen //: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }

            

        }



        private void PropertyGrid1_DragEnter(object sender, DragEventArgs e)
        {
            //TODO: poner panel que se active y diga "suelta la imagen aqui"
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if(files != null && files.Length == 1)
                {

                    // Verifica que al menos uno de los archivos tenga una extensión válida
                    string[] extensionesValidas = { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff", ".tif", ".ico" };

                    if (Array.Exists(extensionesValidas, ext => files[0].EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
                    {
                        
                        e.Effect = DragDropEffects.Copy; // Permitir el arrastre
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void PropertyGrid1_DragLeave(object sender, EventArgs e)
        {
            
        }





        }
}
