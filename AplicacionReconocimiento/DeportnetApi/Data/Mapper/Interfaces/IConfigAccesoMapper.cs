using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.ConfigAcceso;

namespace DeportNetReconocimiento.Api.Data.Mapper.Interfaces
{
    public interface IConfigAccesoMapper
    {
        public ConfiguracionDeAcceso RespuestaConfigAccesoToConfiguracionDeAcceso(RespuestaConfigAcceso respuestaConfigAcceso);


    }
}
