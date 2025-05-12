using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

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

        [Column("name")]
        [StringLength(100)]
        [DisallowNull]
        public string Name { get; set; }

        [Column("amount")]
        [StringLength(50)]
        [DisallowNull]
        public string Amount { get; set; }
        [Column("isSaleItem")]
        [DisallowNull]
        public char IsSaleItem { get; set; } // 'T' = Articulo, 'F' = Servicio 
        [Column("period")]
        [AllowNull]
        public string? Period { get; set; }

        [Column("days")]
        [AllowNull]
        public string? Days { get; set; }

        public Membresia() { }
        public Membresia(int idDx, string name, string amount, char isSaleItem, string period, string days)
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
