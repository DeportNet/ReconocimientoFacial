using DeportnetOffline.Data.Dto.Table;
using DeportnetOffline.Data.Mapper;
using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Utils;
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
        }

        //paginado

        public void CargarDatos(int paginaActual, int tamanioPagina)
        {
                      
            PaginadoResultado<Venta> paginaVentas = PaginadorUtils.ObtenerPaginadoAsync(Context.Ventas, paginaActual, tamanioPagina).Result;

            CambiarInformacionPagina(paginaVentas);

            //todo hacer el mapper

            //dataGridView1.DataSource = TablaMapper.ListaSocioToListaInformacionTablaSocio(paginaVentas.Items);
        }

        private void CambiarInformacionPagina(PaginadoResultado<Venta> paginaSocios) 
        {
            TotalPaginas = paginaSocios.TotalPaginas;
            PaginaActual = paginaSocios.PaginaActual;

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
