using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.Empleados;
using DeportNetReconocimiento.Api.Data.Mapper.Interfaces;
using DeportNetReconocimiento.Api.Services.Interfaces;
using DeportNetReconocimiento.Utils;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DeportNetReconocimiento.Api.Services
{
    public class EmpleadoService : ISincronizacionEmpleadosService
    {

        private readonly BdContext _bdContext;
        private string? idSucursal;
        private readonly IEmpleadoMapper _empleadoMapper;

        public EmpleadoService(BdContext bdContext, IEmpleadoMapper empleadoMapper)
        {
            _bdContext = bdContext;
            _empleadoMapper = empleadoMapper;
            idSucursal = CredencialesUtils.LeerCredencialEspecifica(4);
        }

        public async Task SincronizarEmpleados()
        {
            //1. Obtener de Dx los empleados
            ListadoEmpleadosDtoDx? listadoDeEmpleadosDx = await ObtenerEmpleadosDelWebserviceAsync();
            Console.WriteLine("Obtenemos empleados de dx");

            if (listadoDeEmpleadosDx == null || listadoDeEmpleadosDx.CountUsers == "0")
            {
                Console.WriteLine("El listado de empleados es null o esta vacio");
                return;
            }

            //2. Obtener el nombre de la sucursal
            Console.WriteLine("Entro a guardar nombre de sucursal");
            GuardarNombreSucursal(listadoDeEmpleadosDx.BranchName);


            //3. Mappear el listado de empleados
            List<Empleado> empleados = _empleadoMapper.ListaEmpleadoDtoDxToListaEmpleado(listadoDeEmpleadosDx.Users);


            //4. Obtener listado de empleados
            await InsertarEmpleadosEnTabla(empleados);

        }

        private async Task<ListadoEmpleadosDtoDx> ObtenerEmpleadosDelWebserviceAsync()
        {


            if (idSucursal == null)
            {
                return null;
            }

            string json = await WebServicesDeportnet.ObtenerEmpleadosSucursalOffline(idSucursal);
            ListadoEmpleadosDtoDx apiResponse = JsonConvert.DeserializeObject<ListadoEmpleadosDtoDx>(json);


            if (apiResponse == null)
            {
                Console.WriteLine("Error al obtener listado de clientes, la respuesta vino null");
                return null;
            }

            if (apiResponse.Result == "F")
            {
                Console.WriteLine("Error al obtener listado de clientes: " + apiResponse.ErrorMessage);
                return null;
            }

            return apiResponse;

        }


        private async Task InsertarEmpleadosEnTabla(List<Empleado> listadoEmpleados)
        {
            using var transaction = await _bdContext.Database.BeginTransactionAsync(); // Iniciar transacción
            try
            {
                await VerificarCambiosEnTablaEmpleados(listadoEmpleados);

                //Guardamos los cambios
                await _bdContext.SaveChangesAsync();

                //Commiteamos la transaccion
                await transaction.CommitAsync();// Confirmamos transaccion

                Console.WriteLine($"Se insertaron {listadoEmpleados.Count} empleados en la base de datos.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // En caso de error, deshacer cambios
                Console.WriteLine($"Error al insertar empleados: {ex.Message}");
            }
        }

        private async Task VerificarCambiosEnTablaEmpleados(List<Empleado> listadoEmpleadosRemoto)
        {
            List<Empleado> listadoEmpleadosLocal = await _bdContext.Empleados.ToListAsync();

            // 2️. Determinar cambios
            var empleadosNuevos = listadoEmpleadosRemoto
                .Where(eDx => !listadoEmpleadosLocal.Any(eLoc => eLoc.CompanyMemberId == eDx.CompanyMemberId))
                .ToList();

            var empleadosActualizados = listadoEmpleadosRemoto
                .Where(eDx => listadoEmpleadosLocal.Any(eLoc => eLoc.CompanyMemberId == eDx.CompanyMemberId && !Empleado.EsIgual(eLoc, eDx)))
                .ToList();

            var empleadosEliminados = listadoEmpleadosLocal
                .Where(eLoc => !listadoEmpleadosRemoto.Any(eDx => eDx.CompanyMemberId == eLoc.CompanyMemberId))
                .ToList();

            // 3️. Aplicar cambios en la BD
            if (empleadosEliminados.Count > 0)
            {
                _bdContext.Empleados.RemoveRange(empleadosEliminados);
            }
            if (empleadosNuevos.Count > 0)
            {
                await _bdContext.Empleados.AddRangeAsync(empleadosNuevos);
            }
            if (empleadosActualizados.Count > 0)
            {
                foreach (var unEActualizado in empleadosActualizados)
                {
                    var membresiaLocal = listadoEmpleadosLocal.First(eLoc => eLoc.CompanyMemberId == unEActualizado.CompanyMemberId);
                    _bdContext.Entry(membresiaLocal).CurrentValues.SetValues(unEActualizado);
                }
            }
        }

        private void GuardarNombreSucursal(string nombreSucursal)
        {
            using (var context = BdContext.CrearContexto())
            {
                try
                {
                    ConfiguracionGeneral? configuracion = context.ConfiguracionGeneral.FirstOrDefault();

                    if(configuracion!= null)
                    {
                        configuracion.NombreSucursal = nombreSucursal;
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al guardar el nombre de la sucursal: " + ex.Message);
                }
            }

        }

    }
}
