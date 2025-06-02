namespace DeportNetReconocimiento.Api.Data.Dtos.Dx.ConfigAcceso
{
    public class RespuestaConfigAcceso
    {
        public ConfigAccesoDtoDx? BranchAccess { get; set; }
        public string? Result { get; set; }
        public string? ErrorMessage { get; set; }
    
        public RespuestaConfigAcceso() { }
    }
}
