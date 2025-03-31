using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
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


        [Column("company_member_id")]
        [DisallowNull]
        public int CompanyMemberId { get; set; }

        [Column("first_name")]
        [StringLength(100)]
        [DisallowNull]
        public string FirstName { get; set; }

        [Column("last_name")]
        [StringLength(100)]
        [DisallowNull]
        public string LastName { get; set; }

        [Column("password")]
        [StringLength(64)]
        [DisallowNull]
        public string Password { get; set; }
        
        [Column("is_active")]
        [DisallowNull]
        public char IsActive { get; set; }


        public Empleado() { }

        public string JuntarNombreYApellido()
        {
            return FirstName + " " + LastName;
        }

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
