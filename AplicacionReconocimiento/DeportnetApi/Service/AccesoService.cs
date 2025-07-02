using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.Acceso;
using DeportNetReconocimiento.Api.Data.Mapper.Interfaces;
using DeportNetReconocimiento.Api.Services.Interfaces;
using DeportNetReconocimiento.Utils;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DeportNetReconocimiento.Api.Services
{
    public class AccesoService : ISincronizarAccesoService
    {

        private string? idSucursal;
        private IAccesoMapper _accesoMapper;

        public AccesoService(BdContext bdContext, IAccesoMapper accesoMapper) 
        {
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
            using var bdContext = BdContext.CrearContexto();

            if (accesoSocio == null)
            {
                Console.WriteLine($"El acceso {accesoSocio.Id} es null");
                return;
            }

            try
            {
                //Agrego el acceso socio a la tabla
                await bdContext.AddAsync(accesoSocio);
                //Guardo los cambios en la tabla
                await bdContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar el acceso {accesoSocio.Id} en la base de datos: {ex.Message}");
            }
        }
        public async Task EnviarLoteDeAccesos()
        {
            using var bdContext = BdContext.CrearContexto();


            try
            {
                Acceso loteAcceso = await CrearLoteAcceso();

                //Guardar lote en la BD
                await bdContext.Accesos.AddAsync(loteAcceso);

                //Completar datos del lote 
                Acceso? ultimoLote = await bdContext.Accesos.OrderByDescending(a => a.Id).FirstOrDefaultAsync();

                if(ultimoLote == null)
                {
                    Console.WriteLine("No se pudo obtener el ultimo lote a la hora de enviar el lote de acceso: Lote " + ultimoLote);
                    return;
                }

                    ultimoLote.ProcessId = ultimoLote.Id;
                    await bdContext.Accesos.AddAsync(ultimoLote);

                //Llamado al post de enviar lote 
                string json = await WebServicesDeportnet.EnviarLoteDeAccesos(_accesoMapper.AccesoToAccesoDtoDx(ultimoLote).ToString());

                RespuestaSincroLoteAccesosDtoDx? respuestaSincro = JsonConvert.DeserializeObject<RespuestaSincroLoteAccesosDtoDx>(json);

                ManejarRespuestaSincronizacionLoteAccesos(respuestaSincro, ultimoLote);


            }
            catch (Exception ex)
            {
                Console.Write($"Error al sincronizar el lote de accesos {ex.Message}");
            }
        }

        private void ManejarRespuestaSincronizacionLoteAccesos(RespuestaSincroLoteAccesosDtoDx respuestaSincro, Acceso loteAcceso)
        {


            Console.WriteLine($"Respuesta de sincronización de lote {loteAcceso.ProcessId} es {respuestaSincro.ProcessResult}. " +
                                $"\nMensaje de error: {respuestaSincro.ErrorMessage}" +
                                $"\nCampos con error: {respuestaSincro.ErrorItems.ToList()}");


            if (respuestaSincro == null)
            {
                ManejarSincronizacionSinRespuesta(loteAcceso);
            }

            switch (respuestaSincro.ProcessResult)
            {
                case "T":
                    ManejarSincronizacionExitosa(loteAcceso);
                    break;
                case "F":
                    ManejarSincronizacionErronea(respuestaSincro, loteAcceso);
                    break;
            }


        }

        private async Task ManejarSincronizacionExitosa(Acceso lote)
        {
            using var context = BdContext.CrearContexto();

            //Agarrar todos los registros del lote y eliminarlos 
            if(lote.MemberAccess != null)
            {    
                context.AccesosSocios.RemoveRange(lote.MemberAccess);
                await context.SaveChangesAsync();   
            }
        }

        private void ManejarSincronizacionErronea(RespuestaSincroLoteAccesosDtoDx respuestaSincro, Acceso lote)
        {
            Console.WriteLine("Manejo el error de sincronización erronea - Acceso service " + respuestaSincro.ErrorMessage);
            
            //Ver que registros son correctos y cuales no 
                //Los que no son correctos procesarlos
        }

        private async void ManejarSincronizacionSinRespuesta(Acceso lote)
        {
            VerificarEstadoLoteDtoDx data = new VerificarEstadoLoteDtoDx(idSucursal, lote.ProcessId.ToString());
            string json = JsonConvert.SerializeObject(data);
            string respuesta = await WebServicesDeportnet.VerificarEstadoLoteAcceso(json);
            VerificarEstadoLoteDtoDxResponse estado = JsonConvert.DeserializeObject<VerificarEstadoLoteDtoDxResponse>(respuesta);

            if(estado.Result == null)
            {
               await EnviarLoteDeAccesos();
            }

            if(estado.Result == "T")
            {
            }
            

            Console.WriteLine("La respuesta de la sincronziación  es null");
            //Hacer petición para verificar si el lote está actualizado en DX

        }


        private async Task<Acceso> CrearLoteAcceso()
        {
            using var bdContext = BdContext.CrearContexto();

            int limiteLote = 20;
            List<AccesoSocio> accesoSocios = await bdContext.AccesosSocios.Take(limiteLote).ToListAsync();
            return new Acceso(int.Parse(CredencialesUtils.LeerCredencialEspecifica(4)), accesoSocios);
        }
    }
}
