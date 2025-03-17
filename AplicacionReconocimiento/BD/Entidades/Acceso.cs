using DeportNetReconocimiento.BD.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Modelo
{
    public class Acceso
    {
        [Key]
        [Column("id")]
        private int Id { get; set; }
        private int ActiveBranchId { get; set; }
        private int ProcessId { get; set; }
        private List<AccesoMiembro> MemberAccess{get; set;}

        public Acceso(int activeBranchId, int processId)
        {
            ActiveBranchId = activeBranchId;
            ProcessId = processId;
            MemberAccess = new List<AccesoMiembro>();
        }

        public Acceso(int activeBranchId, int processId, List<AccesoMiembro> memberAccess)
        {
            ActiveBranchId = activeBranchId;
            ProcessId = processId;
            MemberAccess = memberAccess;
        }

    }
}
