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

        [Column("id_dx")]
        [DisallowNull]
        [Required]
        public int IdDx { get; set; }

        [Column("email")]
        [StringLength(100)]
        [EmailAddress]
        [DisallowNull]
        [Required]
        public required string Email { get; set; }

        [Column("first_name")]
        [StringLength(100)]
        [DisallowNull]
        [Required]
        public required string FirstName { get; set; }

        [Required]
        [Column("last_name")]
        [StringLength(100)]
        [DisallowNull]
        public required string LastName { get; set; }

        [Column("id_number")]
        [StringLength(50)]
        [AllowNull]
        public string? IdNumber { get; set; }

        [Column("birth_date")]
        [AllowNull]
        public DateTime? BirthDate { get; set; }

        [Column("cellphone")]
        [StringLength(50)]
        [AllowNull]
        public string? Cellphone { get; set; }

        [Column("is_active")]
        [StringLength(1)]
        [AllowNull]
        public string? IsActive { get; set; } // '1' = Activo, '0' = Inactivo

        [Column("card_number")]
        [StringLength(100)]
        [AllowNull]
        public string? CardNumber { get; set; }

        [Column("address")]
        [StringLength(100)]
        [AllowNull]
        public string? Address { get; set; }

        [Column("address_with_floor")]
        [StringLength(100)]
        [AllowNull]
        public string? AddressWithFloor { get; set; }

        [Column("image_url")]
        [StringLength(250)]
        [AllowNull]
        public string? ImageUrl { get; set; }

        [Column("gender")]
        [StringLength(1)]
        [AllowNull]
        public string? Gender { get; set; } // 'f' = Femenino, 'm' = Masculino

        [Column("is_valid")]
        [StringLength(1)]
        [AllowNull]
        public string? IsValid { get; set; } // Validación previa

        public Socio(int idDx, string email, string firstName, string lastName, string? idNumber, DateTime? birthDate, string? cellphone, string? isActive, string? cardNumber, string? address, string? addressWithFloor, string? imageUrl, string? gender, string isValid)
        {
            IdDx = idDx;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            IdNumber = idNumber;
            BirthDate = birthDate;
            Cellphone = cellphone;
            IsActive = isActive;
            CardNumber = cardNumber;
            Address = address;
            AddressWithFloor = addressWithFloor;
            ImageUrl = imageUrl;
            Gender = gender;
            IsValid = isValid;
        }


    }
}
