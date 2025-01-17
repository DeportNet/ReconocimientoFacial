namespace DeportNetReconocimiento.Api.Dtos.Response
{
    public class ValidarAccesoResponse
    {

        // Atributos
        private string idCliente;
        private string nombre;
        private string apellido;
        private string nombreCompleto;
        private string estado;
        private string mensajeCrudo;
        private string mensajeAcceso;
        private string mostrarcumpleanios;
        private string idSucursal;

        //Constructores 
        public ValidarAccesoResponse() { }

        public ValidarAccesoResponse(string id,
            string nombre,
            string apellido,
            string nombreCompleto,
            string estado,
            string mensajeCrudo,
            string mensajeAcceso,
            string mostrarcumpleanios,
            string idSucursal)
        {
            this.idCliente = id;
            this.nombre = nombre;
            this.apellido = apellido;
            this.nombreCompleto = nombreCompleto;
            this.estado = estado;
            this.mensajeCrudo = mensajeCrudo;
            this.mensajeAcceso = mensajeAcceso;
            this.mostrarcumpleanios = mostrarcumpleanios;
            this.idSucursal = idSucursal;
        }


        // Getters y Setters 

        public string IdCliente { get { return idCliente; } set { idCliente = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public string Apellido { get { return apellido; } set { apellido = value; } }
        public string NombreCompleto { get { return nombreCompleto; } set { nombreCompleto = value; } }
        public string Estado { get { return estado; } set { estado = value; } }
        public string MensajeCrudo { get { return mensajeCrudo; } set { mensajeCrudo = value; } }
        public string MensajeAcceso { get { return mensajeAcceso; } set { mensajeAcceso = value; } }
        public string Mostrarcumpleanios { get { return mostrarcumpleanios; } set { mostrarcumpleanios = value; } }
        public string IdSucursal { get { return idSucursal; } set { idSucursal = value; } }

    }
}
