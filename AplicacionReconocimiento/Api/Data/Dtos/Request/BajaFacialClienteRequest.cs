using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Data.Dtos.Request
{
    public class BajaFacialClienteRequest
    {


        private int idCliente;
        private int idSucursal;
        private string[] arregloIdClientes;

        public BajaFacialClienteRequest(int idCliente, int idSucursal)
        {
            this.idCliente = idCliente;
            this.idSucursal = idSucursal;
            arregloIdClientes = [];
        }

        public BajaFacialClienteRequest(string[] arregloIdClientes, int idSucursal)
        {
            this.arregloIdClientes = arregloIdClientes;
            this.idSucursal = idSucursal;
            idCliente = -1;
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
