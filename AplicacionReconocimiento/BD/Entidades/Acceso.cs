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
        public int Id { get;  }
        public int ActiveBranchId { get; set; }
        public int ProcessId { get; set;  }

        //Tabla pivot ?
        public List<AccesoSocio> MemberAccess{get; set;}

        //public Acceso(int activeBranchId, int processId)
        //{
        //    ActiveBranchId = activeBranchId;
        //    ProcessId = processId;
        //    MemberAccess = new List<AccesoSocio>();
        //}

        public Acceso(int activeBranchId, int processId, List<AccesoSocio> memberAccess)
        {
            ActiveBranchId = activeBranchId;
            ProcessId = processId;
            MemberAccess = memberAccess;
        }

    }
}
