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
    [Table("accesos")]
    public class Acceso
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("active_branch_id")]
        [DisallowNull]
        public int ActiveBranchId { get; set; }

        [Column("process_id")]
        [AllowNull]
        public int? ProcessId { get; set; }

        //[InverseProperty("Acceso")]  // Especificamos la relación inversa con la propiedad de navegación en AccesoSocio

        public List<AccesoSocio>? MemberAccess { get; set; }

        public Acceso() { }

        public Acceso(int activeBranchId, int processId)
        {
            ActiveBranchId = activeBranchId;
            ProcessId = processId;
            MemberAccess = new List<AccesoSocio>();
        }
        public Acceso(int activeBranchId, int processId, List<AccesoSocio> memberAccess)
        {
            ActiveBranchId = activeBranchId;
            ProcessId = processId;
            MemberAccess = memberAccess;
        }

        public Acceso(int activeBranchId, List<AccesoSocio> memberAccess)
        {
            ActiveBranchId = activeBranchId;
            ProcessId = null;
            MemberAccess = memberAccess;
        }

    }
}
