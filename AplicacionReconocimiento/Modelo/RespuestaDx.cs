using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Modelo
{
    public class RespuestaDx
    {
        private string id { get; set; }
        private string nombre { get; set; }
        private string apellido { get; set; }
        private string nombreCompleto { get; set; }
        private string estado { get; set; }
        private string mensajeCrudo { get; set; }
        private string mensajeAccesoAceptado { get; set; }
        private string mensajeAccesoDenegado { get; set; }
        private string mostrarcumpleanios { get; set; }

        public RespuestaDx() { }


        public string Id { get { return id; } set { id = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public string Apellido { get { return apellido; } set { apellido = value; } }
        public string NombreCompleto { get { return nombreCompleto; } set { nombreCompleto = value; } }
        public string Estado { get { return estado; } set { estado = value; } }
        public string MensajeCrudo { get { return mensajeCrudo; } set { mensajeCrudo = value; } }
        public string MensajeAccesoAceptado { get { return mensajeAccesoAceptado; } set { mensajeAccesoAceptado = value; } }
        public string MensajeAccesoDenegado { get { return mensajeAccesoDenegado; } set { mensajeAccesoDenegado = value; } }
        public string Mostrarcumpleanios { get { return mostrarcumpleanios; } set { mostrarcumpleanios = value; } }
    }
}
