using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.GlobalExceptionHandler.Exceptions
{
    public class DispositivoEnUsoException : Exception
    {
        public DispositivoEnUsoException(string message) : base(message)
        {
        }

        public DispositivoEnUsoException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
