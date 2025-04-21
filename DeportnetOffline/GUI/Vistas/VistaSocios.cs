using DeportnetOffline.Data.Dto.Table;
using DeportnetOffline.Data.Mapper;
using DeportnetOffline.GUI.Modales;
using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeportnetOffline
{
    public partial class VistaSocios : UserControl
    {
        public VistaSocios()
        {
            int paginaActual = 1;
            int filasPorPagina = 5;
            int registros = 10;
            InitializeComponent();
            labelCantPaginas.Text = $"Página {paginaActual} de 50";


            textBox1_Leave(this, EventArgs.Empty);
            textBox2_Leave(this, EventArgs.Empty);
            textBox3_Leave(this, EventArgs.Empty);
            ComboBox1_Leave(this, EventArgs.Empty);
            comboBox1.SelectedIndex = 0;
            comboBoxEstado.SelectedIndex = 0;
            cargarDatos();

        }

        public void cargarDatos()
        {
            using (var context = BdContext.CrearContexto())
            {
                List<Socio> socios = context.Socios.ToList();

                dataGridView1.DataSource = TablaMapper.ListaSocioToListaInformacionTablaSocio(socios);

                dataGridView1.Columns["ColumnaCobro"].DisplayIndex = dataGridView1.Columns.Count - 1;
                dataGridView1.Columns["ColumnaVenta"].DisplayIndex = dataGridView1.Columns.Count - 1;
                dataGridView1.Columns["Email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns["NombreYApellido"].HeaderText = "Nombre y Apellido";
                dataGridView1.Columns["NombreYApellido"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns["Direccion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }


        public string CalcularEdad(DateTime fecha)
        {

            int anio = fecha.Year;
            int anioActual = DateTime.Now.Year;

            return (anioActual - anio).ToString();

        }

        public string CalcularEstado(string estado)
        {
            return int.Parse(estado) == 1 ? "Activo" : "Inactivo";
        }


        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBoxApellidoNombre.Text == "Apellido y Nombre")
            {
                textBoxApellidoNombre.Text = "";
                textBoxApellidoNombre.ForeColor = Color.Black; // Color del texto cuando el usuario escribe
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxApellidoNombre.Text))
            {
                textBoxApellidoNombre.Text = "Apellido y Nombre";
                textBoxApellidoNombre.ForeColor = Color.Gray; // Color del placeholder
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBoxEmail.Text == "Email")
            {
                textBoxEmail.Text = "";
                textBoxEmail.ForeColor = Color.Black; // Color del texto cuando el usuario escribe
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEmail.Text))
            {
                textBoxEmail.Text = "Email";
                textBoxEmail.ForeColor = Color.Gray; // Color del placeholder
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBoxNroTarjeta.Text == "Nro. tarjeta o DNI")
            {
                textBoxNroTarjeta.Text = "";
                textBoxNroTarjeta.ForeColor = Color.Black; // Color del texto cuando el usuario escribe
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxNroTarjeta.Text))
            {
                textBoxNroTarjeta.Text = "Nro. tarjeta o DNI";
                textBoxNroTarjeta.ForeColor = Color.Gray; // Color del placeholder
            }
        }

        private void ComboBox1_Enter(object sender, EventArgs e)
        {
            if (comboBox1.ForeColor == Color.Gray)
            {
                comboBox1.Text = "";
                comboBox1.ForeColor = Color.Black;
            }
        }

        private void ComboBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                comboBox1.Text = "Categoría de socio...";
                comboBox1.ForeColor = Color.Gray;
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            ModalNuevoLegajo modal = new ModalNuevoLegajo();
            modal.Show();
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string nombreApellidoSocio = dataGridView1.Rows[e.RowIndex].Cells["NombreYApellido"].Value.ToString();

                if (dataGridView1.Columns[e.ColumnIndex].Name == "ColumnaVenta")
                {
                    ModalVentas modal = new ModalVentas(nombreApellidoSocio);
                    modal.Show();
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "ColumnaCobro")
                {
                    ModalCobro modal = new ModalCobro(nombreApellidoSocio);
                    modal.Show();
                }
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //Boton para aplicar los filtros
        private void button1_Click(object sender, EventArgs e)
        {

            //Obtener datos de todos los inputs
            string estado = comboBoxEstado.Text;
            string? nroTarjeta = textBoxNroTarjeta.Text;
            string? apellidoNombre = textBoxApellidoNombre.Text;
            string? email = textBoxEmail.Text;

            List<Socio> listaSocios = FiltrarSocios(estado, nroTarjeta, apellidoNombre, email);

            dataGridView1.DataSource = TablaMapper.ListaSocioToListaInformacionTablaSocio(listaSocios);
            //Con los campos que tienen datos preparo una consulta

            //Ejecuto la consulta

            //Actualizo los datos con los registros devueltos por la consulta 

        }


        public List<Socio> FiltrarSocios(string estado, string? nroTarjeta, string? apellidoNombre, string? email)
        {

            using var context = BdContext.CrearContexto();

            IQueryable<Socio> query = context.Socios;


            if (!string.IsNullOrEmpty(nroTarjeta))
            {
                query = query.Where(p => p.CardNumber.Contains(nroTarjeta));
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(p => p.Email.Contains(email));
            }

            if(!string.IsNullOrEmpty(apellidoNombre))
            {
                var nombres = apellidoNombre.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var nombre in nombres) {

                    string nombreActual = nombre.ToLower();
                    query = query.Where( p => 
                        p.FirstName.ToLower().Contains(nombreActual) ||
                        p.LastName.ToLower().Contains(nombreActual));
                }
            }

            query = query.Where(p => p.IsActive.Contains(estado));


            return query.ToList();
        }

    }
}
