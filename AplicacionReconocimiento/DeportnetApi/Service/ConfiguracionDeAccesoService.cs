using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.ConfigAcceso;
using DeportNetReconocimiento.Api.Data.Mapper.Interfaces;
using DeportNetReconocimiento.Api.Services.Interfaces;
using DeportNetReconocimiento.Utils;
using Newtonsoft.Json;

namespace DeportNetReconocimiento.Api.Services
{
    public class ConfiguracionDeAccesoService : ISincronizacionConfiguracionDeAccesoService
    {

        private string? idSucursal;
        private readonly BdContext _bdContext;
        private readonly IConfigAccesoMapper _configAccesoMapper;


        public ConfiguracionDeAccesoService(BdContext bdContext, IConfigAccesoMapper configAccesoMapper)
        {
            _bdContext = bdContext;
            _configAccesoMapper = configAccesoMapper;
            idSucursal = CredencialesUtils.LeerCredencialEspecifica(4);
        }

        public async Task SincronizarConfiguracionDeAcceso()
        {
            ConfiguracionDeAcceso? configAcceso = await ObtenerConfiguracionDeAccesoDelWebserviceAsync();

            if (configAcceso == null)
            {
                return;
            }

            //Guardamos la config en la BD
            await InsertarConfigAccesoTabla(configAcceso);
        }

        private async Task<ConfiguracionDeAcceso?> ObtenerConfiguracionDeAccesoDelWebserviceAsync()
        {
            if (idSucursal == null)
            {
                return null;
            }
            string json = await WebServicesDeportnet.ObtenerCofiguracionDeAccesoOffline(idSucursal);
            RespuestaConfigAcceso apiResponse = JsonConvert.DeserializeObject<RespuestaConfigAcceso>(json);

            if (apiResponse == null)
            {
                Console.WriteLine("Error al obtener configuracion de acceso, la respuesta vino null");
                return null;
            }

            if (apiResponse.Result == "F")
            {
                Console.WriteLine("Error al obtener configuracion de acceso: " + apiResponse.ErrorMessage);
                return null;
            }

            return _configAccesoMapper.RespuestaConfigAccesoToConfiguracionDeAcceso(apiResponse);
        }

        private async Task InsertarConfigAccesoTabla(ConfiguracionDeAcceso configuracionDeAcceso)
        {
            using var transaction = await _bdContext.Database.BeginTransactionAsync(); // Iniciar transacción
            try
            {
                //1. Eliminamos datos de tabla ConfigAcceso
                _bdContext.ConfiguracionDeAcceso.RemoveRange(_bdContext.ConfiguracionDeAcceso);


                _bdContext.ConfiguracionDeAcceso.Add(configuracionDeAcceso);

                //Guardamos los cambios
                await _bdContext.SaveChangesAsync();

                //Commiteamos la transaccion
                await transaction.CommitAsync();// Confirmamos transaccion

                Console.WriteLine($"Se elimino e inserto la configAcceso en la base de datos.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // En caso de error, deshacer cambios
                Console.WriteLine($"Error al insertar empleados: {ex.Message}");
            }
        }
    }
}
