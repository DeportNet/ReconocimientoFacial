namespace DeportNetReconocimiento.Api.Data.Dtos.Dx.Acceso
{
    public class RespuestaSincroLoteAccesosDtoDx
    {

        public string ProcessResult { get; set; }
        public string? ErrorMessage { get; set; }
        public ErrorItemLoteDtoDx[]? ErrorItems { get; set; }


        public RespuestaSincroLoteAccesosDtoDx() { }
    }
}
