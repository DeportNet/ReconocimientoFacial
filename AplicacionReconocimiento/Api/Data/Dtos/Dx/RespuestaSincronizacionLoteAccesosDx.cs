using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Data.Dtos.Dx
{
    public class RespuestaSincronizacionLoteAccesosDx
    {

        public string ProcessResult {  get; set; }
        public string? ErrorMessage { get; set; }
        public ErrorItemLoteDx[]? ErrorItems { get; set; }


        public RespuestaSincronizacionLoteAccesosDx() { }
    }
}
