using DeportnetOffline;
using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Properties;
using DeportNetReconocimiento.Hikvision.SDKHikvision;
using DeportNetReconocimiento.Utils;




namespace DeportNetReconocimiento.GUI
{
    public partial class WFConfiguracion : Form
    {
        private ConfiguracionEstilos configuracion;
        private WFPrincipal principal;
        private Credenciales? _credenciales;

        public WFConfiguracion(ConfiguracionEstilos configuracionEstilos, WFPrincipal principal)
        {
            InitializeComponent();
            this.configuracion = configuracionEstilos;
            this.principal = principal;
            _credenciales = CredencialesUtils.LeerCredencialesBd();
            comboBoxNroLector.SelectedIndex = int.Parse(ConfiguracionGeneralUtils.ObtenerLectorActual()) - 1;


            // Asignar el objeto de configuración al PropertyGrid (para que se vea lo que se puede configurar)
            propertyGrid1.SelectedObject = configuracion;
            ComboBoxAperturaMolinete.SelectedIndexChanged += ComboBoxAperturaMolinete_SelectedIndexChanged;
            checkBox1.Checked = configuracionEstilos.BloquearIp;
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


        // - - - - -  Guardar cambios - - - - - -//

        private void GuardarCambiosButton_Click(object sender, EventArgs e)
        {
            if (PanelConfigAdminsitrador.Visible)
            {
                MessageBox.Show(
                    "Por favor, oculte la configuracion de administrador antes de guardar los cambios.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                return;
            }


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
        }

        private void RefrescarPropertyGrid()
        {
            // Recargar el objeto desde el JSON
            configuracion = ConfiguracionEstilos.LeerJsonConfiguracion();

            // Actualizar el PropertyGrid
            propertyGrid1.SelectedObject = null; // Limpia la referencia
            propertyGrid1.SelectedObject = configuracion; // Asigna el objeto actualizado
            propertyGrid1.Refresh();
        }


        // - - - - -  Drag and Drop de Logo (imagen) - - - - - -//

        #region Drag and Drop de Logo (imagen)
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


        #endregion

        // - - - - - Campo administrador - - - - - //

        private void BotonIngresarAdmin_Click(object sender, EventArgs e)
        {

            ValidarAdministrador(TextBoxAdmin.Text);

        }

        public void ValidarAdministrador(string clave)
        {


            //posicion 3 es la clave del dispositivo, pero usamos la misma
            if (clave == _credenciales.Password)
            {
                PanelConfigAdminsitrador.Visible = true;

                //TextBoxIdSucursal.Text = _credenciales[4];
                //textBoxTokenSucursal.Text = _credenciales[5];

                ComboBoxAperturaMolinete.SelectedItem = configuracion.MetodoApertura;
                TextBoxRutaExe.Text = configuracion.RutaMetodoApertura;

            }
            TextBoxAdmin.Text = "";
        }

        private void BotonOcultarConfig_Click(object sender, EventArgs e)
        {

            configuracion.MetodoApertura = ComboBoxAperturaMolinete.Text;
            configuracion.RutaMetodoApertura = TextBoxRutaExe.Text;

            ConfiguracionEstilos.GuardarJsonConfiguracion(configuracion);
            principal.AplicarConfiguracion(configuracion);

            PanelConfigAdminsitrador.Visible = false;

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

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (TextBoxAdmin.UseSystemPasswordChar)
            {
                TextBoxAdmin.UseSystemPasswordChar = false;
                button1.Image = Resources.hidden1;
            }
            else
            {
                TextBoxAdmin.UseSystemPasswordChar = true;
                button1.Image = Resources.eye1;
            }
        }

        private void botonEditarCredenciales_Click(object sender, EventArgs e)
        {
            WFRgistrarDispositivo dialogo = WFRgistrarDispositivo.ObtenerInstancia;
            dialogo.tipoApertura = 2;
            dialogo.ShowDialog();

        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            configuracion.CambiarEstadoBloqueoIp();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxNroLector_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Se obtiene la parte del final, que es el nro de lector, "Lector 1"

            string[] partes = comboBoxNroLector.Text.Split(' ');

            string lectorActual = partes[^1]; // "Lector 1" -> "1"  se puede hacer tambien como partes.length -1

            ConfiguracionGeneralUtils.ActualizarLectorActual(lectorActual);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WFDeportnetOffline wFDeportnetOffline = WFDeportnetOffline.ObtenerInstancia;

            if (wFDeportnetOffline.Visible)
            {
                wFDeportnetOffline.BringToFront();
                return;
            }

            wFDeportnetOffline.Show();

        }
    }
}
