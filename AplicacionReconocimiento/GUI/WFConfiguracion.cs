using DeportNetReconocimiento.Utils;
using System.Text.Json;
using System.Xml;


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

            // Asignar el objeto de configuración al PropertyGrid (para que se vea lo que se puede configurar)
            propertyGrid1.SelectedObject = configuracion;
        }

        //propiedades



        private void WFConfiguracion_Load(object sender, EventArgs e)
        {

        }

        private void PropertyGrid1_Click(object sender, EventArgs e)
        {

        }

        private void GuardarCambiosButton_Click(object sender, EventArgs e)
        {
            this.Close();
            principal.AplicarConfiguracion(configuracion);
            ConfiguracionEstilos.GuardarJsonConfiguracion(configuracion);


            //principal.Show();
        }

        private void PropertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            //se pueden cambiar los estilos de forma dinamica
            principal.AplicarConfiguracion(configuracion);
            ConfiguracionEstilos.GuardarJsonConfiguracion(configuracion);
        }

        private void WFConfiguracion_FormClosing(object sender, FormClosingEventArgs e)
        {
            //TODO: modal con preguntar si desear salir sin guardar cambios
            ConfiguracionEstilos.GuardarJsonConfiguracion(configuracion);
        }

       
    }
}
