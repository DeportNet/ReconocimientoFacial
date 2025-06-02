using DeportnetOffline.Data.Dto.Table;
using DeportnetOffline.Data.Mapper;
using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Utils;
using Microsoft.EntityFrameworkCore;

namespace DeportnetOffline
{
    public partial class VistaCobros : UserControl
    {
        private int PaginaActual;
        private int TotalPaginas;
        private int TamanioPagina;
        private BdContext Context = BdContext.CrearContexto();

        public VistaCobros()
        {
            InitializeComponent();
            PaginaActual = 1;
            TotalPaginas = 1;
            TamanioPagina = 20;

            CargarDatos(PaginaActual, TamanioPagina);
            CargarTabla();
        }

        private void CargarTabla()
        {
            dataGridView1.Columns["IdSocio"].Visible = false;
            dataGridView1.Columns["Id"].Visible = false;

            dataGridView1.Columns["IdDx"].HeaderText= "Legajo";
            dataGridView1.Columns["IsSaleItem"].HeaderText= "Tipo";
            dataGridView1.Columns["FullNameSocio"].HeaderText = "Nombre Socio";
            dataGridView1.Columns["ItemName"].HeaderText = "Nombre producto";
            dataGridView1.Columns["Amount"].HeaderText= "Precio";
            dataGridView1.Columns["SaleDate"].HeaderText= "Fecha / Hora Venta";
            dataGridView1.Columns["Synchronized"].HeaderText = "Sincroniazdo";
            dataGridView1.Columns["SynchronizedDate"].HeaderText= "Fecha / Hora sincro";
        }


        //paginado

        public void CargarDatos(int paginaActual, int tamanioPagina)
        {

            PaginadoResultado<Venta> paginaVentas = PaginadorUtils.ObtenerPaginadoAsync(Context.Ventas.Include(v => v.Socio), paginaActual, tamanioPagina).Result;
    
            CambiarInformacionPagina(paginaVentas);
                
            dataGridView1.DataSource = TablaMapper.ListaCobroToListaInformacionTablaCobro(paginaVentas.Items);
        }

        private void CambiarInformacionPagina(PaginadoResultado<Venta> paginaVentas) 
        {
            TotalPaginas = paginaVentas.TotalPaginas;
            PaginaActual = paginaVentas.PaginaActual;

            labelCantPaginas.Text = $"Página {PaginaActual} de {TotalPaginas}";
        }

        //cambiar pagina

        private void botonSgtPaginacion_Click(object sender, EventArgs e)
        {

        }

        private void botonAntPaginacion_Click(object sender, EventArgs e)
        {

        }
    }
}
