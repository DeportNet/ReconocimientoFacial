using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportnetOffline.Data.Dto.Table
{
    public class InformacionSocioTabla
    {
        public int? Id { get; set; }
        public string NombreYApellido {  get; set; }
        public string NroTarjeta {  get; set; }
        public string DNI { get; set; }
        public string Email { get; set; }
        public string Edad {  get; set; }
        public string Celular { get; set; }
        public string Direccion { get; set; }
        public string Piso { get; set; }
        public string Sexo {  get; set; }
        public string Estado {  get; set; }
    }
}
