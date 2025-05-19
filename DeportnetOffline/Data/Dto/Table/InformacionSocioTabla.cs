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
        public int? IdDx { get; set; }
        public string NombreYApellido {  get; set; }
        public string NroTarjeta {  get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }
        public string Edad {  get; set; }
        public string Celular { get; set; }
        public string Direccion { get; set; }
        public string Piso { get; set; }
        public string Sexo {  get; set; }
        public string Estado {  get; set; }
        public string Synchronized { get; set; }
        public DateTime? SyncronizedDate { get; set; }

        public InformacionSocioTabla() { }

        public InformacionSocioTabla(int? id, string nombreYApellido, string nroTarjeta, string dni, string email, 
            string edad, string celular, string direccion, string piso, string sexo, string estado, string synchronized, DateTime? syncronizedDate)
        {
            Id = id;
            IdDx = idDx;
            NombreYApellido = nombreYApellido;
            NroTarjeta = nroTarjeta;
            Dni = dni;
            Email = email;
            Edad = edad;
            Celular = celular;
            Direccion = direccion;
            Piso = piso;
            Sexo = sexo;
            Estado = estado;
            Synchronized = synchronized;
            SyncronizedDate = syncronizedDate;
        }
    }
}
