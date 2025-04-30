using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DeportnetOffline.Data.Dto.Table
{
    public class InformacionTablaNuevoLegajo : InformacionSocioTabla
    {
        public string IsSincronizado { get; set; }
        public DateTime FechaHoraSincronizado { get; set; }


        public InformacionTablaNuevoLegajo(string isSincronizado, DateTime fechaHoraSincronizado, int? id, string nombreYApellido, string nroTarjeta, string dni, string email, string edad, string celular, string direccion, string piso, string sexo, string estado)
            : base(id,nombreYApellido,nroTarjeta,dni,email, edad, celular, direccion, piso, sexo, estado)
        {
            IsSincronizado = isSincronizado;
            FechaHoraSincronizado = fechaHoraSincronizado;
        }
    }
}
