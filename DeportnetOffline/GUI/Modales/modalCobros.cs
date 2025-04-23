using DeportnetOffline.Data.Dto.Table;
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
        public ModalCobro(InformacionSocioTabla socio)
        {
            InitializeComponent();
            labelNombreApelldioCliente.Text = socio.NombreYApellido;
            comboBox1.SelectedIndex = 0;
        }

        //Obtener los datos para cargar el combo box.

        //Hacer que cuando seleccione un campo del combo box - Traer los datos

        //Calcular la fecha de vigencia de este pago

        //Agregar los datos a los labels 

        //En el boton agregar:

        //Hacer un registro de un cobro 


    }
}
