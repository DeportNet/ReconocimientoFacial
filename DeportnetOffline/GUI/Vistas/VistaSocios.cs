using DeportnetOffline.Data.Dto.FiltrosRequest;
using DeportnetOffline.Data.Dto.Table;
using DeportnetOffline.Data.Filtros;
using DeportnetOffline.Data.Mapper;
using DeportnetOffline.GUI.Modales;
using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Utils;
using System.Data;
using System.Windows.Forms;

namespace DeportnetOffline
{
    public partial class VistaSocios : UserControl
    {
        private int PaginaActual;
        private int TotalPaginas;
        private int TamanioPagina;
        private static BdContext context = BdContext.CrearContexto();
        public VistaSocios()
        {
            InitializeComponent();
            FiltroEstado = "";
            PaginaActual = 1;
            TotalPaginas = 1;
            TamanioPagina = 20;
            
            textBox1_Leave(this, EventArgs.Empty);
            textBox2_Leave(this, EventArgs.Empty);
            textBox3_Leave(this, EventArgs.Empty);
            comboBoxEstado.SelectedIndex = 0;

            CargarDatos(PaginaActual, TamanioPagina);
            CrearTabla();
        }

        public void CrearTabla()
        {
            
            dataGridView1.Columns["ColumnaCobro"].DisplayIndex = dataGridView1.Columns.Count - 1;
            dataGridView1.Columns["ColumnaVenta"].DisplayIndex = dataGridView1.Columns.Count - 1;
            dataGridView1.Columns["Email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["NombreYApellido"].HeaderText = "Nombre y Apellido";
            dataGridView1.Columns["NombreYApellido"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Direccion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Id"].Visible = false;
        }


        public void CargarDatos(int paginaActual, int tamanioPagina)
        {

            PaginadoResultado<Socio> paginaSocios = PaginadorUtils.ObtenerPaginadoAsync(context.Socios, paginaActual, tamanioPagina).Result;

            CambiarInformacionPagina(paginaSocios); 

            dataGridView1.DataSource = TablaMapper.ListaSocioToListaInformacionTablaSocio(paginaSocios.Items);
        }

        //cambiar de pagina
        private void botonAntPaginacion_Click(object sender, EventArgs e)
        {
            if(PaginaActual > 1)
            {
                PaginaActual--;

                //Filtrar socios
                List<Socio> listaSocios = FiltrarSocios(new FiltrosSocioRequest(FiltroEstado, FiltroNroTarjeta, FiltroApellidoNombre, FiltroEmail), PaginaActual, TamanioPagina);

                //Actualizar datos en la tabla
                dataGridView1.DataSource = TablaMapper.ListaSocioToListaInformacionTablaSocio(listaSocios); 
            }
        }

        private void botonSgtPaginacion_Click(object sender, EventArgs e)
        {
          
            if (PaginaActual < TotalPaginas)
            {
                PaginaActual++;
                //Filtrar socios
                List<Socio> listaSocios = FiltrarSocios(new FiltrosSocioRequest(FiltroEstado, FiltroNroTarjeta, FiltroApellidoNombre, FiltroEmail), PaginaActual, TamanioPagina);

                //Actualizar datos en la tabla
                dataGridView1.DataSource = TablaMapper.ListaSocioToListaInformacionTablaSocio(listaSocios); 
            }

        }

        private void CambiarInformacionPagina(PaginadoResultado<Socio> paginaSocios)
        {
            TotalPaginas = paginaSocios.TotalPaginas;
            PaginaActual = paginaSocios.PaginaActual;

            labelCantPaginas.Text = $"Página {PaginaActual} de {TotalPaginas}";
        }

        //Filtros
        public List<Socio> FiltrarSocios(FiltrosSocioRequest filtrosSocio, int nroPagina, int tamPag)
        {

            using var context = BdContext.CrearContexto();

            IQueryable<Socio> query = context.Socios.AsQueryable(); 

            query = FiltrosSocio.FiltrarPorNroTarjetaODni(filtrosSocio.NroTarjeta, query);
            query = FiltrosSocio.FiltrarPorEmail(filtrosSocio.Email, query);
            query = FiltrosSocio.FiltrarPorNombreYApellido(filtrosSocio.ApellidoNombre, query);
            query = FiltrosSocio.FiltrarPorEstado(filtrosSocio.Estado, query);

            PaginadoResultado<Socio> paginaSociosFiltrados = PaginadorUtils.ObtenerPaginadoAsync(query, nroPagina, tamPag).Result;
            
            CambiarInformacionPagina(paginaSociosFiltrados);

            return paginaSociosFiltrados.Items;
        }


        //Eventos de la interfaz

        private string FiltroEstado;
        private string? FiltroNroTarjeta;
        private string? FiltroApellidoNombre;
        private string? FiltroEmail;


        //Boton para aplicar los filtros
        private void button1_Click(object sender, EventArgs e)
        {
            //Obtener datos de todos los inputs
            FiltroEstado = ObtenerEstadoFiltro(comboBoxEstado.Text);
            FiltroNroTarjeta = LimpiarPlaceholderCampoFiltro(textBoxNroTarjeta.Text);
            FiltroApellidoNombre = LimpiarPlaceholderCampoFiltro(textBoxApellidoNombre.Text);
            FiltroEmail = LimpiarPlaceholderCampoFiltro(textBoxEmail.Text);
            int nroPagina = 1;
            int tamPag = TamanioPagina;

            //Filtrar socios
            List<Socio> listaSocios = FiltrarSocios(new FiltrosSocioRequest(FiltroEstado, FiltroNroTarjeta, FiltroApellidoNombre, FiltroEmail), nroPagina, tamPag);

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
            modal.ShowDialog();
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                InformacionSocioTabla socio = (InformacionSocioTabla)dataGridView1.Rows[e.RowIndex].DataBoundItem;

                if (dataGridView1.Columns[e.ColumnIndex].Name == "ColumnaVenta")
                {
                    if(socio.Estado == "Activo")
                    {
                        ModalVentas modal = new ModalVentas(socio);
                        modal.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No se le puede vender nada al socio porque esta inactivo","Socio inactivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "ColumnaCobro")
                {
                    ModalCobro modal = new ModalCobro(socio);
                    modal.ShowDialog();
                }
            }
        }



        public string ObtenerEstadoFiltro(string estado)
        {
            estado = estado.Trim().ToLower();
            
            return estado switch
            {
                "actívos e inactivos" => "",
                "solo inactivos" => "0",
                "solo activos" => "1"
            };

            
        }

        //Limpia los placeholders, esto se hace porque son texto que con eventos se cambia,
        //por lo tanto si no esta seleccionado el campo hay un texto que afecta a los filtros.
        public string LimpiarPlaceholderCampoFiltro(string campo)
        {

            campo = campo.Trim().ToLower();

            return campo switch
            {
                "apellido y nombre" => "",
                "nro. tarjeta o dni" => "",
                "email" => "",
                _ => campo
            };

        }


    }



}
