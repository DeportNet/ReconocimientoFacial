using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Data.Mapper.Interfaces
{
    internal interface IAccesoSocioMapper
    {

        AccesoSocioDtoDx AccesoSocioToAccesoSocioDtoDx(AccesoSocio acceso);
        List<AccesoSocioDtoDx> ListaAccesoSocioToAccesoSocioDtoDx(List<AccesoSocio> accesos);
    }
}
