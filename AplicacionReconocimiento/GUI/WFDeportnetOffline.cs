
using DeportNetReconocimiento.Utils;

namespace DeportnetOffline
{
    public partial class WFDeportnetOffline : Form
    {
        private static WFDeportnetOffline? instancia = null;
        private WFDeportnetOffline()
        {
            InitializeComponent();
            botonSocios_Click(this, EventArgs.Empty);
        }

        public static WFDeportnetOffline ObtenerInstancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new WFDeportnetOffline();
                }
                return instancia;
            }
        }

        private void botonSocios_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null; // Quita el foco del bot�n
            VistaSocios vistaSocios = new VistaSocios();
            cambiarUserControl(vistaSocios);
            cambiarBotonSeleccionado(0);


        }

        private void botonAccesos_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null; // Quita el foco del bot�n
            VistaAccesos vistaAccesos = new VistaAccesos();
            cambiarUserControl(vistaAccesos);
            cambiarBotonSeleccionado(1);
        }

        private void botonCobros_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null; // Quita el foco del bot�n
            VistaCobros vistaCobros = new VistaCobros();
            cambiarUserControl(vistaCobros);
            cambiarBotonSeleccionado(2);
        }

        private void botonAltaLegajos_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null; // Quita el foco del bot�n
            VistaAltaLegajos vistaAltaLegajos = new VistaAltaLegajos();
            cambiarUserControl(vistaAltaLegajos);
            cambiarBotonSeleccionado(3);
        }



        private void cambiarUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContenido.Controls.Clear();
            panelContenido.Controls.Add(userControl);
            userControl.BringToFront();

        }



        private void cambiarBotonSeleccionado(int nroBoton)
        {
            botonSocios.BackColor = SystemColors.Window;
            botonAccesos.BackColor = SystemColors.Window;
            botonCobros.BackColor = SystemColors.Window;
            botonAltaLegajos.BackColor = SystemColors.Window;

            switch (nroBoton)
            {
                //Boton Socio
                case 0:
                    botonSocios.BackColor = SystemColors.ActiveCaption;
                    break;
                //Boton Acceso
                case 1:
                    botonAccesos.BackColor = SystemColors.ActiveCaption;
                    break;
                //Boton Cobros
                case 2:
                    botonCobros.BackColor = SystemColors.ActiveCaption;
                    break;
                //Boton Alta legajos
                case 3:
                    botonAltaLegajos.BackColor = SystemColors.ActiveCaption;
                    break;
            }

        }

        private void WFDeportnetOffline_FormClosing(object sender, FormClosingEventArgs e)
        {

            var result = MessageBox.Show("Deportnet dice:\n�Est�s seguro de que quieres cerrar la aplicaci�n Offline",
                                             "Confirmaci�n",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // 
                this.Dispose();
            }
            else
            {
                // Cancelar el cierre
                e.Cancel = true;
            }

        }

        private void WFDeportnetOffline_FormClosed(object sender, FormClosedEventArgs e)
        {
            ConfiguracionGeneralUtils.CambiarEstadoModuloActivo();
        }
    }
}
