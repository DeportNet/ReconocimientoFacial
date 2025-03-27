using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;


namespace DeportNetReconocimiento.Api.Data.Domain
{
    [Table("configuracion_general")]
    public class ConfiguracionGeneral
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        //cant maxima lotes
        [Column("max_lot_quantity")]
        [DisallowNull]
        [DefaultValue(200)]
        public int CantMaxLotes { get; set; }

        [Column("password")]
        [StringLength(50)]
        [Required]
        public string ContraseniaBd { get; set; }
        
        [Column("branch_name")]
        [StringLength(100)]
        [Required]
        [DisallowNull]
        //nombre sucursal
        public string NombreSucursal { get; set; }

        [Column("last_syncro")]
        [AllowNull]
        //fecha de sincronizacion
        public DateTime? UltimaFechaSincronizacion { get; set; }

        //anterior fecha de sincronizacion
        [Column("prior_last_syncro")]
        [AllowNull]
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
