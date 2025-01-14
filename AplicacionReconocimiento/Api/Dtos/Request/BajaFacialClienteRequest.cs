using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Dtos.Request
{
    public class BajaFacialClienteRequest
    {


        private int idCliente;
        private int idSucursal;
        

        public BajaFacialClienteRequest(int idCliente, int idGimnasio)
        {
            this.idCliente = idCliente;
            this.idSucursal = idGimnasio;
        }

        public BajaFacialClienteRequest() { }

        // Propiedades con getter y setter
        public int IdCliente
        {
            get { return idCliente; }
            set { idCliente = value; }
        }
        public int IdSucursal
        {
            get { return idSucursal; }
            set { idSucursal = value; }
        }


        public override string ToString()
        {
            return "IdCliente: " + idCliente + ", IdGimnasio: " + idSucursal;
        }
    }
}
