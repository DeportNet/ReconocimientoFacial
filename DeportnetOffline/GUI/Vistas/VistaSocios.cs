using DeportnetOffline.Data.Dto.Table;
using DeportnetOffline.Data.Mapper;
using DeportnetOffline.GUI.Modales;
using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using System.Data;

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


        //Filtros
        public List<Socio> FiltrarSocios(string estado, string? nroTarjeta, string? apellidoNombre, string? email)
        {

            using var context = BdContext.CrearContexto();

            IQueryable<Socio> query = context.Socios;

            query = FiltrarPorNroTarjetaODNI(nroTarjeta, query);
            query = FiltrarPorEmail(email, query);
            query = FiltrarPorNombreYApellido(apellidoNombre, query);
            query = FiltrarPorEstado(estado, query);

            return query.ToList();
        }


        private IQueryable<Socio> FiltrarPorNroTarjetaODNI(string nroTarjeta, IQueryable<Socio> query)
        {

                if (!string.IsNullOrEmpty(nroTarjeta))
                {
                    query = query.Where(p =>
                    p.CardNumber.Contains(nroTarjeta) ||
                    p.IdNumber.ToLower().Contains(nroTarjeta));

                }

            return query;
        }

        private IQueryable<Socio> FiltrarPorEmail(string email, IQueryable<Socio> query)
        {

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(p => p.Email.Contains(email));
            }

            return query;
        }

        private IQueryable<Socio> FiltrarPorNombreYApellido(string apellidoNombre, IQueryable<Socio> query)
        {

            if (!string.IsNullOrEmpty(apellidoNombre))
            {
                var nombres = apellidoNombre.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var nombre in nombres)
                {

                    string nombreActual = nombre.ToLower();
                    query = query.Where(p =>
                        p.FirstName.ToLower().Contains(nombreActual) ||
                        p.LastName.ToLower().Contains(nombreActual));
                }
            }

            return query;
        }

        private IQueryable<Socio> FiltrarPorEstado(string estado, IQueryable<Socio> query)
        {
            if (!string.IsNullOrEmpty(estado))
            {
                query = query.Where(p => p.IsActive.Contains(estado));
            }

            return query;
        }


        //Eventos de la interfaz

        //Boton para aplicar los filtros
        private void button1_Click(object sender, EventArgs e)
        {
            //Obtener datos de todos los inputs
            string estado = ObtenerEstadoFiltro(comboBoxEstado.Text);
            string? nroTarjeta = LimpiarPlaceholderCampoFiltro(textBoxNroTarjeta.Text);
            string? apellidoNombre = LimpiarPlaceholderCampoFiltro(textBoxApellidoNombre.Text);
            string? email = LimpiarPlaceholderCampoFiltro(textBoxEmail.Text);

            //Filtrar socios
            List<Socio> listaSocios = FiltrarSocios(estado, nroTarjeta, apellidoNombre, email);

            //Actualizar datos en la tabla
            dataGridView1.DataSource = TablaMapper.ListaSocioToListaInformacionTablaSocio(listaSocios);
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

        private void button2_Click(object sender, EventArgs e)
        {
            ModalNuevoLegajo modal = new ModalNuevoLegajo();
            modal.Show();
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                InformacionSocioTabla socio = (InformacionSocioTabla)dataGridView1.Rows[e.RowIndex].DataBoundItem;

                if (dataGridView1.Columns[e.ColumnIndex].Name == "ColumnaVenta")
                {
                    ModalVentas modal = new ModalVentas(socio);
                    modal.Show();
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "ColumnaCobro")
                {
                    ModalCobro modal = new ModalCobro(socio);
                    modal.Show();
                }
            }
        }



        public string ObtenerEstadoFiltro(string estado)
        {
            switch (estado.Trim().ToLower())
            {
                case "actívos e inactivos":
                    estado = "";
                    break;
                case "solo activos":
                    estado = "1";
                    break;
                case "solo inactivos":
                    estado = "0";
                    break;
            }
            return estado;
        }

        //Limpia los placeholders, esto se hace porque son texto que con eventos se cambia,
        //por lo tanto si no esta seleccionado el campo hay un texto que afecta a los filtros.
        public string LimpiarPlaceholderCampoFiltro(string campo)
        {
            switch (campo.Trim().ToLower())
            {
                case "apellido y nombre":
                    campo = "";
                    break;
                case "nro. tarjeta o dni":
                    campo = "";
                    break;
                case "email":
                    campo = "";
                    break;
                default:
                    break;
            }

            return campo;
        }

    }



}
