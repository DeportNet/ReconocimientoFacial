using DeportnetOffline.Data.Dto.Table;
using DeportnetOffline.Data.Filtros;
using DeportnetOffline.Data.Mapper;
using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Utils;

namespace DeportnetOffline
{
    public partial class VistaAltaLegajos : UserControl
    {
        private int PaginaActual;
        private int TotalPaginas;
        private int TamanioPagina;
        private BdContext Context = BdContext.CrearContexto();

        public VistaAltaLegajos()
        {
            InitializeComponent();
            PaginaActual = 1;
            TotalPaginas = 1;
            TamanioPagina = 20;
            CargarDatos(PaginaActual, TamanioPagina);
            CrearTabla();
        }

        public void CrearTabla()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nombre y Apellido",
                DataPropertyName = "NombreYApellido",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nro. Tarjeta",
                DataPropertyName = "NroTarjeta"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Email",
                DataPropertyName = "Email"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Sexo",
                DataPropertyName = "Sexo"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Edad",
                DataPropertyName = "Edad"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Estado",
                DataPropertyName = "Estado"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Sincronizado",
                DataPropertyName = "IsSincronizado"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Fecha - Hora sincro",
                DataPropertyName = "FechaHoraSincronizado",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "g" } // Formato corto de fecha y hora
            });

            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        //paginado

        public void CargarDatos(int paginaActual, int tamanioPagina)
        {
            //creamos la consulta a la base de datos con el filtro de nuevos socios
            IQueryable<Socio> query = Context.Socios.AsQueryable();
            query = FiltrosSocio.FiltrarPorNuevosSocios(query);

            //hacemos la consulta paginada + filtrada
            PaginadoResultado<Socio> paginaSocios = PaginadorUtils.ObtenerPaginadoAsync(query, paginaActual, tamanioPagina).Result;

            //proporcionamos la info de las pags
            CambiarInformacionPagina(paginaSocios);

            //actualizamos la info de la tabla
            dataGridView1.DataSource = TablaMapper.ListadoNuevosLegajosToListadoInformacionNuevoLegajos(paginaSocios.Items);
        }

        private void CambiarInformacionPagina(PaginadoResultado<Socio> paginaSocios)
        {
            TotalPaginas = paginaSocios.TotalPaginas;
            PaginaActual = paginaSocios.PaginaActual;

            labelCantPaginas.Text = $"Página {PaginaActual} de {TotalPaginas}";
        }

        //cambiar de pagina
        private void botonAntPaginacion_Click(object sender, EventArgs e)
        {
            if (PaginaActual > 1)
            {
                PaginaActual--;


                //creamos la consulta a la base de datos con el filtro de nuevos socios
                IQueryable<Socio> query = Context.Socios.AsQueryable();
                query = FiltrosSocio.FiltrarPorNuevosSocios(query);

                //hacemos la consulta paginada + filtrada
                PaginadoResultado<Socio> paginaSocios = PaginadorUtils.ObtenerPaginadoAsync(query, PaginaActual, TamanioPagina).Result;

                CambiarInformacionPagina(paginaSocios);

                //Actualizar datos en la tabla
                dataGridView1.DataSource = TablaMapper.ListadoNuevosLegajosToListadoInformacionNuevoLegajos(paginaSocios.Items);
            }
        }

        private void botonSgtPaginacion_Click(object sender, EventArgs e)
        {

            if (PaginaActual < TotalPaginas)
            {
                PaginaActual++;

                //creamos la consulta a la base de datos con el filtro de nuevos socios
                IQueryable<Socio> query = Context.Socios.AsQueryable();
                query = FiltrosSocio.FiltrarPorNuevosSocios(query);

                //hacemos la consulta paginada + filtrada
                PaginadoResultado<Socio> paginaSocios = PaginadorUtils.ObtenerPaginadoAsync(query, PaginaActual, TamanioPagina).Result;

                CambiarInformacionPagina(paginaSocios);

                //Actualizar datos en la tabla
                dataGridView1.DataSource = TablaMapper.ListadoNuevosLegajosToListadoInformacionNuevoLegajos(paginaSocios.Items);
            }

        }
    }
}
