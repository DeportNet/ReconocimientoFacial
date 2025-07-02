namespace DeportNetReconocimiento.Api.Data.Dtos.Dx.Concepts
{
    public class ConceptDtoDx
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Amount { get; set; }
        public string? IsSaleItem { get; set; } // 'T' = Articulo, 'F' = Membresia
        public string? Period { get; set; }
        public string? Days { get; set; }

        public ConceptDtoDx() { }




    }
}
