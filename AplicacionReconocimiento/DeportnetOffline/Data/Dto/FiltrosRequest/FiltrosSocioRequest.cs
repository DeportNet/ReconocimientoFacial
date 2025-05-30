using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportnetOffline.Data.Dto.FiltrosRequest
{
    public class FiltrosSocioRequest
    {
        public string? Estado { get; set; }
        public string? NroTarjeta { get; set; }
        public string? ApellidoNombre { get; set; }
        public string? Email { get; set; }

        public FiltrosSocioRequest(string? estado, string? nroTarjeta, string? apellidoNombre, string? email)
        {
            Estado = estado;
            NroTarjeta = nroTarjeta;
            ApellidoNombre = apellidoNombre;
            Email = email;
        }
    }
}
