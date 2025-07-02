namespace DeportNetReconocimiento.Api.Data.Dtos.Dx.Acceso
{
    public class AccesoDtoDx
    {
        public int ActiveBranchId { get; set; }
        public int? ProcessId { get; set; }
        public List<AccesoSocioDtoDx> MemberAccess { get; set; }


        public AccesoDtoDx() { }

    }
}
