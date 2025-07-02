using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.Empleados;
using DeportNetReconocimiento.Api.Data.Mapper.Interfaces;

namespace DeportNetReconocimiento.Api.Data.Mapper
{
    public class EmpleadoMapper : IEmpleadoMapper
    {
        public Empleado EmpleadoDtoDxToEmpleado(EmpleadoDtoDx empleadoDtoDx)
        {
            return new Empleado
            {
                CompanyMemberId = int.Parse(empleadoDtoDx.CompanyMemberId),
                FirstName = empleadoDtoDx.FirstName,
                LastName = empleadoDtoDx.LastName,
                Password = empleadoDtoDx.Password,
                IsActive = empleadoDtoDx.IsActive,
                FullName = empleadoDtoDx.FirstName + " " + empleadoDtoDx.LastName
            };
        }

        public List<Empleado> ListaEmpleadoDtoDxToListaEmpleado(List<EmpleadoDtoDx> listaEmpleado)
        {
            return listaEmpleado.Select(EmpleadoDtoDxToEmpleado).ToList();
        }

    }
}
