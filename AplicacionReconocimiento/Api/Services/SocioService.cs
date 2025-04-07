using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.Socios;
using DeportNetReconocimiento.Api.Data.Mapper.Interfaces;
using DeportNetReconocimiento.Api.Services.Interfaces;
using DeportNetReconocimiento.Utils;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Services
{
    public class SocioService : ISincronizacionSocioService
    {


        private readonly BdContext _bdContext;
        private string? idSucursal;
        private readonly ISocioMapper _socioMapper;

        public SocioService(BdContext bdContext, ISocioMapper socioMapper)
        {
            _bdContext = bdContext;
            _socioMapper = socioMapper;
            idSucursal = "23"; //CredencialesUtils.LeerCredencialEspecifica(4);
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
            ListadoClientesDtoDx apiResponse = JsonConvert.DeserializeObject<ListadoClientesDtoDx>(json);

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
            using var transaction = await _bdContext.Database.BeginTransactionAsync(); // Iniciar transacción
            try
            {
                await VerificarCambiosEnTablaSocios(listadoSociosDx);

                await _bdContext.SaveChangesAsync();

                await transaction.CommitAsync();// Confirmamos transaccion

                Console.WriteLine($"Se insertaron {listadoSociosDx.Count} socios en la base de datos.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // En caso de error, deshacer cambios
                Console.WriteLine($"Error al insertar socios: {ex.Message}");
            }
        }

        private async Task VerificarCambiosEnTablaSocios(List<Socio> listadoSociosDx)
        {
            List<Socio> listadoSociosLocal = await _bdContext.Socios.ToListAsync();

            // 2️. Determinar cambios
            var nuevosSocios = listadoSociosDx.Where(sDx => !listadoSociosLocal.Any(sl => sl.IdDx == sDx.IdDx)).ToList();
            var sociosActualizados = listadoSociosDx.Where(sDx => listadoSociosLocal.Any(sl => sl.IdDx == sDx.IdDx && !Socio.EsIgual(sl, sDx))).ToList();
            var sociosEliminados = listadoSociosLocal.Where(sl => !listadoSociosDx.Any(sDx => sDx.IdDx == sl.IdDx)).ToList();

            // 3️. Aplicar cambios en la BD
            if (sociosEliminados.Count > 0)
            {
                _bdContext.Socios.RemoveRange(sociosEliminados);
            }
            if (nuevosSocios.Count > 0)
            {
                await _bdContext.Socios.AddRangeAsync(nuevosSocios);
            }
            if (sociosActualizados.Count > 0)
            {
                foreach (var socio in sociosActualizados)
                {
                    var socioLocal = listadoSociosLocal.First(l => l.IdDx == socio.IdDx);
                    _bdContext.Entry(socioLocal).CurrentValues.SetValues(socio);
                }
            }
        }
    }
}
