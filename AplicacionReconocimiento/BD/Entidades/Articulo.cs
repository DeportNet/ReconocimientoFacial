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
        private int id { get; set; }

        [Column("id_dx")]
        private int IdDx { get; set; }
        private string Name { get; set; }
        private double Amount { get; set; }
        private string IsSaleItem { get; set; } // 'T' = Articulo, 'F' = Servicio

        public Articulo(int idDx, string nombre, double precio, string esUnArticulo)
        {
            IdDx = idDx;
            Name = nombre;
            Amount = precio;
            IsSaleItem = esUnArticulo;
        }
    }
}
