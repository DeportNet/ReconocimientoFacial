using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Data.Domain
{
    public class AccesoSocio
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }

        public string MemberId { get; set; }
        public string AccessDate { get; set; }
        public string IsSuccessful { get; set; }

        public int? AccesoId { get; set; }
        public virtual Acceso? Acceso { get; set; }

        public AccesoSocio() { }

        public AccesoSocio(string memberId, string accessDate, string isSuccessful)
        {
            MemberId = memberId;
            AccessDate = accessDate;
            IsSuccessful = isSuccessful;
        }

        public AccesoSocio(string memberId, string accessDate, string isSuccessful, int accesoId)
        {
            MemberId = memberId;
            AccessDate = accessDate;
            IsSuccessful = isSuccessful;
            AccesoId = accesoId;
        }
    }
}
