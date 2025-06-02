using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.Socios;

namespace DeportNetReconocimiento.Api.Data.Mapper.Interfaces
{
    public interface ISocioMapper
    {
        Socio SocioDtoDxToSocio(SocioDtoDx socioDto);
        List<Socio> ListaSocioDtoDxToListaSocio(List<SocioDtoDx> sociosDto);
    }
}
