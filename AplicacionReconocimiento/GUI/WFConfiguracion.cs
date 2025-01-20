
using DeportNetReconocimiento.Utils;
using System.Numerics;


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
            ComboBoxAperturaMolinete.SelectedIndexChanged += ComboBoxAperturaMolinete_SelectedIndexChanged;
            ConfiguracionManager.OnConfiguracionActualizada += RefrescarPropertyGrid;

        }

        private void ComboBoxAperturaMolinete_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (ComboBoxAperturaMolinete.SelectedItem.ToString() == ".exe")
            {
                TextBoxRutaExe.Enabled = true;
                BotonAbrirFileDialog.Enabled = true;
            }
            else
            {
                TextBoxRutaExe.Enabled = false;
                BotonAbrirFileDialog.Enabled = false;
                TextBoxRutaExe.Clear();
            }
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

        }
        private void WFConfiguracion_FormClosing(object sender, FormClosingEventArgs e)
        {
            //TODO: modal con preguntar si desear salir sin guardar cambios
            //ConfiguracionEstilos.GuardarJsonConfiguracion(configuracion);
            ConfiguracionManager.OnConfiguracionActualizada -= RefrescarPropertyGrid;

        }


        private void PropertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {

            principal.AplicarConfiguracion(configuracion);
            ConfiguracionEstilos.GuardarJsonConfiguracion(configuracion);
            propertyGrid1.Refresh();

        }

        private void RefrescarPropertyGrid()
        {
            // Recargar el objeto desde el JSON
            configuracion = ConfiguracionEstilos.LeerJsonConfiguracion("configuracionEstilos");

            // Actualizar el PropertyGrid
            propertyGrid1.SelectedObject = null; // Limpia la referencia
            propertyGrid1.SelectedObject = configuracion; // Asigna el objeto actualizado
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
                MessageBox.Show("Los datos arrastrados no son archivos válidos.",
                     "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                return;
            }

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            //solo un archivo
            if (files.Length != 1)
            {
                MessageBox.Show("Por favor, arrastra solo una imagen.",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );

                return;
            }

            string filePath = files[0]; //se obtiene la ruta del archivo

            // Verifica si el archivo tiene una extensión de imagen válida
            string[] extensionesValidas = { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff", ".tif", ".ico" };
            if (!Array.Exists(extensionesValidas, ext => filePath.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show(
                    "Por favor, arrastra un archivo de imagen válido (.jpg, .png, .jpeg, .bmp, .gif, .tiff, .tif o .ico).", 
                    "Error de tipo de imagen",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                return;
            }

            //Limitamos a 100MB el tamaño de la imagen
            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Length > 100 * 1024 * 1024)
            {
                MessageBox.Show(
                    "El archivo es demasiado grande, limite 100 MB.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                return;
            }


            // Intenta cargarlo como imagen
            try
            {

                configuracion.Logo = Image.FromFile(filePath); // Carga la imagen en memoria 


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

                if (files != null && files.Length == 1)
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



        // - - - - - Campo administrador - - - - - //
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BotonIngresarAdmin_Click(object sender, EventArgs e)
        {

            ValidarAdministrador(TextBoxAdmin.Text);

        }

        public void ValidarAdministrador(string clave)
        {
            string[] credenciales = CredencialesUtils.LeerCredenciales();
           
            //posicion 3 es la clave del dispositivo, pero usamos la misma
            if (clave == credenciales[3])
            {
                PanelConfigAdminsitrador.Visible = true;

                if (CredencialesUtils.LeerCredenciales().Length > 4)
                {
                    TextBoxToken.Text = credenciales[4]; //ConfiguracionEstilos.DesencriptadorToken(WFPrincipal.ObtenerInstancia.LeerCredenciales()[4]);
                }


                //string ruta = "configuracionEstilos";
                //ConfiguracionEstilos configuracion = new ConfiguracionEstilos();
                //configuracion = ConfiguracionEstilos.LeerJsonConfiguracion(ruta);


                ComboBoxAperturaMolinete.SelectedItem = configuracion.MetodoApertura;
                TextBoxRutaExe.Text = configuracion.RutaMetodoApertura;

            }
            TextBoxAdmin.Text = "";
        }

        private void BotonOcultarConfig_Click(object sender, EventArgs e)
        {
            PanelConfigAdminsitrador.Visible = false;
            string apertura = ComboBoxAperturaMolinete.Text;
            string rutaApertura = TextBoxRutaExe.Text;
            
            configuracion.MetodoApertura = apertura;
            configuracion.RutaMetodoApertura = rutaApertura;


            ConfiguracionEstilos.GuardarJsonConfiguracion(configuracion);
            principal.AplicarConfiguracion(configuracion);

            try
            {
                //BigInteger token = ConfiguracionEstilos.EncriptadorToken(BigInteger.Parse(TextBoxToken.Text));
                GuardarTokenCredenciales(TextBoxToken.Text);

            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(
                    "El numero ingresado debe ser de tipo numero", // Mensaje
                    "Error de Formato",                                  // Título
                    MessageBoxButtons.OK,                                   // Botones (OK)
                    MessageBoxIcon.Error                                    // Ícono (Error)
                    );

            }



        }

        public void EscribirArchivoCredenciales(string[] arregloDeDatos)
        {
            //guardamos los datos en un archivo binario
            string rutaArchivo = "credenciales.bin";

            using (BinaryWriter writer = new BinaryWriter(File.Open(rutaArchivo, FileMode.Truncate)))
            {
                foreach (string dato in arregloDeDatos)
                {
                    writer.Write(dato);
                }
            }
        }

        public void GuardarTokenCredenciales(string token)
        {

            // Obtenemos las credenciales
            string[] credenciales = CredencialesUtils.LeerCredenciales();
            EscribirArchivoCredenciales([credenciales[0], credenciales[1], credenciales[2], credenciales[3], token.ToString()]);

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        //Boton tres puntos
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Todos los archivos (*.*)|*.*"; // Filtro opcional
                openFileDialog.Title = "Selecciona un archivo";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Muestra la ruta seleccionada en el TextBox
                    TextBoxRutaExe.Text = openFileDialog.FileName;
                }
            }
        }

        private void PanelConfigAdminsitrador_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TextBoxRutaExe_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
