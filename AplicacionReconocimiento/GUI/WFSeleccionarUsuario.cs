using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeportNetReconocimiento.GUI
{
    public partial class WFSeleccionarUsuario : Form
    {
        private readonly BdContext _contextBd;
        private string nombreSucursal;
        private bool ingresoSinEmpleado;
        private List<Empleado> listadoEmpleados;

        public WFSeleccionarUsuario()
        {
            InitializeComponent();
            _contextBd = BdContext.CrearContexto();
            nombreSucursal = ObtenerNombreSucursal();
            listadoEmpleados = ObtenerListadoDeEmpleados();

            ingresoSinEmpleado = false;
        }
        private string ObtenerNombreSucursal()
        {
            string? nombre = "pepito";// _contextBd.ConfiguracionGeneral.FirstOrDefault(c => c.Id == 1)?.NombreSucursal;

            if (nombre == null)
            {
                Console.WriteLine("No se pudo obtener el nombre de la sucursal en WFSeleccionarUsuario");
                return "Sin nombre";
            }

            return nombre;
        }

        private List<Empleado> ObtenerListadoDeEmpleados()
        {
            List<Empleado> listadoAux = _contextBd.Empleados.ToList();


            if (listadoAux.Count == 0)
            {
                listadoAux.Add(new Empleado(-1, "Empleado", "Predeterminado", "", "T"));
                Console.WriteLine("No se encontraron empleados en WFSeleccionarUsuario");
            }

            return listadoAux;
        }

        private void WFSeleccionarUsuario_Load(object sender, EventArgs e)
        {
            label1.Text = "Seleccione un usuario para ingresar a " + nombreSucursal;
            CargarCombobox();
        }

        private void CargarCombobox()
        {
            //si no tiene empleados, se agrega un empleado predeterminado para que pueda pasar igual

            comboBox1.DataSource = listadoEmpleados;
            
            comboBox1.DisplayMember = "FullName";

            comboBox1.ValueMember = "Id";

            
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            mensajeErrorLabel.Hide();
            Empleado empleadoSeleccionado = (Empleado)comboBox1.SelectedItem;

            if (empleadoSeleccionado == null)
            {
                Console.WriteLine("Empleado seleccionado es null");
                return;
            }

            if(empleadoSeleccionado.Id == -1)
            {
                ingresoSinEmpleado = true;
                button1.Text = "Ingresar sin empleado";
                panel1.Hide();
                return;
            }
           
            ingresoSinEmpleado = false;
            label2.Text = "Ingrese la contraseña de " + empleadoSeleccionado.FullName + ":";
            

            panel1.Show();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (mensajeErrorLabel.Visible)
            {
                mensajeErrorLabel.Hide();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IngresarSinEmpleado();
        }

        private void IngresarSinEmpleado()
        {
            Console.WriteLine("Logica ingresar sin empleado");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Empleado empleadoSeleccionado = (Empleado)comboBox1.SelectedItem;

            if (empleadoSeleccionado == null)
            {
                Console.WriteLine("Empleado seleccionado es null");
                return;
            }

            //logica si la lista viene vacia
            if (ingresoSinEmpleado)
            {
                IngresarSinEmpleado();
            }

            if(empleadoSeleccionado.Password != textBox1.Text)
            {
                mensajeErrorLabel.Show();
                mensajeErrorLabel.Text = "Contraseña incorrecta";
                return;
            }

            //si la contrasenia es correcta abrir el formulario principal


            this.Hide();
            //WFPrincipal.ObtenerInstancia.Show();
        }

    }
}
