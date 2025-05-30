using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Data.Domain
{
    [Table("articulos")]
    public class Articulo
    {
        [Key]
        [Column("id")]
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

        public Articulo(int idDx, string name, string amount, char isSaleItem)
        {
            IdDx = idDx;
            Name = name;
            Amount = amount;
            IsSaleItem = isSaleItem;
        }

        public Articulo() { }

        public static bool EsIgual(Articulo local, Articulo remoto)
        {
            return local.IdDx == remoto.IdDx &&
                   local.Name == remoto.Name &&
                   local.Amount == remoto.Amount &&
                   local.IsSaleItem == remoto.IsSaleItem;
        }

    }
}
