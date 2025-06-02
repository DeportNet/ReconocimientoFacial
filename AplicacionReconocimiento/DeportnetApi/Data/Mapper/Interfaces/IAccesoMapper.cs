using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.Acceso;

namespace DeportNetReconocimiento.Api.Data.Mapper.Interfaces
{
    public interface IAccesoMapper
    {
        AccesoDtoDx AccesoToAccesoDtoDx(Acceso acceso);
       // List<AccesoDtoDx> MapearListaDtoASocio(List<Acceso> accesos);
    }
}
