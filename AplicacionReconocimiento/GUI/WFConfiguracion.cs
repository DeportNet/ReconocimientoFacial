using DeportNetReconocimiento.Utils;


namespace DeportNetReconocimiento.GUI
{
    public partial class WFConfiguracion : Form
    {
        private ConfiguracionEstilos configuracion;
        private WFPrincipal principal;

       
        public WFConfiguracion(ConfiguracionEstilos configuracionEstilos, WFPrincipal principal)
        {
            InitializeComponent();
            this.configuracion = configuracionEstilos;
            this.principal = principal;

            // Asignar el objeto de configuración al PropertyGrid
            propertyGrid1.SelectedObject = configuracion;
        }

        //propiedades

  

        private void WFConfiguracion_Load(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = configuracion;
        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }

    }
}
