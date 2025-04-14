using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DeportNetReconocimiento.Api.Data.Domain
{
    [Table("accesos_socios")]
    public class AccesoSocio
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }

        [Column("company_member_id")]
        [DisallowNull]
        public int CompanyMemberId { get; set; }

        [Column("member_id")]
        [DisallowNull]
        public int MemberId { get; set; }

        [Column("access_date")]
        [DisallowNull]
        public DateTime AccessDate { get; set; }

        [Column("isSuccessful")]
        [DisallowNull]
        public bool IsSuccessful { get; set; }

        [Column("access_id")]
        [AllowNull]
        public int? AccesoId { get; set; }

        [ForeignKey("AccesoId")]
        public virtual Acceso? Acceso { get; set; }

        public AccesoSocio() { }

        public AccesoSocio(int memberId, DateTime accessDate, bool isSuccessful)
        {
            MemberId = memberId;
            AccessDate = accessDate;
            IsSuccessful = isSuccessful;
        }

        public AccesoSocio(int memberId, int companyMemberId, DateTime accessDate, bool isSuccessful, int accesoId)
        {
            MemberId = memberId;
            CompanyMemberId = companyMemberId;
            AccessDate = accessDate;
            IsSuccessful = isSuccessful;
            AccesoId = accesoId;
        }
    }
}
