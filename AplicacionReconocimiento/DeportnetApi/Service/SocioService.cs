using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.Socios;
using DeportNetReconocimiento.Api.Data.Mapper.Interfaces;
using DeportNetReconocimiento.Api.Services.Interfaces;
using DeportNetReconocimiento.DeportnetApi.Data.Dto.Dx.Socios;
using DeportNetReconocimiento.Utils;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DeportNetReconocimiento.Api.Services
{
    public class SocioService : ISincronizacionSocioService
    {
        private string? idSucursal;
        private readonly ISocioMapper _socioMapper;

        public SocioService(BdContext bdContext, ISocioMapper socioMapper)
        {
            _socioMapper = socioMapper;
            idSucursal = CredencialesUtils.LeerCredencialEspecifica(4);
        }


        public async Task SincronizarSocios()
        {
            
            //1. Pegarle al Webservice de DeportNet para obtener todos los clientes, Leer y Convertir el json en una lista de clientes
            List<Socio> listadoDeSociosDx = await ObtenerSociosDelWebserviceAsync();


            if (listadoDeSociosDx.Count == 0 || listadoDeSociosDx == null)
            {
                Console.WriteLine("El listado de socios es null o esta vacio");
                return;
            }

            //2. Logica con la base de datos
            await InsertarSociosEnTabla(listadoDeSociosDx);
        }

        private async Task<List<Socio>> ObtenerSociosDelWebserviceAsync()
        {

            List<Socio> listadoDeSocios = new List<Socio>();
            if (idSucursal == null)
            {
                return listadoDeSocios;
            }

            string json = await WebServicesDeportnet.ObtenerClientesOffline(idSucursal);
            ListadoSociosDtoDx apiResponse = JsonConvert.DeserializeObject<ListadoSociosDtoDx>(json);

            if (apiResponse == null)
            {
                Console.WriteLine("Error al obtener listado de clientes, la respuesta vino null");
                return listadoDeSocios;
            }

            if (apiResponse.Result == "F")
            {
                Console.WriteLine("Error al obtener listado de clientes: " + apiResponse.ErrorMessage);
                return listadoDeSocios;
            }

            return _socioMapper.ListaSocioDtoDxToListaSocio(apiResponse.Members);

        }

        private async Task InsertarSociosEnTabla(List<Socio> listadoSociosDx)
        {
            using var bdContext = BdContext.CrearContexto();
            using var transaction = await bdContext.Database.BeginTransactionAsync(); // Iniciar transacción
            try
            {
                await VerificarCambiosEnTablaSocios(listadoSociosDx, bdContext);
                await bdContext.SaveChangesAsync();

                await transaction.CommitAsync();// Confirmamos transaccion

                Console.WriteLine($"Se insertaron {listadoSociosDx.Count} socios en la base de datos.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // En caso de error, deshacer cambios
                Console.WriteLine($"Error al insertar socios: {ex.Message}");
            }
        }

        private async Task VerificarCambiosEnTablaSocios(List<Socio> listadoSociosDx, BdContext bdContext)
        {

            List<Socio> listadoSociosLocal = await bdContext.Socios.ToListAsync();

            // 2️. Determinar cambios
            var nuevosSocios = listadoSociosDx.Where(sDx => !listadoSociosLocal.Any(sl => sl.IdDx == sDx.IdDx)).ToList();
            var sociosActualizados = listadoSociosDx.Where(sDx => listadoSociosLocal.Any(sl => sl.IdDx == sDx.IdDx && !Socio.EsIgual(sl, sDx))).ToList();
            var sociosEliminados = listadoSociosLocal.Where(sl => !listadoSociosDx.Any(sDx => sDx.IdDx == sl.IdDx)).ToList();

            // 3️. Aplicar cambios en la BD
            if (sociosEliminados.Count > 0)
            {
                bdContext.Socios.RemoveRange(sociosEliminados);
            }
            if (nuevosSocios.Count > 0)
            {
                await bdContext.Socios.AddRangeAsync(nuevosSocios);
            }
            if (sociosActualizados.Count > 0)
            {
                foreach (var socio in sociosActualizados)
                {
                    var socioLocal = listadoSociosLocal.First(l => l.IdDx == socio.IdDx);
                    bdContext.Entry(socioLocal).CurrentValues.SetValues(socio);
                }
            }
        }

        public static async Task ActualizarEstadoSocio(int? idSocio, int estado)
        {
            using var context = BdContext.CrearContexto();

            
            Socio socio = context.Socios.Find(idSocio);
            if(socio == null)
            {
                Console.WriteLine("No se encontró al socio con id " + idSocio);
                return;
            }

            socio.IsValid = estado == 1 ? "T" : "F";

            context.SaveChanges();
            Console.WriteLine("Estado de socio actualizado con exito");

        }

        private List<Socio> ObtenerListadoNuevosSocios()
        {
            using var bdContext = BdContext.CrearContexto();

            List<Socio> listadoNuevosSocios = bdContext.Socios
                .Where(s => s.IdDx == null || s.IdDx == 0)
                .ToList();
            return listadoNuevosSocios;
        }

        public async Task EnviarNuevosSocios()
        {
            List<Socio> listadoSocios = ObtenerListadoNuevosSocios();

            if (listadoSocios.Count == 0)
            {
                Console.WriteLine("No hay nuevos socios para enviar");
                
            }

            List<NuevoSocio> listadoNuevosSociosParsed = _socioMapper.ListaSocioToListaNuevoSocio(listadoSocios);
            string jsonRta = await WebServicesDeportnet.EnviarNuevosSocios(listadoNuevosSociosParsed, idSucursal);

            //falta agregar los nuevos ids, a los socios nuevos






        }
    }
}
