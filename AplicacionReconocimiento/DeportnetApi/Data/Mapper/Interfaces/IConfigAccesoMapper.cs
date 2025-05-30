using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.ConfigAcceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Data.Mapper.Interfaces
{
    public interface IConfigAccesoMapper
    {
        public ConfiguracionDeAcceso RespuestaConfigAccesoToConfiguracionDeAcceso(RespuestaConfigAcceso respuestaConfigAcceso);


    }
}
