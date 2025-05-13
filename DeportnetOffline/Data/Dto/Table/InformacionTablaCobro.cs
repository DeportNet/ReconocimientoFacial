using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportnetOffline.Data.Dto.Table
{
    public class InformacionTablaCobro
    {
        public int Id { get; set; }
        public int IdSocio { get; set; }
        public char IsSaleItem { get; set; } // T = Articulo, F = Membresia
        public string FullNameSocio { get; set; }
        public string ItemName { get; set; }
        public double Amount { get; set; }
        public DateTime? SaleDate { get; set; }
        public string Synchronized { get; set; }
        public DateTime? SyncronizedDate { get; set; }
        

        public InformacionTablaCobro() { }

        public InformacionTablaCobro(int id, int idSocio, char isSaleItem,string fullNameSocio, string itemName, double amount, DateTime? date, string synchronized, DateTime? syncronizedDate)
        {
            Id = id;
            IdSocio = idSocio;
            IsSaleItem = isSaleItem;
            FullNameSocio = fullNameSocio;
            ItemName = itemName;
            Amount = amount;
            SaleDate = date;
            Synchronized = synchronized;
            SyncronizedDate = syncronizedDate;
        }

    }
}
