using DeportNetReconocimiento.SDKHikvision;


namespace DeportNetReconocimiento.GUI
{


    public partial class WFPanelOffline : Form
    {
        private static WFPanelOffline instancia; 
        public WFPanelOffline()
        {
            InitializeComponent();

        }
         

        public static WFPanelOffline ObtenerInstancia
        {

            get
            {
                if (instancia == null)
                {
                    instancia = new WFPanelOffline();
                }
                return instancia;

            }
        }


        private void WFPanelOffline_Load(object sender, EventArgs e)
        {

        }

        // 0-close, 1-open, 2-stay open, 3-stay close
        private void CerrarPuerta_Click(object sender, EventArgs e)
        {
            Hik_Controladora_Puertas.OperadorPuerta(0);
        }

        private void AbrirPuerta_Click(object sender, EventArgs e)
        {
            Hik_Controladora_Puertas.OperadorPuerta(1);
        }

        private void MantenerAbiertaPuerta_Click(object sender, EventArgs e)
        {
            Hik_Controladora_Puertas.OperadorPuerta(2);
        }

        private void ManterCerradaPuerta_Click(object sender, EventArgs e)
        {
            Hik_Controladora_Puertas.OperadorPuerta(3);
        }
    }
}
