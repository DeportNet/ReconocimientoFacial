using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx;
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

namespace DeportNetReconocimiento.Api.Services
{
    public class SincronizacionService : IFuncionesSincronizacionService
{
        private readonly BdContext _contextBd;
        private string? idSucursal;
        private readonly ISocioMapper _socioMapper;
        private AccesoMapper _accesoMapper = new AccesoMapper();
        public SincronizacionService(BdContext contextBd, ISocioMapper socioMapper)
        {
            _contextBd = contextBd;
            _socioMapper = socioMapper;
            idSucursal = CredencialesUtils.LeerIdSucursal();
        }

        /*VALIDAR SI SE SINCRONIZO HOY*/

        public void SeSincronizoHoy() { 


        }

        /*SOCIOS*/

        public async void SincronizarSocios()
        {
            //0. Verificar la ultima fecha de sincro


            //1. Pegarle al Webservice de DeportNet para obtener todos los clientes, Leer y Convertir el json en una lista de clientes
            List<Socio> listadoDeSociosDx = await ObtenerSociosDelWebserviceAsync();


            if (listadoDeSociosDx.Count == 0)
            {
                return;
            }

            //2. Logica con la base de datos
            InsertarSociosEnTabla(listadoDeSociosDx);

            //3. Registrar la fecha de sincronizacion en la tabla socios



        }
        private async Task<List<Socio>> ObtenerSociosDelWebserviceAsync()
        {

            List<Socio> listadoDeSocios = new List<Socio>();
            if (idSucursal == null)
            {
                return listadoDeSocios;
            }

            string json = await WebServicesDeportnet.ObtenerClientesOffline(idSucursal);
            ListadoClientesDx apiResponse = JsonConvert.DeserializeObject<ListadoClientesDx>(json);

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

            return _socioMapper.MapearListaDtoASocio(apiResponse.Members);

        }

        private async void InsertarSociosEnTabla(List<Socio> listadoSociosDx)
        {
            using var transaction = await _contextBd.Database.BeginTransactionAsync(); // Iniciar transacción
            try
            {
                VerificarCambiosEnTablaSocios(listadoSociosDx);

                await _contextBd.SaveChangesAsync();

                await transaction.CommitAsync();// Confirmamos transaccion

                Console.WriteLine($"Se insertaron {listadoSociosDx.Count} socios en la base de datos.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // En caso de error, deshacer cambios
                Console.WriteLine($"Error al insertar socios: {ex.Message}");
            }
        }
        private async void VerificarCambiosEnTablaSocios(List<Socio> listadoSociosDx)
        {
            List<Socio> listadoSociosLocal = await _contextBd.Socios.ToListAsync();

            // 2️. Determinar cambios
            var nuevosSocios = listadoSociosDx.Where(sDx => !listadoSociosLocal.Any(sl => sl.IdDx == sDx.IdDx)).ToList();
            var sociosActualizados = listadoSociosDx.Where(sDx => listadoSociosLocal.Any(sl => sl.IdDx == sDx.IdDx && !Socio.EsIgual(sl, sDx))).ToList();
            var sociosEliminados = listadoSociosLocal.Where(sl => !listadoSociosDx.Any(sDx => sDx.IdDx == sl.IdDx)).ToList();

            // 3️. Aplicar cambios en la BD
            if (sociosEliminados.Count > 0)
            {
                _contextBd.Socios.RemoveRange(sociosEliminados);
            }
            if (nuevosSocios.Count > 0)
            {
                await _contextBd.Socios.AddRangeAsync(nuevosSocios);
            }
            if (sociosActualizados.Count > 0)
            {
                foreach (var socio in sociosActualizados)
                {
                    var socioLocal = listadoSociosLocal.First(l => l.IdDx == socio.IdDx);
                    _contextBd.Entry(socioLocal).CurrentValues.SetValues(socio);
                }
            }
        }

        /*CONCEPTS*/

        public async void SincronizarConcepts()
        {
            //1. Obtener de Dx los conceptos, tanto como membresias y articulos
            ListadoDeConceptsDx listadoDeConceptsDx = await ObtenerConceptsDelWebserviceAsync();


            if (listadoDeConceptsDx == null )
            {
                return;
            }

            //2. Separar los conceptos en membresias y articulos
            var (membresias, articulos) = ObtenerListadoDeMembresiasYArticulos(listadoDeConceptsDx);

            //3. Logica con la base de datos Membresia
            InsertarMembresiasEnTabla(membresias);

            //4. Logica con la base de datos Articulo
            InsertarArticulosEnTabla(articulos);

            //5. Registrar la fecha de sincronizacion en la tabla concepts
        }
        private async Task<ListadoDeConceptsDx> ObtenerConceptsDelWebserviceAsync()
        {
            ListadoDeConceptsDx listadoDeConceptsDx = new ListadoDeConceptsDx();
            if (idSucursal == null)
            {
                return listadoDeConceptsDx;
            }

            string json = await WebServicesDeportnet.ObtenerConceptsOffline(idSucursal);
            listadoDeConceptsDx = JsonConvert.DeserializeObject<ListadoDeConceptsDx>(json);

            if (listadoDeConceptsDx == null)
            {
                Console.WriteLine("Error al obtener listado de concepts, la respuesta vino null");
                return listadoDeConceptsDx;
            }

            if (listadoDeConceptsDx.Result == "F")
            {
                Console.WriteLine("Error al obtener listado de concepts: " + listadoDeConceptsDx.ErrorMessage);
                return listadoDeConceptsDx;
            }

            return listadoDeConceptsDx;
        }
        private (List<Membresia> Membresias, List<Articulo> Articulos) ObtenerListadoDeMembresiasYArticulos(ListadoDeConceptsDx listadoDeConceptsDx)
        {
            List<Membresia> membresias = new List<Membresia>();
            List<Articulo> articulos = new List<Articulo>();

            if (listadoDeConceptsDx.Concepts == null || listadoDeConceptsDx.Concepts.Count == 0)
            {
                return (membresias, articulos);
            }


            //recorremos la lista de concepts, y separamos las listas de membresias y articulos

            listadoDeConceptsDx.Concepts.ForEach(c =>
            {
                

                switch (c.IsSaleItem)
                {
                    case "F":
                        membresias.Add(new Membresia
                        {
                            IdDx = c.Id,
                            Name = c.Name,
                            Amount = c.Amount,
                            IsSaleItem = c.IsSaleItem,
                            Period = c.Period,
                            Days = c.Days,
                        });
                        break;
                    case "T":
                        articulos.Add(new Articulo
                        {
                            IdDx = c.Id,
                            Name = c.Name,
                            Amount = c.Amount,
                            IsSaleItem = c.IsSaleItem,
                        });

                        break;
                    default:
                        break;
                }

            });
            return (membresias, articulos);
        }
        private async void InsertarArticulosEnTabla(List<Articulo> listadoArticulosDx)
        {
            using var transaction = await _contextBd.Database.BeginTransactionAsync(); // Iniciar transacción
            try
            {
                VerificarCambiosEnTablaArticulos(listadoArticulosDx);

                //Guardamos los cambios
                await _contextBd.SaveChangesAsync();

                //Commiteamos la transaccion
                await transaction.CommitAsync();// Confirmamos transaccion

                Console.WriteLine($"Se insertaron {listadoArticulosDx.Count} articulos en la base de datos.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // En caso de error, deshacer cambios
                Console.WriteLine($"Error al insertar articulos: {ex.Message}");
            }
        }
        private async void VerificarCambiosEnTablaArticulos(List<Articulo> listadoArticulosDx)
        {
            List<Articulo> listadoArticulosLocal = await _contextBd.Articulos.ToListAsync();

            var nuevosArticulos = listadoArticulosDx
                .Where(aDx => !listadoArticulosLocal.Any(al => al.IdDx == aDx.IdDx))
                .ToList();

            var articulosActualizados = listadoArticulosDx
                .Where(aDx => listadoArticulosLocal.Any(al => al.IdDx == aDx.IdDx && !Articulo.EsIgual(al, aDx)))
                .ToList();

            var articulosEliminados = listadoArticulosLocal
                .Where(al => !listadoArticulosDx.Any(aDx => aDx.IdDx == al.IdDx))
                .ToList();

            // 2. Aplicar cambios en la BD
            if (articulosEliminados.Count > 0)
            {
                _contextBd.Articulos.RemoveRange(articulosEliminados);
            }
            if (nuevosArticulos.Count > 0)
            {
                await _contextBd.Articulos.AddRangeAsync(nuevosArticulos);
            }
            if (articulosActualizados.Count > 0)
            {
                foreach (var articulo in articulosActualizados)
                {
                    var articuloLocal = listadoArticulosLocal.First(al => al.IdDx == articulo.IdDx);
                    _contextBd.Entry(articuloLocal).CurrentValues.SetValues(articulo);
                }
            }
        }
        private async void InsertarMembresiasEnTabla(List<Membresia> listadoMembresias)
        {
            using var transaction = await _contextBd.Database.BeginTransactionAsync(); // Iniciar transacción
            try
            {
                VerificarCambiosEnTablaMembresias(listadoMembresias);

                //Guardamos los cambios
                await _contextBd.SaveChangesAsync();

                //Commiteamos la transaccion
                await transaction.CommitAsync();// Confirmamos transaccion

                Console.WriteLine($"Se insertaron {listadoMembresias.Count} membresias en la base de datos.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // En caso de error, deshacer cambios
                Console.WriteLine($"Error al insertar membresias: {ex.Message}");
            }
        }
        private async void VerificarCambiosEnTablaMembresias(List<Membresia> listadoMembresiasDx)
        {
            List<Membresia> listadoMembresiasLocal = await _contextBd.Membresias.ToListAsync();

            // 2️. Determinar cambios
            var nuevasMembresias = listadoMembresiasDx
                .Where(mDx => !listadoMembresiasLocal.Any(ml => ml.IdDx == mDx.IdDx))
                .ToList();

            var membresiasActualizadas = listadoMembresiasDx
                .Where(mDx => listadoMembresiasLocal.Any(ml => ml.IdDx == mDx.IdDx && !Membresia.EsIgual(ml, mDx)))
                .ToList();

            var membresiasEliminadas = listadoMembresiasLocal
                .Where(ml => !listadoMembresiasDx.Any(mDx => mDx.IdDx == ml.IdDx))
                .ToList();

            // 3️. Aplicar cambios en la BD
            if (membresiasEliminadas.Count > 0)
            {
                _contextBd.Membresias.RemoveRange(membresiasEliminadas);
            }
            if (nuevasMembresias.Count > 0)
            {
                await _contextBd.Membresias.AddRangeAsync(nuevasMembresias);
            }
            if (membresiasActualizadas.Count > 0)
            {
                foreach (var unaMActualizada in membresiasActualizadas)
                {
                    var membresiaLocal = listadoMembresiasLocal.First(l => l.IdDx == unaMActualizada.IdDx);
                    _contextBd.Entry(membresiaLocal).CurrentValues.SetValues(unaMActualizada);
                }
            }
        }

        /*ACCESOS*/

        public async void SincronizarAcceso()
        {
            //0 Verificar la ultima fecha de sincornización
            //1 Logica para agarrar los accesos e ir subiendolos
            
        }
        
        private async Task InsertarAccesoSocioEnTabla(AccesoSocio accesoSocio)
        {
            if(accesoSocio == null)
            {
                Console.WriteLine($"El acceso {accesoSocio.Id} es null");
                return;
            }

            try
            {
                //Agrego el acceso socio a la tabla
                await _contextBd.AddAsync(accesoSocio);
                //Guardo los cambios en la tabla
                await _contextBd.SaveChangesAsync();
            }
            catch(Exception ex) 
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
                await _contextBd.Accesos.AddAsync(loteAcceso);
                
                //Completar datos del lote 
                Acceso ultimoLote = await _contextBd.Accesos.OrderByDescending(a => a.Id).FirstOrDefaultAsync();
                ultimoLote.ProcessId = ultimoLote.Id;
                await _contextBd.Accesos.AddAsync(ultimoLote);

                //Llamado al post de enviar lote 
                string json = await  WebServicesDeportnet.EnviarLoteDeAccesos(_accesoMapper.MapearAccesoAAccesoDto(ultimoLote).ToString());

                RespuestaSincronizacionLoteAccesosDx respuestaSincronizacion = System.Text.Json.JsonSerializer.Deserialize<RespuestaSincronizacionLoteAccesosDx>(json);

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
            List<AccesoSocio> accesoSocios = await _contextBd.AccesosSocios.Take(limiteLote).ToListAsync();
            return new Acceso(int.Parse(CredencialesUtils.LeerCredenciales()[4]), accesoSocios);
        }





        /*EMPLEADOS*/

        /*CONFIGURACIONES LOCAL*/

        /*CONFIGURACIONES DX*/
    }
}
