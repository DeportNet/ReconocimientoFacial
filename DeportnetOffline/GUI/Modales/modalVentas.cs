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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DeportnetOffline
{
    public partial class ModalVentas : Form
    {

        private List<Articulo> articulos = [];
        private InformacionSocioTabla socio;
        private Articulo articuloSeleccionado;
        public ModalVentas(InformacionSocioTabla socioTabla)
        {
            socio = socioTabla;
            InitializeComponent();
            labelNombreApelldioCliente.Text = socioTabla.NombreYApellido;
            ObtenerProductosDeBD();
        }

        //Obtener los datos para cargar el combo box.

        public void ObtenerProductosDeBD()
        {
            using (var context = BdContext.CrearContexto())
            {
                articulos = context.Articulos.ToList();
            }
            if(articulos != null)
            {
                CargarComboBox(articulos);
            }
        }


        private void CargarComboBox(List<Articulo> articulos)
        {
            articulos.Insert(0, new Articulo(idDx: 0, nombre: "Seleccione un producto", precio: 0, esUnArticulo: 'x'));

            comboBox1.DataSource = articulos;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "IdDx";
            comboBox1.SelectedIndex = 0;
        }

        //Hacer que cuando seleccione un campo del combo box - Traer los datos


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(articulos.Count  > 0)
            {
                articuloSeleccionado = (Articulo?)comboBox1.SelectedItem;
                if(articuloSeleccionado != null && articuloSeleccionado.IdDx != 0)
                {
                    ActualizarLabels(articuloSeleccionado);
                }
                else
                {
                    LimpiarLabels();
                }
            }
        }


        //Agregar los datos a los labels 


        private void ActualizarLabels(Articulo articulo)
        {
            labelPrecio.Text = "$" + articulo.Amount.ToString();
            labelDescripcion.Text = articulo.Name.ToString();
            label4.Text = "$" + articulo.Amount.ToString();
            labelCantidad.Text = "1";
        }


        private void LimpiarLabels()
        {
            labelPrecio.Text = " ";
            labelDescripcion.Text = " ";
            label4.Text = " ";
            labelCantidad.Text = " ";
            comboBox1.SelectedIndex = 0;
        }

        private void buttonCobrar_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Aca hay que hacer la logica de venta");

            if(socio != null)
            {

            Venta venta = new Venta(itemId: articuloSeleccionado.IdDx ,branchMemberId: socio.Id, isSaleItem:'T');

               RegistrarVenta(venta);

                //if (resultado)
                //{
                //    MessageBox.Show("Venta registrada con exito");
                //LimpiarLabels();

                //}
                //else
                //{
                //    MessageBox.Show("Error al registrar la venta, intente nuevamente");
                //}
            //Prepara la query para almacenar la venta
            //Ejecutar la query


            //Mostrar aviso de que se realizó la venta

            //Limpiar los campos
            }

        }


        private async void RegistrarVenta(Venta venta)
        {
            using (var context = BdContext.CrearContexto())
            {
                await context.Ventas.AddAsync(venta);
                await context.SaveChangesAsync();
            }
        }






    }
}
