using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Modelo
{
    public class Articulo
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Column("id_dx")]
        public int IdDx { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public string IsSaleItem { get; set; } // 'T' = Articulo, 'F' = Servicio

        public Articulo(int idDx, string nombre, double precio, string esUnArticulo)
        {
            IdDx = idDx;
            Name = nombre;
            Amount = precio;
            IsSaleItem = esUnArticulo;
        }

        public Articulo() { }
    }
}
