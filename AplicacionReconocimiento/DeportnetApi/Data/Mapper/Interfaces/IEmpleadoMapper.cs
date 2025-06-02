using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.Empleados;

namespace DeportNetReconocimiento.Api.Data.Mapper.Interfaces
{
    public interface IEmpleadoMapper
    {
        public Empleado EmpleadoDtoDxToEmpleado(EmpleadoDtoDx empleadoDtoDx);
        public List<Empleado> ListaEmpleadoDtoDxToListaEmpleado(List<EmpleadoDtoDx> listaEmpleado);


    }
}
