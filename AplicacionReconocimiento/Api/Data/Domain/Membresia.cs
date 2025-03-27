using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Data.Domain
{
    [Table("membresias")]
    public class Membresia
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("id_dx")]
        public int IdDx { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public string IsSaleItem { get; set; } // 'T' = Articulo, 'F' = Servicio 
        public string Period { get; set; }
        public string Days { get; set; }

        public Membresia() { }
        public Membresia(int idDx, string name, string amount, string isSaleItem, string period, string days)
        {
            IdDx = idDx;
            Name = name;
            Amount = amount;
            IsSaleItem = isSaleItem;
            Period = period;
            Days = days;
        }

        public static bool EsIgual(Membresia local, Membresia remota)
        {
            return 
            local.IdDx == remota.IdDx &&
            local.Name == remota.Name &&
            local.Amount == remota.Amount &&
            local.IsSaleItem == remota.IsSaleItem &&
            local.Period == remota.Period &&
            local.Days == remota.Days;


        }
    }
}
