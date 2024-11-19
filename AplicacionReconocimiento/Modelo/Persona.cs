using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Modelo
{
    public class Persona
    {

       private  string id { get; set; }
        private string nombre { get; set; }
        private string apellido { get; set; }
        private string actividad { get; set; }
        private string clasesRestantes { get; set; }
        private string mensaje { get; set; }

        public Persona(string id, string nombre, string apellido, string actividad, string clasesRestantes, string mensaje) { 
        
            this.id = id;
            this.nombre = nombre;   
            this.apellido = apellido;   
            this.actividad = actividad;
            this.clasesRestantes = clasesRestantes;
            this.mensaje = mensaje;        
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

        public string ClasesRestantes
        {
            get { return clasesRestantes; }
            set { clasesRestantes = value; }
        }

        public string Mensaje
        {
            get { return mensaje; }
            set { mensaje = value; }
        }

    }
}
