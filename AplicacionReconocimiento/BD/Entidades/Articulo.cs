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
        public int id { get;  }

        [Column("id_dx")]
        public int IdDx { get;  }
        public string Name { get;  }
        public double Amount { get; }
        public string IsSaleItem { get; } // 'T' = Articulo, 'F' = Servicio

        public Articulo(int idDx, string nombre, double precio, string esUnArticulo)
        {
            IdDx = idDx;
            Name = nombre;
            Amount = precio;
            IsSaleItem = esUnArticulo;
        }
    }
}
