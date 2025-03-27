using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Data.Mapper.Interfaces
{
    public interface IEmpleadoMapper
    {
        public Empleado EmpleadoDtoDxToEmpleado(EmpleadoDtoDx empleadoDtoDx);
        public List<Empleado> ListaEmpleadoDtoDxToListaEmpleado(List<EmpleadoDtoDx> listaEmpleado);


    }
}
