using DeportNetReconocimiento.Api.Services;
using DeportNetReconocimiento.Properties;
using DeportNetReconocimiento.SDK;
using DeportNetReconocimiento.Utils;

namespace DeportNetReconocimiento
{
    public partial class WFRgistrarDispositivo : Form
    {
        public bool ignorarCierre = false;
        private static WFRgistrarDispositivo? instancia;
        private string[] credencialesLeidas;

        private WFRgistrarDispositivo()
        {
            InitializeComponent();

            if (CredencialesUtils.ExisteArchivoCredenciales())
            {
                credencialesLeidas = CredencialesUtils.LeerCredenciales();
                AgregarValoresAInputs(credencialesLeidas);
            }


        }


        private void AgregarValoresAInputs(string[] credencialesLeidas)
        {
            if (credencialesLeidas != null && credencialesLeidas.Length >= 6 )
            {
                //ip , puerto, usuario, contraseña, sucursalId, tokenSucursal
                textBoxDeviceAddress.Text = credencialesLeidas[0];
                textBoxPort.Text = credencialesLeidas[1];
                textBoxUserName.Text = credencialesLeidas[2];
                textBoxPassword.Text = credencialesLeidas[3];
                textBoxSucursalID.Text = credencialesLeidas[4];
                textBoxTokenSucursal.Text = credencialesLeidas[5];
            }
        }


        public static WFRgistrarDispositivo ObtenerInstancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new WFRgistrarDispositivo();
                }
                return instancia;
            }
        }

        private async void BtnAdd_Click(object sender, EventArgs e)
        {

            if (textBoxDeviceAddress.Text.Length <= 0 || textBoxDeviceAddress.Text.Length > 128)
            {
                //Properties.Resources.deviceAddressTips
                MessageBox.Show("Ip con longitud incorrecta");
                return;
            }

            bool parsePuerto = int.TryParse(textBoxPort.Text, out int port);
            if (!parsePuerto)
            {
                MessageBox.Show("El puerto debe ser numerico");
                return;
            }

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

            bool parseSucursalId = int.TryParse(textBoxSucursalID.Text, out int sucursalId);

            if (!parseSucursalId)
            {
                MessageBox.Show("El ID de la sucursal debe ser numerico");
                return;
            }
            if (textBoxSucursalID.Text.Length > 12 || sucursalId <= 0)
            {
                MessageBox.Show("El ID de la sucursal no puede ser mayor a 12 caracteres ni negativo");
                return;
            }

            if (textBoxTokenSucursal.Text.Length < 1)
            {
                MessageBox.Show("El token de la sucursal es obligatorio");
                return;
            }


            Hik_Resultado conexionDx = await WebServicesDeportnet.TestearConexionDeportnet(textBoxTokenSucursal.Text, textBoxSucursalID.Text);


            if (!conexionDx.Exito)
            {
                conexionDx.MessageBoxResultado("Conexion con Deportnet");
                return;
            }


            Hik_Resultado resultadoLogin = Hik_Controladora_General.InstanciaControladoraGeneral.InicializarPrograma(textBoxUserName.Text, textBoxPassword.Text, textBoxPort.Text, textBoxDeviceAddress.Text);


            if (!resultadoLogin.Exito)
            {
                resultadoLogin.MessageBoxResultado("Error al incializar el programa Hikvision");
                return;
            }


            //creamos un arreglo de strings con los datos que recibimos del input
            //ip , puerto, usuario, contraseña, sucursalId, tokenSucursal
            CredencialesUtils.EscribirArchivoCredenciales([textBoxDeviceAddress.Text, textBoxPort.Text, textBoxUserName.Text, textBoxPassword.Text, textBoxSucursalID.Text, textBoxTokenSucursal.Text]);
            ignorarCierre = true;
            this.Close();
            Environment.Exit(0); // 0 indica salida exitosa; otro valor indica error.

        }



        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void CerrarFormulario(object sender, FormClosingEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxPassword.UseSystemPasswordChar)
            {
                textBoxPassword.UseSystemPasswordChar = false;
                BotonVer1.Image = Resources.hidden1;
            }
            else
            {
                textBoxPassword.UseSystemPasswordChar = true;
                BotonVer1.Image = Resources.eye1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBoxSucursalID.UseSystemPasswordChar)
            {
                textBoxSucursalID.UseSystemPasswordChar = false;
                BotonVer2.Image = Resources.hidden1;
            }
            else
            {
                textBoxSucursalID.UseSystemPasswordChar = true;
                BotonVer2.Image = Resources.eye1;
            }

        }

        private void BotonVer3_Click(object sender, EventArgs e)
        {
            if (textBoxTokenSucursal.UseSystemPasswordChar)
            {
                textBoxTokenSucursal.UseSystemPasswordChar = false;
                BotonVer3.Image = Resources.hidden1;
            }
            else
            {
                textBoxTokenSucursal.UseSystemPasswordChar = true;
                BotonVer3.Image = Resources.eye1;
            }


        }
    }
}
