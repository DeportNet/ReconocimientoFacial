using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.GlobalExceptionHandler
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; } // Opcional, solo para depuración
        public string Timestamp { get; set; } 

        public ErrorDetails(int statusCode, string message, string stackTrace = null)
        {
            StatusCode = statusCode;
            Message = message;
            StackTrace = stackTrace;
            Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        // Serializa la respuesta como JSON
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
