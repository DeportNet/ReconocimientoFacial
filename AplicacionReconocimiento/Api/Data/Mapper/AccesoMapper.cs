using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx;
using DeportNetReconocimiento.Api.Data.Mapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Data.Mapper
{
    public class AccesoMapper : IAccesoMapper
    {

        AccesoSocioMapper _accesoSocioMapeper = new AccesoSocioMapper();
        public AccesoDtoDx MapearAccesoAAccesoDto(Acceso acceso)
        {
            AccesoDtoDx accesoDtoDx = new AccesoDtoDx();
            accesoDtoDx.ActiveBranchId = acceso.ActiveBranchId;
            accesoDtoDx.ProcessId = acceso.ProcessId;
            accesoDtoDx.MemberAccess = _accesoSocioMapeper.MapearListaAccesoSocioAAccesoSocioDto(acceso.MemberAccess);

            return accesoDtoDx;
        }

    }
}
