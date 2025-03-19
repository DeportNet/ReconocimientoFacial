using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.BD.Entidades
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
        public double Amount { get; set; }
        public string IsSaleItem { get; set; } // 'T' = Articulo, 'F' = Servicio 
        public string Period { get; set; }
        public string Days { get; set; }

        public Membresia() { }
        public Membresia(int idDx, string name, double amount, string isSaleItem, string period, string days)
        {
            IdDx = idDx;
            Name = name;
            Amount = amount;
            IsSaleItem = isSaleItem;
            Period = period;
            Days = days;
        }

    }
}
