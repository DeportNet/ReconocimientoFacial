namespace DeportNetReconocimiento.Api.Data.Dtos.Dx.ConfigAcceso
{
    public class ConfigAccesoDtoDx
    {
        public int? CardLength { get; set; }
        public string? StartCharacter { get; set; }
        public string? EndCharacter { get; set; }
        public string? SecondStartCharacter { get; set; }

        public ConfigAccesoDtoDx() { }

        public ConfigAccesoDtoDx(int cardLength, string startCharacter, string endCharacter, string secondStartCharacter)
        {
            CardLength = cardLength;
            StartCharacter = startCharacter;
            EndCharacter = endCharacter;
            SecondStartCharacter = secondStartCharacter;
        }




    }
}
