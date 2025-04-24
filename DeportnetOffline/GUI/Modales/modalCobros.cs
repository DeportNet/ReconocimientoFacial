using DeportnetOffline.Data.Dto.Table;
using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
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
    public partial class ModalCobro : Form
    {



        private List<Membresia> ListaMembresias = [];
        private InformacionSocioTabla socio;
        private Articulo articuloSeleccionado;


        public ModalCobro(InformacionSocioTabla socioTabla)
        {
            InitializeComponent();
            socio = socioTabla;
            labelNombreApelldioCliente.Text = socio.NombreYApellido;
            ObtenerMembresiasDeBD();

        }

        public void ObtenerMembresiasDeBD()
        {
            using(var context = BdContext.CrearContexto())
            {
                ListaMembresias = context.Membresias.ToList();
            }
            if(ListaMembresias != null)
            {
                CargarComboBox(ListaMembresias);
            }
        }

        private void CargarComboBox(List<Membresia> membresias)
        {
            membresias.Insert(0, new Membresia(idDx: 0, name:"Seleccione una membresía", amount: 0, isSaleItem:'x', period: 0, days: 0));

            comboBox1.DataSource = membresias;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "IdDx";
            comboBox1.SelectedIndex = 0;
            comboBox1.IntegralHeight = false;
            comboBox1.MaxDropDownItems = 10;
        }

        //Obtener los datos para cargar el combo box.

        //Hacer que cuando seleccione un campo del combo box - Traer los datos

        //Calcular la fecha de vigencia de este pago

        //Agregar los datos a los labels 

        //En el boton agregar:

        //Hacer un registro de un cobro 


    }
}
