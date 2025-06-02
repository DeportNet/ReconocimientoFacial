namespace DeportNetReconocimiento.Api.Data.Dtos.Dx.Socios
{
    public class ListadoClientesDtoDx
    {
        public List<SocioDtoDx>? Members { get; set; }
        public string? Result { get; set; }
        public string? ErrorMessage { get; set; }
        public int? CountMembers { get; set; }

        public ListadoClientesDtoDx() { }
    }
}
