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
    public class AccesoSocioMapper : IAccesoSocioMapper
    {

        public  AccesoSocioDtoDx MapearAccesoSocioAAccesoSocioDto(AccesoSocio acceso)
        {
            AccesoSocioDtoDx accesoSocioDtoDx = new AccesoSocioDtoDx();
            accesoSocioDtoDx.Id = acceso.Id;
            accesoSocioDtoDx.MemberId = acceso.MemberId;
            accesoSocioDtoDx.AccessDate = acceso.AccessDate;
            accesoSocioDtoDx.IsSuccessful = acceso.IsSuccessful;

            return accesoSocioDtoDx;

        }
        public List<AccesoSocioDtoDx> MapearListaAccesoSocioAAccesoSocioDto(List<AccesoSocio> accesos)
        {
            return accesos.Select(MapearAccesoSocioAAccesoSocioDto).ToList();
        }
    }
}
