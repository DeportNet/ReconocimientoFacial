using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Modelo
{
    public class Persona
    {

        private string id { get; set; }
        private string nombre { get; set; }
        private string apellido { get; set; }
        private string actividad { get; set; }
        private string vencimiento { get; set; }
        private string clasesRestantes { get; set; }
        private string rta {  get; set; }
        private string mensaje { get; set; }
        private string fecha { get; set; }
        private string hora { get; set; }
        private string pregunta { get; set; }

        public Persona(string id, string nombre, string apellido, string actividad, string clasesRestantes, string mensaje, string vencimiento, string rta, string fecha, string hora, string pregunta)
        {

            this.id = id;
            this.nombre = nombre;
            this.apellido = apellido;
            this.actividad = actividad;
            this.vencimiento = vencimiento;
            this.clasesRestantes = clasesRestantes;
            this.rta = rta;
            this.mensaje = mensaje;
            this.fecha = fecha;
            this.hora = hora;
            this.pregunta = pregunta;
        }

        public Persona() { }

        // Propiedades con getter y setter
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }

        public string Actividad
        {
            get { return actividad; }
            set { actividad = value; }
        }
        public string Vencimiento
        {
            get { return vencimiento; }
            set { vencimiento = value; }
        }

        public string ClasesRestantes
        {
            get { return clasesRestantes; }
            set { clasesRestantes = value; }
        }

        public string Rta
        {
            get { return rta; }
            set { rta = value; }
        }
        public string Mensaje
        {
            get { return mensaje; }
            set { mensaje = value; }
        }

        public string Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        public string Hora
        {
            get { return hora; }
            set { hora = value; }
        }

        public string Pregunta
        {
            get { return pregunta; }
            set { pregunta = value; }
        }

    }
}
