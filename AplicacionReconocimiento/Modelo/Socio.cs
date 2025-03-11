using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace DeportNetReconocimiento.Modelo
{
    [Table("socios")]
    public class Socio
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("idDx")]
        [DisallowNull]
        [Required]
        public int IdDx { get; set; }

        [Column("email")]
        [StringLength(100)]
        [EmailAddress]
        [DisallowNull]
        [Required]
        public required string Email { get; set; }

        [Column("firstName")]
        [StringLength(100)]
        [DisallowNull]
        [Required]
        public required string Nombre { get; set; }

        [Required]
        [Column("lastName")]
        [StringLength(100)]
        [DisallowNull]
        public required string Apellido { get; set; }

        [Column("idNumber")]
        [StringLength(50)]
        [AllowNull]
        public string? NumeroDocumento { get; set; }

        [Column("birthDate")]
        [AllowNull]
        public DateTime? FechaNacimiento { get; set; }

        [Column("cellphone")]
        [StringLength(50)]
        [AllowNull]
        public string? Celular { get; set; }

        [Column("isActive")]
        [StringLength(1)]
        [AllowNull]
        public string? Activo { get; set; } // '1' = Activo, '0' = Inactivo

        [Column("cardNumber")]
        [StringLength(100)]
        [AllowNull]
        public string? TarjetaIngreso { get; set; }

        [Column("address")]
        [StringLength(100)]
        [AllowNull]
        public string? Domicilio { get; set; }

        [Column("addressWithFloor")]
        [StringLength(100)]
        [AllowNull]
        public string? DomicilioConPiso { get; set; }

        [Column("imageUrl")]
        [StringLength(250)]
        [AllowNull]
        public string? ImagenUrl { get; set; }

        [Column("gender")]
        [StringLength(1)]
        [AllowNull]
        public string? Sexo { get; set; } // 'f' = Femenino, 'm' = Masculino

        [Column("isValid")]
        [StringLength(1)]
        [AllowNull]
        public string? Vigente { get; set; } // Validación previa
    }
}
