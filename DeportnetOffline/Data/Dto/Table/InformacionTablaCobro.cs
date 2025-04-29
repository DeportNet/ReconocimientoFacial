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
        public string NombreYApellido { get; set; }
        public string ItemName { get; set; }
        public float Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string IsSincronizado { get; set; }
        public DateTime HoraSincro { get; set; }
        
    }
}
