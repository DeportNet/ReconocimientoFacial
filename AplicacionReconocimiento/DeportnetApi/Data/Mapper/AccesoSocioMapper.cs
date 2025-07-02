using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.Acceso;
using DeportNetReconocimiento.Api.Data.Mapper.Interfaces;

namespace DeportNetReconocimiento.Api.Data.Mapper
{
    public class AccesoSocioMapper : IAccesoSocioMapper
    {

        public  AccesoSocioDtoDx AccesoSocioToAccesoSocioDtoDx(AccesoSocio acceso)
        {
            AccesoSocioDtoDx accesoSocioDtoDx = new AccesoSocioDtoDx();
            accesoSocioDtoDx.Id = acceso.Id;
            accesoSocioDtoDx.CompanyMemberId = acceso.CompanyMemberId.ToString();
            accesoSocioDtoDx.MemberId = acceso.MemberId.ToString();
            accesoSocioDtoDx.AccessDate = acceso.AccessDate.ToString();
            accesoSocioDtoDx.IsSuccessful = acceso.IsSuccessful.ToString();

            return accesoSocioDtoDx;

        }
        public List<AccesoSocioDtoDx> ListaAccesoSocioToAccesoSocioDtoDx(List<AccesoSocio> accesos)
        {
            return accesos.Select(AccesoSocioToAccesoSocioDtoDx).ToList();
        }
    }
}
