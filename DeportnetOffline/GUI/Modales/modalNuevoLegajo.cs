using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Repository;
using DeportNetReconocimiento.Api.Services;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Text.RegularExpressions;


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
            if (string.IsNullOrEmpty(textBoxNombre.Text))
            {
                return;
            }

            if (!EsTextoValido(textBoxNombre.Text))
            {
                MessageBox.Show("El nombre no puede contener números", "Nombre invalido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxNombre.Text = "";
                textBoxNombre_Leave(this, EventArgs.Empty);
            }
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
            if (string.IsNullOrEmpty(textBoxApellido.Text))
            {
                return;
            }
            if (!EsTextoValido(textBoxApellido.Text))
            {
                MessageBox.Show("El apellido no puede contener números", "Apellido invalido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxApellido.Text = "";
                textBoxApellido_Leave(this, EventArgs.Empty);
            }

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
            }
        }



        private void textBoxEmail_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxEmail.Text == "Email")
            {
                return;
            }
            if (string.IsNullOrEmpty(textBoxEmail.Text))
            {
                return;
            }
            if (!EsEmailValido(textBoxEmail.Text))
            {
                MessageBox.Show("Recuerde que el email debe contener '@', un dominio y una extensión", "Email invalido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxEmail.Text = "";
                textBoxEmail_Leave(this, EventArgs.Empty);
            }
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
            }
        }

        private void textBoxTelefono_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxTelefono.Text == "Telefono")
            {
                return;
            }
            if (string.IsNullOrEmpty(textBoxTelefono.Text))
            {
                return;
            }

            if (!EsNumeroValido(textBoxTelefono.Text))
            {
                MessageBox.Show("El teléfono solo puede contener números", "Teléfono invalido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxTelefono.Text = "";
                textBoxTelefono_Leave(this, EventArgs.Empty);
            }
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
            }
        }
        private void textBoxDireccionConPiso_Enter(object sender, EventArgs e)
        {
            if (textBoxPiso.Text == "Piso/Dep.")
            {
                textBoxPiso.Text = "";
                textBoxPiso.ForeColor = Color.Black; // Color del texto cuando el usuario escribe
            }
        }


        private void textBoxDireccionConPiso_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxPiso.Text))
            {
                textBoxPiso.Text = "Piso/Dep.";
                textBoxPiso.ForeColor = Color.Gray; // Color del placeholder
            }
        }

        private void textBoxDireccion_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxDireccion.Text))
            {
                return;
            }
            if (!EsLetrasNumerosEspaciosValido(textBoxDireccion.Text))
            {
                MessageBox.Show("La dirección no puede contener caracteres especiales", "Dirección invalido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxDireccion.Text = "";
                textBoxDireccion_Leave(this, EventArgs.Empty);
            }
        }

        private void textBoxPiso_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxPiso.Text))
            {
                return;
            }
            if (!EsLetrasNumerosEspaciosValido(textBoxDireccion.Text))
            {
                MessageBox.Show("El piso no puede contener caracteres especiales", "Piso invalido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPiso.Text = "";
                textBoxDireccionConPiso_Leave(this, EventArgs.Empty);
            }
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
            }
        }
        private void textBoxNroTarjeta_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxNroTarjeta.Text))
            {
                return;
            }
            if (!EsNumeroValido(textBoxNroTarjeta.Text))
            {
                MessageBox.Show("La tarjeta solo puede contener números", "Tarjeta invalido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxNroTarjeta.Text = "";
                textBoxNroTarjeta_Leave(this, EventArgs.Empty);
            }

        }


        //Eventos de selector de fecha

        private void dateTimePickerFechaNacimiento_Validating(object sender, CancelEventArgs e)
        {
            if (!EsFechaValida(dateTimePickerFechaNacimiento.Value))
            {
                MessageBox.Show("Seleccione una fecha anterior a hoy", "Fecha invalida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePickerFechaNacimiento.Value = DateTime.Today;
            }
        }

        //Validaciones 

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

        
        private async void buttonGuardarLegajo_Click(object sender, EventArgs e)
        {
            BdContext context = new BdContext(new DbContextOptions<BdContext>());

            var socioRepository = new SocioRepository(context);
            Socio socio = new Socio
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

           bool resultado = await socioRepository.InsertarUnSocioEnTabla( socio );

           Console.WriteLine(resultado ? $"El socio  {socio.FirstName + ' ' + socio.LastName} se insertó correctamente en la base de datos" : $"Error al insertar al socio {socio.FirstName + ' ' + socio.LastName} en la base de datos");
        }
    }
}
