using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Repository;
using DeportNetReconocimiento.Api.Services;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Windows.Foundation.Metadata;


namespace DeportnetOffline.GUI.Modales
{
    public partial class ModalNuevoLegajo : Form
    {
        public ModalNuevoLegajo()
        {
            InitializeComponent();

        }


        private void modalNuevoLegajo_Load(object sender, EventArgs e)
        {
            textBoxNombre_Leave(this, EventArgs.Empty);
            textBoxApellido_Leave(this, EventArgs.Empty);
            textBoxNroTarjeta_Leave(this, EventArgs.Empty);
            textBoxTelefono_Leave(this, EventArgs.Empty);
            textBoxDireccionConPiso_Leave(this, EventArgs.Empty);
            textBoxDireccion_Leave(this, EventArgs.Empty);
            textBoxEmail_Leave(this, EventArgs.Empty);
            comboBoxGenero.SelectedIndex = 0;
        }

        // Eventos de nombre

        private void textBoxNombre_Enter(object sender, EventArgs e)
        {
            if (textBoxNombre.Text == "Nombre")
            {
                textBoxNombre.Text = "";
                textBoxNombre.ForeColor = Color.Black; // Color del texto cuando el usuario escribe
            }

        }

        private void textBoxNombre_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxNombre.Text))
            {
                textBoxNombre.Text = "Nombre";
                textBoxNombre.ForeColor = Color.Gray; // Color del placeholder
            }
        }

        private void textBoxNombre_Validating(object sender, CancelEventArgs e)
        {

            ValidarCampo(textBoxNombre, labelErrorNombre, EsTextoValido, "Nombre", true);

        }
        // Eventos de Apellido


        private void textBoxApellido_Enter(object sender, EventArgs e)
        {
            if (textBoxApellido.Text == "Apellido")
            {
                textBoxApellido.Text = "";
                textBoxApellido.ForeColor = Color.Black; // Color del texto cuando el usuario escribe
            }
        }

        private void textBoxApellido_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxApellido.Text))
            {
                textBoxApellido.Text = "Apellido";
                textBoxApellido.ForeColor = Color.Gray; // Color del placeholder
            }
        }

        private void textBoxApellido_Validating(object sender, CancelEventArgs e)
        {

            ValidarCampo(textBoxApellido, labelErrorApellido, EsTextoValido, "Apellido", true);

        }

        // Eventos de Email


        private void textBoxEmail_Enter(object sender, EventArgs e)
        {
            if (textBoxEmail.Text == "Email")
            {
                textBoxEmail.Text = "";
                textBoxEmail.ForeColor = Color.Black; // Color del texto cuando el usuario escribe
            }
        }

        private void textBoxEmail_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEmail.Text))
            {
                textBoxEmail.Text = "Email";
                textBoxEmail.ForeColor = Color.Gray; // Color del placeholder
                labelEmailError.Visible = false;
            }
        }

        private void textBoxEmail_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxEmail.Text == "Email")
            {
                return;
            }

            ValidarCampo(textBoxEmail, labelEmailError, EsEmailValido, "Email", true);

        }


        // Eventos de Telefono


        private void textBoxTelefono_Enter(object sender, EventArgs e)
        {
            if (textBoxTelefono.Text == "Telefono")
            {
                textBoxTelefono.Text = "";
                textBoxTelefono.ForeColor = Color.Black; // Color del texto cuando el usuario escribe
            }
        }

        private void textBoxTelefono_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxTelefono.Text))
            {
                textBoxTelefono.Text = "Telefono";
                textBoxTelefono.ForeColor = Color.Gray; // Color del placeholder
                labelTelefonoError.Visible = false;
            }
        }

        private void textBoxTelefono_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxTelefono.Text == "Telefono")
            {
                return;
            }

            ValidarCampo(textBoxTelefono, labelTelefonoError, EsNumeroValido, "Telefono", false);

        }


        // Eventos de Direccion

        private void textBoxDireccion_Enter(object sender, EventArgs e)
        {
            if (textBoxDireccion.Text == "Direccion")
            {
                textBoxDireccion.Text = "";
                textBoxDireccion.ForeColor = Color.Black; // Color del texto cuando el usuario escribe
            }
        }

        private void textBoxDireccion_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDireccion.Text))
            {
                textBoxDireccion.Text = "Direccion";
                textBoxDireccion.ForeColor = Color.Gray; // Color del placeholder
                labelDireccionError.Visible = false;
            }
        }
        private void textBoxDireccionConPiso_Enter(object sender, EventArgs e)
        {
            if (textBoxPiso.Text == "Dep")
            {
                textBoxPiso.Text = "";
                textBoxPiso.ForeColor = Color.Black; // Color del texto cuando el usuario escribe
            }
        }


        private void textBoxDireccionConPiso_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxPiso.Text))
            {
                textBoxPiso.Text = "Dep";
                textBoxPiso.ForeColor = Color.Gray; // Color del placeholder
                labelPisoDepartamentoError.Visible = false;
            }
        }

        private void textBoxDireccion_Validating(object sender, CancelEventArgs e)
        {

            ValidarCampo(textBoxDireccion, labelDireccionError, EsLetrasNumerosEspaciosValido, "Dep", false);

        }

        private void textBoxPiso_Validating(object sender, CancelEventArgs e)
        {

            ValidarCampo(textBoxPiso, labelPisoDepartamentoError, EsLetrasNumerosEspaciosValido, "Direccion", false);

        }

        // Eventos de Tarjeta

        private void textBoxNroTarjeta_Enter(object sender, EventArgs e)
        {
            if (textBoxNroTarjeta.Text == "Tarjeta")
            {
                textBoxNroTarjeta.Text = "";
                textBoxNroTarjeta.ForeColor = Color.Black; // Color del texto cuando el usuario escribe
            }
        }

        private void textBoxNroTarjeta_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxNroTarjeta.Text))
            {
                textBoxNroTarjeta.Text = "Tarjeta";
                textBoxNroTarjeta.ForeColor = Color.Gray; // Color del placeholder
                labelNroTarjetaError.Visible = false;
            }
        }
        private void textBoxNroTarjeta_Validating(object sender, CancelEventArgs e)
        {

            ValidarCampo(textBoxNroTarjeta, labelNroTarjetaError, EsNumeroValido, "Tarjeta", false);


        }


        //Eventos de selector de fecha

        private void dateTimePickerFechaNacimiento_Validating(object sender, CancelEventArgs e)
        {

            ValidarFecha();
            
        }

        private async void buttonGuardarLegajo_Click(object sender, EventArgs e)
        {
            if (ValidarTodosLosCampos())
            {
                Socio socio = ObtenerSocio();
                socio = FormatearSocio(socio);
                bool resultado = await SocioRepository.InsertarUnSocioEnTabla(socio);
                LimpiarLabels();
                MessageBox.Show("Socio agregado con exito", "Socio registrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine(resultado ? $"El socio  {socio.FirstName + ' ' + socio.LastName} se insertó correctamente en la base de datos" : $"Error al insertar al socio {socio.FirstName + ' ' + socio.LastName} en la base de datos");
                this.Close();
            }
        }


        public Socio FormatearSocio(Socio socio)
        {
            socio.FirstName = MayusculaEnNombres(socio.FirstName);
            socio.LastName = MayusculaEnNombres(socio.LastName);
            socio.Address = MayusculaEnNombres(socio.Address);

            if(socio.Address == "Direccion")
                socio.Address = "";
            if (socio.Cellphone == "Telefono")
                socio.Cellphone = "";
            if (socio.AddressFloor == "Dep")
                socio.AddressFloor = "";
            if (socio.CardNumber == "Tarjeta")  
                socio.CardNumber = "";

            return socio;
        }
       
        private string MayusculaEnNombres(string nombre)
        {
            string[] nombres = nombre.ToLower().Split(" ");

            for(int i = 0; i < nombres.Length; i++)
            {
                nombres[i] = char.ToUpper(nombres[i][0]) + nombres[i].Substring(1);
            }

            return string.Join(" ", nombres);
        }

        public Socio ObtenerSocio()
        {
            return new Socio
            {
                FirstName = textBoxNombre.Text,
                LastName = textBoxApellido.Text,
                Gender = comboBoxGenero.SelectedItem.ToString(),
                BirthDate = dateTimePickerFechaNacimiento.Value,
                Email = textBoxEmail.Text,
                Cellphone = textBoxTelefono.Text,
                Address = textBoxDireccion.Text,
                AddressFloor = textBoxPiso.Text,
                CardNumber = textBoxNroTarjeta.Text
            };
        }

        private void LimpiarLabels()
        {
            textBoxPiso.Text = "";
            textBoxTelefono.Text = "";
            textBoxApellido.Text = "";
            textBoxNombre.Text = "";
            textBoxEmail.Text = "";
            textBoxNroTarjeta.Text = "";
            textBoxDireccion.Text = "";
            dateTimePickerFechaNacimiento.Value = DateTime.Now;
            comboBoxGenero.SelectedIndex = 0;
        }




        //Validaciones 

        private bool ValidarTodosLosCampos()
        {
            bool esValido = true;

            esValido &= ValidarCampo(textBoxNombre, labelErrorNombre, EsTextoValido, "Nombre", true);
            esValido &= ValidarCampo(textBoxApellido, labelErrorApellido, EsTextoValido, "Apellido", true);
            esValido &= ValidarCampo(textBoxEmail, labelEmailError, EsEmailValido, "Email", true);
            esValido &= ValidarCampo(textBoxTelefono, labelTelefonoError, EsNumeroValido, "Telefono", false);
            esValido &= ValidarCampo(textBoxDireccion, labelDireccionError, EsLetrasNumerosEspaciosValido, "Direccion", false);
            esValido &= ValidarCampo(textBoxPiso, labelPisoDepartamentoError, EsLetrasNumerosEspaciosValido, "Dep", false);
            esValido &= ValidarCampo(textBoxNroTarjeta, labelNroTarjetaError, EsNumeroValido, "Tarjeta", false);
            esValido &= ValidarFecha();

            return esValido;
        }
        private bool ValidarCampo(TextBox textBox, Label labelError, Func<string, bool> funcionValidacion, string placeholder, bool requerido)
        {

            if(string.IsNullOrWhiteSpace(textBox.Text) || textBox.Text == placeholder && requerido == false) {
                labelError.Visible = false;
                Console.WriteLine("Entro en la validacion 1");
                return true;
            }

            if (string.IsNullOrWhiteSpace(textBox.Text) || !funcionValidacion(textBox.Text) || textBox.Text == placeholder)
            {
                labelError.Visible = true;
                Console.WriteLine("Entro en la validacion 2");

                return false;
            }
            else
            {
                labelError.Visible = false;
                Console.WriteLine("Entro en la validacion 3");

                return true;
            }
        }




        private bool ValidarFecha()
        {
            bool esValido = true;
            if (!EsFechaValida(dateTimePickerFechaNacimiento.Value))
            {
                labelFechaNacimientoError.Visible = true;
                esValido = false;
            }
            else
            {
                labelFechaNacimientoError.Visible = false;
            }

            return esValido;
        }


        private bool EsEmailValido(string email)
        {
            string patron = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, patron);
        }

        private bool EsFechaValida(DateTime fecha)
        {
            DateTime fechaActual = DateTime.Today;
            return fechaActual > fecha;
        }

        private bool EsTextoValido(string texto)
        {
            string patron = @"^[a-zA-Z\s]+$";
            return Regex.IsMatch(texto, patron);
        }

        private bool EsNumeroValido(string texto)
        {
            string patron = @"^\d+$"; // Solo números
            return Regex.IsMatch(texto, patron);
        }

        private bool EsLetrasNumerosEspaciosValido(string texto)
        {
            string patron = @"^[a-zA-Z0-9\s]+$";
            return Regex.IsMatch(texto, patron);

        }





    }
}
