using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;


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
        public string IsActive { get; set; }

        [Column("full_name")]
        [AllowNull]
        public string? FullName { get; set; }

        [Column("is_admin_user")]
        public string? IsAdminUser { get; set; } 
        public Empleado() { }

        public Empleado(int companyMemberId, string firstName, string lastName, string password, string isActive, string isAdminUser)
        {
            CompanyMemberId = companyMemberId;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            IsActive = isActive;
            FullName = JuntarNombreYApellido();
            IsAdminUser = isAdminUser;
        }

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

        public override string ToString()
        {
            return $"{Id} - {FirstName} {LastName}";
        }

    }
}
