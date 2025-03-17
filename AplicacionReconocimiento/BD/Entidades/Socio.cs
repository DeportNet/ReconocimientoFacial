using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace DeportNetReconocimiento.BD.Entidades
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
        public required string FirstName { get; set; }

        [Required]
        [Column("lastName")]
        [StringLength(100)]
        [DisallowNull]
        public required string LastName { get; set; }

        [Column("idNumber")]
        [StringLength(50)]
        [AllowNull]
        public string? IdNumber { get; set; }

        [Column("birthDate")]
        [AllowNull]
        public DateTime? BirthDate { get; set; }

        [Column("cellphone")]
        [StringLength(50)]
        [AllowNull]
        public string? Cellphone { get; set; }

        [Column("isActive")]
        [StringLength(1)]
        [AllowNull]
        public string? IsActive { get; set; } // '1' = Activo, '0' = Inactivo

        [Column("cardNumber")]
        [StringLength(100)]
        [AllowNull]
        public string? CardNumber { get; set; }

        [Column("address")]
        [StringLength(100)]
        [AllowNull]
        public string? Address { get; set; }

        [Column("addressWithFloor")]
        [StringLength(100)]
        [AllowNull]
        public string? AddressWithFloor { get; set; }

        [Column("imageUrl")]
        [StringLength(250)]
        [AllowNull]
        public string? ImageUrl { get; set; }

        [Column("gender")]
        [StringLength(1)]
        [AllowNull]
        public string? Gender { get; set; } // 'f' = Femenino, 'm' = Masculino

        [Column("isValid")]
        [StringLength(1)]
        [AllowNull]
        public string? IsValid { get; set; } // Validación previa


        public Socio(int idDx, string email, string nombre, string apellido, string? numeroDocumento, DateTime? fechaNacimiento, string? celular, string? activo, string? tarjetaIngreso, string? domicilio, string? domicilioConPiso, string? imagenUrl, string? sexo, string? vigente)
        {
            IdDx = idDx;
            Email = email;
            Nombre = nombre;
            Apellido = apellido;
            NumeroDocumento = numeroDocumento;
            FechaNacimiento = fechaNacimiento;
            Celular = celular;
            IsActive = activo;
            CardNumber = tarjetaIngreso;
            Address = domicilio;
            AddressWithFloor = domicilioConPiso;
            ImageUrl = imagenUrl;
            Gender = sexo;
            IsValid = vigente;
        }



    }
}
