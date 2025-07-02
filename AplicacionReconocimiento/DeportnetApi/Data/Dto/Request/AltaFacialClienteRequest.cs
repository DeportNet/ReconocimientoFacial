namespace DeportNetReconocimiento.Api.Data.Dtos.Request
{
    public class AltaFacialClienteRequest
    {

        private int idCliente;
        private int idSucursal;
        private string nombreCliente;

        public AltaFacialClienteRequest(int idCliente, int idGimnasio, string nombreCliente)
        {
            this.idCliente = idCliente;
            idSucursal = idGimnasio;
            this.nombreCliente = nombreCliente;
        }

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

        public string NombreCliente
        {
            get { return nombreCliente; }
            set { nombreCliente = value; }
        }

        public override string ToString()
        {
            return "IdCliente: " + idCliente + ", IdGimnasio: " + idSucursal + ", NombreCliente: " + nombreCliente;
        }

    }

}
