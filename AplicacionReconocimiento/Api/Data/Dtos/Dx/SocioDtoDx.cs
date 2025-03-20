using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Data.Dtos.Dx
{
    public class SocioDtoDx
    {
        public int Id { get; set; }
        public  string? Email { get; set; }
        public  string? FirstName { get; set; }
        public  string? LastName { get; set; }
        public string? IdNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Cellphone { get; set; }
        public string? IsActive { get; set; } // '1' = Activo, '0' = Inactivo
        public string? CardNumber { get; set; }
        public string? Address { get; set; }
        public string? AddressFloor { get; set; }
        public string? ImageUrl { get; set; }
        public string? Gender { get; set; } // 'f' = Femenino, 'm' = Masculino
        public string? IsValid { get; set; } // Validación previa


        public SocioDtoDx() { }
    }
}
