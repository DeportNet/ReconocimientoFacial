namespace DeportnetOffline.Data.Dto.Table
{
    public class InformacionTablaCobro
    {
        public int Id { get; set; }
        public int? IdSocio { get; set; }
        public int? IdDx { get; set; }
        public string IsSaleItem { get; set; } 
        public string FullNameSocio { get; set; }
        public string ItemName { get; set; }
        public string Amount { get; set; }
        public DateTime? SaleDate { get; set; }
        public string Synchronized { get; set; }
        public DateTime? SynchronizedDate { get; set; }
        

        public InformacionTablaCobro() { }

        public InformacionTablaCobro(int id, int idSocio,int? idDx, string isSaleItem,string fullNameSocio, string itemName, string amount, DateTime? date, string synchronized, DateTime? syncronizedDate)
        {
            Id = id;
            IdSocio = idSocio;
            IdDx = IdDx;
            IsSaleItem = isSaleItem;
            FullNameSocio = fullNameSocio;
            ItemName = itemName;
            Amount = amount;
            SaleDate = date;
            Synchronized = synchronized;
            SynchronizedDate = syncronizedDate;
        }

    }
}
