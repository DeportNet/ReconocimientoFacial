using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Modelo
{
    [Table("servicios")]
    public class Servicio
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        public int IdDx { get; set; }

        public string Nombre { get; set; }

        public double Precio { get; set; }

        public string EsUnArticulo { get; set; } // 'T' = Articulo, 'F' = Servicio 

        public string Periodo { get; set; }

        public string Dias { get; set; }

    }
}
