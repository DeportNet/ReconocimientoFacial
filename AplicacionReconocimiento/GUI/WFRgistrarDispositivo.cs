using DeportNetReconocimiento.SDK;

namespace DeportNetReconocimiento
{
    public partial class WFRgistrarDispositivo : Form
    {
        public bool ignorarCierre = false;

        public WFRgistrarDispositivo()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (textBoxDeviceAddress.Text.Length <= 0 || textBoxDeviceAddress.Text.Length > 128)
            {
                //Properties.Resources.deviceAddressTips
                MessageBox.Show("Ip con longitud incorrecta");
                return;
            }

            int.TryParse(textBoxPort.Text, out int port);
            if (textBoxPort.Text.Length > 5 || port <= 0)
            {
                //Properties.Resources.portTips
                MessageBox.Show("Puerto con longitud incorrecta");
                return;
            }

            if (textBoxUserName.Text.Length > 32 || textBoxUserName.Text.Length < 3)
            {
                //Properties.Resources.usernameAndPasswordTips
                MessageBox.Show("Usuario no puede ser menor a 3 caracteres ni mayor a 32 caracteres");
                return;
            }

            if (textBoxPassword.Text.Length > 16 || textBoxPassword.Text.Length < 3)
            {
                //Properties.Resources.usernameAndPasswordTips
                MessageBox.Show("Contraseña no puede ser menor a 3 caracteres ni mayor a 16 caracteres");
                return;
            }

            int.TryParse(textBoxSucursalID.Text, out int sucursalId);
            if(textBoxSucursalID.Text.Length > 12 || sucursalId <= 0)
            {
                MessageBox.Show("El ID de la sucursal no puede ser mayor a 12 caracteres ni menor a 1");
            }

            Hik_Resultado resultadoLogin = Hik_Controladora_General.InstanciaControladoraGeneral.InicializarPrograma(textBoxUserName.Text, textBoxPassword.Text, textBoxPort.Text, textBoxDeviceAddress.Text);


            if (resultadoLogin.Exito)
            {

                //creamos un arreglo de strings con los datos que recibimos del input
                //ip , puerto, usuario, contraseña, sucursalId
                escribirArchivoCredenciales([textBoxDeviceAddress.Text, textBoxPort.Text, textBoxUserName.Text, textBoxPassword.Text, textBoxSucursalID.Text]);
                ignorarCierre = true;
                this.Close();
                Environment.Exit(0); // 0 indica salida exitosa; otro valor indica error.

            }
            else
            {
                resultadoLogin.MessageBoxResultado("Error al incializar el programa");
            }

        }

        public void escribirArchivoCredenciales(string[] arregloDeDatos)
        {
            //guardamos los datos en un archivo binario
            string rutaArchivo = "credenciales.bin";

            using (BinaryWriter writer = new BinaryWriter(File.Open(rutaArchivo, FileMode.Create)))
            {
                foreach (string dato in arregloDeDatos)
                {
                    writer.Write(dato);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void cerrarFormulario(object sender, FormClosingEventArgs e)
        {
            if (!ignorarCierre)
            {

                var result = MessageBox.Show("¿Estás seguro de que quieres cerrar la aplicación?",
                                             "Confirmación",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Cerrar completamente la aplicación
                    Environment.Exit(0);
                }
                else
                {
                    // Cancelar el cierre
                    e.Cancel = true;
                }
            }
        }

        private void WFRgistrarDispositivo_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
