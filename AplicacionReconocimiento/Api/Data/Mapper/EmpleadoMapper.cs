using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.Empleados;
using DeportNetReconocimiento.Api.Data.Mapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                IsActive = empleadoDtoDx.IsActive[0]
            };
        }

        public List<Empleado> ListaEmpleadoDtoDxToListaEmpleado(List<EmpleadoDtoDx> listaEmpleado)
        {
            return listaEmpleado.Select(EmpleadoDtoDxToEmpleado).ToList();
        }

    }
}
