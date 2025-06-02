using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.Acceso;

namespace DeportNetReconocimiento.Api.Data.Mapper.Interfaces
{
    internal interface IAccesoSocioMapper
    {

        AccesoSocioDtoDx AccesoSocioToAccesoSocioDtoDx(AccesoSocio acceso);
        List<AccesoSocioDtoDx> ListaAccesoSocioToAccesoSocioDtoDx(List<AccesoSocio> accesos);
    }
}
