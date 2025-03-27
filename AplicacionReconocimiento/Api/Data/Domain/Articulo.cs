using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int id { get; set; }

        [Column("id_dx")]
        public int IdDx { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public string IsSaleItem { get; set; } // 'T' = Articulo, 'F' = Servicio

        public Articulo(int idDx, string nombre, string precio, string esUnArticulo)
        {
            IdDx = idDx;
            Name = nombre;
            Amount = precio;
            IsSaleItem = esUnArticulo;
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
