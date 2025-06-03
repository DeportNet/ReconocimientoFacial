using DeportnetOffline.Data.Dto.Table;
using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Repository;
using DeportNetReconocimiento.Api.Services;


namespace DeportnetOffline
{
    public partial class ModalCobro : Form
    {

        private List<Membresia> membresias = [];
        private InformacionSocioTabla socio;
        private Membresia membresiaSeleccionada;


        public ModalCobro(InformacionSocioTabla socioTabla)
        {
            InitializeComponent();
            socio = socioTabla;
            labelNombreApelldioCliente.Text = socio.NombreYApellido;
            ObtenerMembresiasDeBD();
        }

        public void ObtenerMembresiasDeBD()
        {
            using var bdContext = BdContext.CrearContexto();
            membresias = bdContext.Membresias.ToList();
            
            if (membresias != null)
            {
                CargarComboBox(membresias);
            }
        }

        private void CargarComboBox(List<Membresia> membresias)
        {
            membresias.Insert(0, new Membresia(idDx: 0, name: "Seleccione una membresía", amount: "0", isSaleItem: 'x', period: "0", days: "0"));

            comboBox1.DataSource = membresias;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "IdDx";
            comboBox1.SelectedIndex = 0;
            comboBox1.IntegralHeight = false;
            comboBox1.MaxDropDownItems = 10;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (membresias.Count > 0)
            {
                membresiaSeleccionada = (Membresia?)comboBox1.SelectedItem;
                if (membresiaSeleccionada != null && membresiaSeleccionada.IdDx != 0)
                {
                    ActualizarLabels(membresiaSeleccionada);
                }
                else
                {
                    LimpiarLabels();
                }
            }
        }


        private void ActualizarLabels(Membresia membresia)
        {
            labelCantidad.Text = "1";
            labelPrecio.Text = membresia.Amount.ToString();
            labelDescripcion.Text = membresia.Name.ToString();
            labelVigencia.Text = membresia.Period.ToString();
            label4.Text = membresia.Amount.ToString();
        }

        private void LimpiarLabels()
        {
            labelCantidad.Text = "";
            labelPrecio.Text = "";
            labelDescripcion.Text = "";
            labelVigencia.Text = "";
            label4.Text = "";
            comboBox1.SelectedIndex = 0;
        }

        private async void buttonCobrar_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0)
            {
                return;
            }

            if(socio == null)
            {
                return;
            }
            //        public Venta(int itemId, int branchMemberId,int? idSocio, char isSaleItem, string? period, string? days, string name, string amount)
            Venta venta = new Venta(
                itemId: membresiaSeleccionada.IdDx,
                branchMemberId: socio.IdDx,
                idSocio: socio.Id,
                isSaleItem: membresiaSeleccionada.IsSaleItem,
                period: membresiaSeleccionada.Period,
                days: membresiaSeleccionada.Days,
                name: membresiaSeleccionada.Name,
                amount: membresiaSeleccionada.Amount
                );
             
            bool resultado = await VentaRepository.RegistrarVenta(venta);

            if (resultado)
            {

                MessageBox.Show("Venta completada", "La venta se registro exitosamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await SocioService.ActualizarEstadoSocio(socio.Id, 1);
                
                LimpiarLabels();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al registrar la venta, intente nuevamente");
            }
            
        }

    }
}
