namespace DeportNetReconocimiento.GUI
{
    public partial class Loading : Form
    {
        
        public Loading()
        {
            InitializeComponent();
        }

        public void CambiarTexto(string texto)
        {
            label1.Text = texto;
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
