using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Dtos.Response
{
    public class DetallesResponse
    {
        public int statusCode { get; set; }
        public string errorMessage { get; set; }
        //public string stackTrace { get; set; } // Opcional, solo para depuración
        public string timeStamp { get; set; }
        public string result { get; set; }

        public DetallesResponse(string result, int statusCode, string message = null /*string stackTrace = null*/)
        {
            this.statusCode = statusCode;
            errorMessage = message;
            //this.stackTrace = stackTrace;
            timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.result = result;
        }

        // Serializa la respuesta como JSON
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
