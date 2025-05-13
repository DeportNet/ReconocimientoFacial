using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Data.Dtos.Dx.Acceso
{
    public class VerificarEstadoLoteDtoDxResponse
    {
        public string Result { get; set; }
        public string ResultItems { get; set; }

        public VerificarEstadoLoteDtoDxResponse()
        {

        }

        public VerificarEstadoLoteDtoDxResponse(string result, string resultItems)
        {
            Result = result;
            ResultItems = resultItems;
        }
    }
}
