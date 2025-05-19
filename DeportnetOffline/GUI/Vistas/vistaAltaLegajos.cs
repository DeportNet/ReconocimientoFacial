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
        }

        //paginado

        public void CargarDatos(int paginaActual, int tamanioPagina)
        {

            PaginadoResultado<Socio> paginaSocios = PaginadorUtils.ObtenerPaginadoAsync(Context.Socios, paginaActual, tamanioPagina).Result;

            CambiarInformacionPagina(paginaSocios);

            //todo hacer el mapper

            //dataGridView1.DataSource = TablaMapper.ListaCobroToListaInformacionTablaCobro(paginaSocios.Items);
        }

        private void CambiarInformacionPagina(PaginadoResultado<Socio> paginaSocios)
        {
            TotalPaginas = paginaSocios.TotalPaginas;
            PaginaActual = paginaSocios.PaginaActual;

            labelCantPaginas.Text = $"Página {PaginaActual} de {TotalPaginas}";
        }


    }
}
