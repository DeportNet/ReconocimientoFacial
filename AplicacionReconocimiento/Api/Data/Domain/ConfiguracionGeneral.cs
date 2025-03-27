using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DeportNetReconocimiento.Api.Data.Domain
{
    [Table("configuracion_general")]
    public class ConfiguracionGeneral
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        //cant maxima lotes
        public int CantMaxLotes { get; set; }

        
        public string ContraseniaBd { get; set; }

        //nombre sucursal
        public string NombreSucursal { get; set; }

        //fecha de sincronizacion
        public DateTime? UltimaFechaSincronizacion { get; set; }

        //anterior fecha de sincronizacion
        public DateTime? AnteriorFechaSincronizacion { get; set; }

        public ConfiguracionGeneral() { }

        public ConfiguracionGeneral(int cantMaxLotes, string contraseniaBd, string nombreSucursal,DateTime? ultimaFechaSincronizacion, DateTime? anteriorFechaSincronizacion)
        {
            CantMaxLotes = cantMaxLotes;
            ContraseniaBd = contraseniaBd;
            NombreSucursal = nombreSucursal;
            UltimaFechaSincronizacion = ultimaFechaSincronizacion;
            AnteriorFechaSincronizacion = anteriorFechaSincronizacion;
        }

    }
}
