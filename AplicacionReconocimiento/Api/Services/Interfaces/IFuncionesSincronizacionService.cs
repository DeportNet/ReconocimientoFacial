using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Services.Interfaces
{
    public interface IFuncionesSincronizacionService
    {
        public Task SincronizarTodasLasTablasDx();
        public bool SeSincronizoHoy();
        public void ActualizarFechaSincronizacion();
        public Task SincronizarSocios();
        public Task SincronizarEmpleados();
        public Task SincronizarConcepts();

    }
}
