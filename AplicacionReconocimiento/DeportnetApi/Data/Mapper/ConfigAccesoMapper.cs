using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.ConfigAcceso;
using DeportNetReconocimiento.Api.Data.Mapper.Interfaces;

namespace DeportNetReconocimiento.Api.Data.Mapper
{
    public class ConfigAccesoMapper : IConfigAccesoMapper
    {
        public ConfiguracionDeAcceso RespuestaConfigAccesoToConfiguracionDeAcceso(RespuestaConfigAcceso respuestaConfigAcceso)
        {
            ConfigAccesoDtoDx configAccesoDto = respuestaConfigAcceso.BranchAccess;

            return new ConfiguracionDeAcceso
            {
                CardLength = configAccesoDto.CardLength,
                StartCharacter = configAccesoDto.StartCharacter,
                EndCharacter = configAccesoDto.EndCharacter,
                SecondStartCharacter = configAccesoDto.SecondStartCharacter
            };
        }
    }
}
