
namespace DeportnetOffline
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void botonSocios_Click(object sender, EventArgs e)
        {
            VistaSocios vistaSocios = new VistaSocios();
            cambiarUserControl(vistaSocios);
        }

        private void botonAccesos_Click(object sender, EventArgs e)
        {
            VistaAccesos vistaAccesos = new VistaAccesos();
            cambiarUserControl(vistaAccesos);
        }

        private void botonCobros_Click(object sender, EventArgs e)
        {
            VisataCobros vistaCobros = new VisataCobros();
            cambiarUserControl(vistaCobros);
        }

        private void cambiarUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContenido.Controls.Clear();
            panelContenido.Controls.Add(userControl);
            userControl.BringToFront();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void vistaSocios1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            vistaAltaLegajos vistaAltaLegajos = new vistaAltaLegajos();
            cambiarUserControl(vistaAltaLegajos);
        }
    }
}
