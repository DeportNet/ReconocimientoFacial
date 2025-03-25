using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Data.Dtos.Dx
{
    public class AccesoDtoDx
    {
        public int ActiveBranchId { get; set; }
        public int? ProcessId { get; set; }
        public List<AccesoSocioDtoDx> MemberAccess { get; set; }


        public AccesoDtoDx() { }

    }
}
