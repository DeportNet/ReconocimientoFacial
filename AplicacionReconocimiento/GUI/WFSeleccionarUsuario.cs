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
        private Empleado? empleadoSeleccionado;
        public WFSeleccionarUsuario()
        {
            InitializeComponent();
            _contextBd = BdContext.CrearContexto();
            nombreSucursal = ObtenerNombreSucursal();
            listadoEmpleados = ObtenerListadoDeEmpleados();
            empleadoSeleccionado = null;
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
            if (listadoEmpleados.Count == 0)
            {
                comboBox1.Items.Add("Empleado Predeterminado");
                return;
            }


            //comboBox1.DataSource = listadoEmpleados;
            //comboBox1.ValueMember = "Id";
            foreach (Empleado empleado in listadoEmpleados)
            {
                comboBox1.Items.Add(empleado.JuntarNombreYApellido());
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            


            //es la parte de seleccione...
            if (comboBox1.SelectedIndex == 0)
            {
                panel1.Hide();
                return;
            }

            if(comboBox1.Items[0]?.ToString() == "Empleado Predeterminado")
            {
                button1.Text = "Ingresar default";
                empleadoSeleccionado = new Empleado(); //para poder pasar una validacion
                ingresoSinEmpleado = true;
                return;
            }

            label2.Text = "Ingrese la contraseña de " + comboBox1.SelectedItem + ":";
            panel1.Show();

            //if (comboBox1.SelectedItem is Empleado empleado)
            //{
            //    Console.WriteLine($"Seleccionaste: {empleado.FirstName} {empleado.LastName}");
            //    label2.Text = "Ingrese la contraseña de " + empleado.JuntarNombreYApellido() + ":";
            //    panel1.Show();
            //}
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
            if(empleadoSeleccionado == null)
            {
                return;
            }

            if (ingresoSinEmpleado)
            {
                IngresarSinEmpleado();
            }

            if(empleadoSeleccionado.Password != textBox1.Text)
            {
                mensajeErrorLabel.Show();
                mensajeErrorLabel.Text = "Contraseña incorrecta";
            }

            //si la contrasenia es correcta abrir el formulario principal


            this.Hide();
            //wfPrincipal.Show();
        }

    }
}
