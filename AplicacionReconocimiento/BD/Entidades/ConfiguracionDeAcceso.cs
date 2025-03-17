using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Modelo
{
    [Table("configuracion_de_acceso")]
    public class ConfiguracionDeAcceso
    {
        private int CardLength { get; set; }
        private string StartCharacter { get; set; }
        private string EndCharacter { get; set; }
        private string SecondStartCharacter { get; set; }

        public ConfiguracionDeAcceso(int cardLength, string startCharacter, string endCharacter, string secondStartCharacter)
        {
            CardLength = cardLength;
            StartCharacter = startCharacter;
            EndCharacter = endCharacter;
            SecondStartCharacter = secondStartCharacter;
        }
    }
}
