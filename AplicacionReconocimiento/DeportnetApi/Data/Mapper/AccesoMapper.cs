using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.Acceso;
using DeportNetReconocimiento.Api.Data.Mapper.Interfaces;

namespace DeportNetReconocimiento.Api.Data.Mapper
{
    public class AccesoMapper : IAccesoMapper
    {

        AccesoSocioMapper _accesoSocioMaper = new AccesoSocioMapper();
        public AccesoDtoDx AccesoToAccesoDtoDx(Acceso acceso)
        {
            AccesoDtoDx accesoDtoDx = new AccesoDtoDx();
            accesoDtoDx.ActiveBranchId = acceso.ActiveBranchId;
            accesoDtoDx.ProcessId = acceso.ProcessId;
            accesoDtoDx.MemberAccess = _accesoSocioMaper.ListaAccesoSocioToAccesoSocioDtoDx(acceso.MemberAccess);

            return accesoDtoDx;
        }

    }
}
