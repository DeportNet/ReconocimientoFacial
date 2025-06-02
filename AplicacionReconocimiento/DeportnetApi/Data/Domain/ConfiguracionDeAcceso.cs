using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;


namespace DeportNetReconocimiento.Api.Data.Domain
{
    [Table("configuracion_de_acceso")]
    public class ConfiguracionDeAcceso
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("cardLength")]
        [AllowNull]
        public int? CardLength { get; set; }

        [Column("startCharacter")]
        [StringLength(5)]
        [AllowNull]

        public string? StartCharacter { get; set; }

        [Column("endCharacter")]
        [StringLength(5)]
        [AllowNull]
        public string? EndCharacter { get; set; }

        [Column("secondStartCharacter")]
        [StringLength(5)]
        [AllowNull]
        public string? SecondStartCharacter { get; set; }

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
