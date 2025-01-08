using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Dtos.Request
{
    public class AltaFacialClienteRequest
    {

        private int idCliente;
        private int idGimnasio;
        private string nombreCliente;

        public AltaFacialClienteRequest(int idCliente, int idGimnasio, string nombreCliente)
        {
            this.idCliente = idCliente;
            this.idGimnasio = idGimnasio;
            this.nombreCliente = nombreCliente;
        }

        public AltaFacialClienteRequest() { }

        // Propiedades con getter y setter
        public int IdCliente
        {
            get { return idCliente; }
            set { idCliente = value; }
        }
        public int IdGimnasio
        {
            get { return idGimnasio; }
            set { idGimnasio = value; }
        }

        public string NombreCliente
        {
            get { return nombreCliente; }
            set { nombreCliente = value; }
        }

        public override string ToString()
        {
            return  "IdCliente: " + idCliente + ", IdGimnasio: " + idGimnasio + ", NombreCliente: " + nombreCliente;
        }

    }

}
