

namespace DeportnetOffline
{
    public partial class VistaAccesos : UserControl
    {
        public VistaAccesos()
        {
            InitializeComponent();
            int paginaActual = 1;
            int filasPorPagina = 5;
            int registros = 10;
            labelCantPaginas.Text = $"Página {paginaActual} de 50";
        }


    }
}
