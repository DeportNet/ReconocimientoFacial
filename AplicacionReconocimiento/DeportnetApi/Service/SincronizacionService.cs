using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Mapper;
using DeportNetReconocimiento.Api.Data.Mapper.Interfaces;
using DeportNetReconocimiento.Api.Services.Interfaces;
using DeportNetReconocimiento.Utils;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Runtime.Intrinsics.X86;
using System.Xml.Serialization;
using Windows.UI;
using System.Text.Json;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.Acceso;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.ConfigAcceso;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.Socios;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.Concepts;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.Empleados;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;


namespace DeportNetReconocimiento.Api.Services
{
    public class SincronizacionService : IFuncionesSincronizacionService
{
        private readonly BdContext _contextBd;
        private readonly ISincronizacionSocioService _socioService;
        private readonly ISincronizacionEmpleadosService _empleadoService;
        private readonly ISincronizacionConceptsService _conceptService;
        private readonly ISincronizacionConfiguracionDeAccesoService _configuracionAccesoService;

        public SincronizacionService(BdContext contextBd ,ISincronizacionConfiguracionDeAccesoService sincronizacionConfigAccesoService, ISincronizacionConceptsService conceptsService, ISincronizacionEmpleadosService empleadosService, ISincronizacionSocioService socioService)
        {
            _contextBd = contextBd;
            _socioService = socioService;
            _empleadoService = empleadosService;
            _conceptService = conceptsService;
            _configuracionAccesoService = sincronizacionConfigAccesoService;
            //idSucursal = CredencialesUtils.LeerCredencialesBd().BranchId;//CredencialesUtils.LeerCredencialEspecifica(4);// "23";
            
        }

        /*TRAERSE TABLAS DE DX*/

        public async Task RecibirTodasLasTablasDx()
        {

            if (SeSincronizoHoy())
            {
                Console.WriteLine("Ya se sincronizo hoy todas las tablas de dx");
                return;
            }

            //1. Obtener de Dx los empleados
            await _empleadoService.SincronizarEmpleados();
            
            //2. Obtener de Dx los concepts
            await _conceptService.SincronizarConcepts();

            ////3. Obtener de Dx los clientes
            // Hay que inportar esto
            await _socioService.SincronizarSocios(); 

            ////4. Obtener Configuracion de Acceso
            await _configuracionAccesoService.SincronizarConfiguracionDeAcceso();


            // Actualizamos la fecha de sincronizacion
            ActualizarFechaSincronizacion();
        }

        /*VALIDAR SI SE SINCRONIZO HOY*/
        public bool SeSincronizoHoy() {

            ConfiguracionGeneral? config = ConfiguracionGeneralUtils.ObtenerConfiguracionGeneral();

            if (config == null)
            {
                ConfiguracionGeneral confGeneral = new ConfiguracionGeneral();
                _contextBd.ConfiguracionGeneral.AddAsync(confGeneral);
            }
            bool flag = false;
            DateTime? ultimaFecha = _contextBd.ConfiguracionGeneral
                              .OrderBy(c => c.Id == 1) 
                              .Select(c => c.UltimaFechaSincronizacion)
                              .FirstOrDefault();

            Console.WriteLine("ultima fecha de sincro: " + ultimaFecha.ToString());

            //si la fecha es null, no se sincronizo nunca o si la fecha es 01/01/0001 00:00:00
            if (ultimaFecha == null || ultimaFecha == DateTime.MinValue) {
                return flag;
            }

            DateTime fechaActual = DateTime.Now;

            //si la fecha de sincro es igual a la fecha actual, ya se sincronizo hoy
            if (ultimaFecha.Value.Date == fechaActual.Date)
            {
                flag = true;
            }

            return flag;
        }

        public void ActualizarFechaSincronizacion()
        {
            ConfiguracionGeneral? config = _contextBd.ConfiguracionGeneral
                .OrderBy(c => c.Id == 1)
                .FirstOrDefault();


            
            if (config == null)
            {
                Console.WriteLine("Config es null");
                return;
            }
            

            try
            {

                config.AnteriorFechaSincronizacion = config.UltimaFechaSincronizacion;
                config.UltimaFechaSincronizacion = DateTime.Now;
                _contextBd.SaveChanges();

                Console.WriteLine("Se actualizo la ultima fecha de sincronizacion");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar fecha de sincronizacion: " + ex.Message);
            }

        }

        /*ENVIAR TODAS LAS TABLAS A DX*/

        public async Task EnviarTodasLasTablasDx()
        {
            //1. Enviar a los socios NUEVOS
            await _socioService.EnviarNuevosSocios();


            //1. Enviar los empleados
            await _empleadoService.EnviarEmpleados();

            //2. Enviar los concepts
            await _conceptService.EnviarConcepts();


            //4. Enviar configuracion de acceso
            await _configuracionAccesoService.EnviarConfiguracionDeAcceso();

        }
}
