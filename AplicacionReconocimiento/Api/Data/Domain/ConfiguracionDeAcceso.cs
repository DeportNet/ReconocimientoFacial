using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Data.Domain
{
    [Table("configuracion_de_acceso")]
    public class ConfiguracionDeAcceso
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
        public int CardLength { get; set; }
        public string StartCharacter { get; set; }
        public string EndCharacter { get; set; }
        public string SecondStartCharacter { get; set; }

        public ConfiguracionDeAcceso() { }

        public ConfiguracionDeAcceso(int cardLength, string startCharacter, string endCharacter, string secondStartCharacter)
        {
            CardLength = cardLength;
            StartCharacter = startCharacter;
            EndCharacter = endCharacter;
            SecondStartCharacter = secondStartCharacter;
        }
    }
}
