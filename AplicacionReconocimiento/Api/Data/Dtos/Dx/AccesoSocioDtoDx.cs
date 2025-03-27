using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Data.Dtos.Dx
{
    public class AccesoSocioDtoDx
    {
        public string Id {  get; set; }
        public string CompanyMemberId { get; set; }
        public string MemberId { get; set; }
        public string AccessDate { get; set; }
        public string IsSuccessful { get; set; }
    
        public AccesoSocioDtoDx() { }


    }
}
