using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Data.Domain
{
    [Table("empleados")]
    public class Empleado
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        public string CompanyMemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string IsActive { get; set; }


        public Empleado() { }

        public static bool EsIgual(Empleado local, Empleado remoto)
        {
            return local.CompanyMemberId == remoto.CompanyMemberId &&
                   local.FirstName == remoto.FirstName &&
                   local.LastName == remoto.LastName &&
                   local.Password == remoto.Password &&
                   local.IsActive == remoto.IsActive;
        }

    }
}
