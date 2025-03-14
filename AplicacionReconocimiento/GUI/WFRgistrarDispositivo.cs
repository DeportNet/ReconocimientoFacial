using DeportNetReconocimiento.Api.Services;
using DeportNetReconocimiento.GUI;
using DeportNetReconocimiento.Properties;
using DeportNetReconocimiento.SDK;
using DeportNetReconocimiento.Utils;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace DeportNetReconocimiento
{
    public partial class WFRgistrarDispositivo : Form
    {
        public bool ignorarCerrarPrograma = false;
        private static WFRgistrarDispositivo? instancia;
        private string[] credencialesLeidas;
        private bool inputsValidos = false;
        private Loading loading;
        private WFRgistrarDispositivo()
        {
            InitializeComponent();
            this.loading = new Loading();
        }


        private void AgregarValoresAInputs(string[] credencialesLeidas)
        {


            if (credencialesLeidas != null && credencialesLeidas.Length >= 6)
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

        private void WFRgistrarDispositivo_Load(object sender, EventArgs e)
        {
            if (CredencialesUtils.ExisteArchivoCredenciales())
            {
                AgregarValoresAInputs(CredencialesUtils.LeerCredenciales());
                ignorarCerrarPrograma = true;
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


        private bool ValidarInputs(bool buscarDisp= false)
        {
            bool flag = false;

            //si queremos buscar el disp, podemos obviar estas validaciones
            if (!buscarDisp)
            {
                if (string.IsNullOrEmpty(textBoxDeviceAddress.Text) ||
                    string.IsNullOrWhiteSpace(textBoxDeviceAddress.Text)
                    || textBoxDeviceAddress.Text.Length > 128 )
                {
                    //Properties.Resources.deviceAddressTips
                    MessageBox.Show("Ip del dispositivo invalida");
                    return flag;
                }

                bool parseSucursalId = int.TryParse(textBoxSucursalID.Text, out int parseSucursalIdOut);

                if (!parseSucursalId)
                {
                    MessageBox.Show("El ID de la sucursal debe ser numerico");
                    return flag;
                }
                if (textBoxSucursalID.Text.Length > 12 || parseSucursalIdOut <= 0)
                {
                    MessageBox.Show("El ID de la sucursal no puede ser mayor a 12 caracteres ni negativo");
                    return flag;
                }

                if (textBoxTokenSucursal.Text.Length < 1)
                {
                    MessageBox.Show("El token de la sucursal es obligatorio");
                    return flag;
                }

            }


            bool parsePuerto = int.TryParse(textBoxPort.Text, out int parsePuertoOut);
            if (!parsePuerto)
            {
                MessageBox.Show("El puerto debe ser numerico");
                return flag;
            }

            if (textBoxPort.Text.Length > 5 || parsePuertoOut <= 0)
            {
                //Properties.Resources.portTips
                MessageBox.Show("Puerto con longitud incorrecta");
                return flag;
            }

            if (textBoxUserName.Text.Length > 32 || textBoxUserName.Text.Length < 3)
            {
                //Properties.Resources.usernameAndPasswordTips
                MessageBox.Show("Usuario no puede ser menor a 3 caracteres ni mayor a 32 caracteres");
                return flag;
            }

            if (textBoxPassword.Text.Length > 16 || textBoxPassword.Text.Length < 3)
            {
                //Properties.Resources.usernameAndPasswordTips
                MessageBox.Show("Contraseña no puede ser menor a 3 caracteres ni mayor a 16 caracteres");
                return flag;
            }

            //si pasa todos los filtros 
            flag = true;

            return flag;
        }

        private bool seClickeoBotonLogin = false;

        private async void BtnAdd_Click(object sender, EventArgs e)
        {

            if (!ValidarInputs() || loading.Visible || seClickeoBotonLogin)
            {
                return;
            }
            seClickeoBotonLogin = true;

            Hik_Resultado resultadoLogin = Hik_Controladora_General.InstanciaControladoraGeneral.InicializarPrograma(textBoxUserName.Text, textBoxPassword.Text, textBoxPort.Text, textBoxDeviceAddress.Text);

            if (!resultadoLogin.Exito)
            {
                resultadoLogin.MessageBoxResultado("Error al incializar el dispositivo Hikvision");
                return;
            }

            Hik_Resultado conexionDx = await WebServicesDeportnet.TestearConexionDeportnet(textBoxTokenSucursal.Text, textBoxSucursalID.Text);


            if (!conexionDx.Exito)
            {
                conexionDx.MessageBoxResultado("Conexion con Deportnet");
                return;
            }

            //creamos un arreglo de strings con los datos que recibimos del input
            //ip , puerto, usuario, contraseña, sucursalId, tokenSucursal
            CredencialesUtils.EscribirArchivoCredenciales([textBoxDeviceAddress.Text, textBoxPort.Text, textBoxUserName.Text, textBoxPassword.Text, textBoxSucursalID.Text, textBoxTokenSucursal.Text]);

            ignorarCerrarPrograma = true;
            seClickeoBotonLogin = false;
            this.Close();


            //WFPrincipal.ObtenerInstancia.Show();
            //LevantarWFPrincipal();
            //Environment.Exit(0); 

        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void CerrarFormulario(object sender, FormClosingEventArgs e)
        {
            if (!ignorarCerrarPrograma)
            {

                var result = MessageBox.Show("¿Estás seguro de que quieres cerrar la aplicación?",
                                             "Confirmación",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Cerrar completamente la aplicación
                    Environment.Exit(0); // 0 indica salida exitosa; otro valor indica error.
                }
                else
                {
                    // Cancelar el cierre
                    e.Cancel = true;
                }
            }
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



        private async void botonBuscarIp_Click(object sender, EventArgs e)
        {
            bool buscarDisp = true;
            if (!ValidarInputs(buscarDisp) || loading.Visible)
            {
                return;
            }
            loading.Show();
            
            Hik_Resultado resultadoLogin = await Task.Run(()=> BuscadorIpDispositivo.ObtenerIpDispositivo(textBoxPort.Text, textBoxUserName.Text, textBoxPassword.Text));
            
            loading.Hide();


            if (!resultadoLogin.Exito)
            {
                resultadoLogin.MessageBoxResultado("Error al incializar el dispositivo Hikvision");
                return;
            }

            textBoxDeviceAddress.Text = resultadoLogin.Mensaje;
        }
    }
}
