using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Data.Domain
{
    [Table("credenciales")]
    public class Credenciales
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }







    }
}
