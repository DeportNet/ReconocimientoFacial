using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.Acceso;
using DeportNetReconocimiento.Api.Data.Mapper.Interfaces;
using DeportNetReconocimiento.Api.Services.Interfaces;
using DeportNetReconocimiento.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Services
{
    public class AccesoService : ISincronizarAccesoService
    {

        private string? idSucursal;
        private BdContext _bdContext;
        private IAccesoMapper _accesoMapper;

        public AccesoService(BdContext bdContext, IAccesoMapper accesoMapper) 
        {
            _bdContext = bdContext;
            _accesoMapper = accesoMapper;
            idSucursal = CredencialesUtils.LeerCredencialEspecifica(4);
        }


        public async void SincronizarAcceso()
        {
            //0 Verificar la ultima fecha de sincornización
            //1 Logica para agarrar los accesos e ir subiendolos

        }
        private async Task InsertarAccesoSocioEnTabla(AccesoSocio accesoSocio)
        {
            if (accesoSocio == null)
            {
                Console.WriteLine($"El acceso {accesoSocio.Id} es null");
                return;
            }

            try
            {
                //Agrego el acceso socio a la tabla
                await _bdContext.AddAsync(accesoSocio);
                //Guardo los cambios en la tabla
                await _bdContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar el acceso {accesoSocio.Id} en la base de datos: {ex.Message}");
            }
        }
        public async Task EnviarLoteDeAccesos()
        {
            try
            {
                Acceso loteAcceso = await CrearLoteAcceso();

                //Guardar lote en la BD
                await _bdContext.Accesos.AddAsync(loteAcceso);

                //Completar datos del lote 
                Acceso ultimoLote = await _bdContext.Accesos.OrderByDescending(a => a.Id).FirstOrDefaultAsync();
                ultimoLote.ProcessId = ultimoLote.Id;
                await _bdContext.Accesos.AddAsync(ultimoLote);

                //Llamado al post de enviar lote 
                string json = await WebServicesDeportnet.EnviarLoteDeAccesos(_accesoMapper.AccesoToAccesoDtoDx(ultimoLote).ToString());

                RespuestaSincronizacionLoteAccesosDtoDx respuestaSincronizacion = System.Text.Json.JsonSerializer.Deserialize<RespuestaSincronizacionLoteAccesosDtoDx>(json);

                Console.WriteLine($"Respuesta de sincronización de lote {ultimoLote.ProcessId} es {respuestaSincronizacion.ProcessResult}. " +
                    $"\nMensaje de error: {respuestaSincronizacion.ErrorMessage}" +
                    $"\nCampos con error: {respuestaSincronizacion.ErrorItems.ToList()}");
            }
            catch (Exception ex)
            {
                Console.Write($"Error al sincronizar el lote de accesos {ex.Message}");
            }
        }
        public async Task<Acceso> CrearLoteAcceso()
        {
            int limiteLote = 20;
            List<AccesoSocio> accesoSocios = await _bdContext.AccesosSocios.Take(limiteLote).ToListAsync();
            return new Acceso(int.Parse(CredencialesUtils.LeerCredencialEspecifica(4)), accesoSocios);
        }
    }
}
