namespace DeportNetReconocimiento.Api.Data.Dtos.Dx.Acceso
{
    public class VerificarEstadoLoteDtoDx
    {
        public string BranchId {  get; set; }
        public string ProcessId {  get; set; }

        public VerificarEstadoLoteDtoDx()
        {

        }

        public VerificarEstadoLoteDtoDx(string branchId, string processId)
        {
            this.BranchId = branchId;
            this.ProcessId = processId;
        }
    }
}
