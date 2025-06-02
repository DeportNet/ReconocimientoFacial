namespace DeportNetReconocimiento.Api.Data.Dtos.Dx.EstadoLoteAcceso
{
    public class EstadoLoteRequest
    {
        private string branchId {  get; set; }
        private string processId { get; set; }

        public EstadoLoteRequest(string branchId, string processId)
        {
            this.branchId = branchId;
            this.processId = processId;
        }

    }
}
