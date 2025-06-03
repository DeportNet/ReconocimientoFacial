using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.DeportnetApi.Data.Dto.Dx.Socios
{
    public class NuevoSocio
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } // 'm' = Masculino, 'f' = Femenino
        public string Email { get; set; }
        public string CardNumber { get; set; } // Nro de tarjeta

    }
}
