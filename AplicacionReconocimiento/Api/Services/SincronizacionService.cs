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
        private string? idSucursal;
        private readonly ISocioMapper _socioMapper;
        private readonly IAccesoMapper _accesoMapper;
        private readonly IEmpleadoMapper _empleadoMapper;
        private readonly IConfigAccesoMapper _configAccesoMapper;

        public SincronizacionService(BdContext contextBd, ISocioMapper socioMapper, IAccesoMapper accesoMapper, IEmpleadoMapper empleadoMapper, IConfigAccesoMapper configAccesoMapper)
        {
            _contextBd = contextBd;
            _socioMapper = socioMapper;
            _accesoMapper = accesoMapper;
            _empleadoMapper = empleadoMapper;
            _configAccesoMapper = configAccesoMapper;
            //idSucursal = CredencialesUtils.LeerCredencialesBd().BranchId;//CredencialesUtils.LeerCredencialEspecifica(4);// "23";
            
        }

        /*TRAERSE TABLAS DE DX*/

        public async Task SincronizarTodasLasTablasDx()
        {
            idSucursal = "23"; //CredencialesUtils.LeerCredencialesBd().BranchId;//CredencialesUtils.LeerCredencialEspecifica(4);// 

            if (SeSincronizoHoy())
            {
                Console.WriteLine("Ya se sincronizo hoy todas las tablas de dx");
                return;
            }

            //1. Obtener de Dx los empleados
            await SincronizarEmpleados();
            
            //2. Obtener de Dx los concepts
            await SincronizarConcepts();

            ////3. Obtener de Dx los clientes
            await SincronizarSocios();

            ////4. Obtener Configuracion de Acceso
            await SincronizarConfiguracionDeAcceso();


            // Actualizamos la fecha de sincronizacion
            ActualizarFechaSincronizacion();
        }


        /*VALIDAR SI SE SINCRONIZO HOY*/
        public bool SeSincronizoHoy() {

            ConfiguracionGeneral? config = _contextBd.ConfiguracionGeneral
               .OrderBy(c => c.Id == 1)
               .FirstOrDefault();

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


        /*SOCIOS*/

        public async Task SincronizarSocios()
        {
            //0. Verificar la ultima fecha de sincro


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
            using var transaction = await _contextBd.Database.BeginTransactionAsync(); // Iniciar transacción
            try
            {
                await VerificarCambiosEnTablaSocios(listadoSociosDx);

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
        private async Task VerificarCambiosEnTablaSocios(List<Socio> listadoSociosDx)
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
        public async Task SincronizarConcepts()
        {
            //1. Obtener de Dx los conceptos, tanto como membresias y articulos
            ListadoDeConceptsDtoDx listadoDeConceptsDx = await ObtenerConceptsDelWebserviceAsync();


            if (listadoDeConceptsDx == null || listadoDeConceptsDx.ConceptsCount == 0)
            {
                Console.WriteLine("El listado de concepts es null o esta vacio");
                return;
            }

            //2. Separar los conceptos en membresias y articulos
            var (membresias, articulos) = ObtenerListadoDeMembresiasYArticulos(listadoDeConceptsDx);

            //3. Logica con la base de datos Membresia
            await InsertarMembresiasEnTabla(membresias);

            //4. Logica con la base de datos Articulo
            await InsertarArticulosEnTabla(articulos);

            //5. Registrar la fecha de sincronizacion en la tabla concepts
        }
        private async Task<ListadoDeConceptsDtoDx> ObtenerConceptsDelWebserviceAsync()
        {
            ListadoDeConceptsDtoDx listadoDeConceptsDx = new ListadoDeConceptsDtoDx();
            if (idSucursal == null)
            {
                return listadoDeConceptsDx;
            }

            string json = await WebServicesDeportnet.ObtenerConceptsOffline(idSucursal);
            listadoDeConceptsDx = JsonConvert.DeserializeObject<ListadoDeConceptsDtoDx>(json);

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
        private (List<Membresia> Membresias, List<Articulo> Articulos) ObtenerListadoDeMembresiasYArticulos(ListadoDeConceptsDtoDx listadoDeConceptsDx)
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
                            Amount = double.Parse(c.Amount),
                            IsSaleItem = c.IsSaleItem[0],
                            Period = int.Parse(c.Period),
                            Days = int.Parse(c.Days),
                        });
                        break;
                    case "T":
                        articulos.Add(new Articulo
                        {
                            IdDx = c.Id,
                            Name = c.Name,
                            Amount = double.Parse(c.Amount),
                            IsSaleItem = c.IsSaleItem[0],
                        });

                        break;
                    default:
                        break;
                }

            });
            return (membresias, articulos);
        }
        private async Task InsertarArticulosEnTabla(List<Articulo> listadoArticulosDx)
        {
            using var transaction = await _contextBd.Database.BeginTransactionAsync(); // Iniciar transacción
            try
            {
                await VerificarCambiosEnTablaArticulos(listadoArticulosDx);

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
        private async Task VerificarCambiosEnTablaArticulos(List<Articulo> listadoArticulosDx)
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
        private async Task InsertarMembresiasEnTabla(List<Membresia> listadoMembresias)
        {
            using var transaction = await _contextBd.Database.BeginTransactionAsync(); // Iniciar transacción
            try
            {
                await VerificarCambiosEnTablaMembresias(listadoMembresias);

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
        private async Task VerificarCambiosEnTablaMembresias(List<Membresia> listadoMembresiasDx)
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
                string json = await  WebServicesDeportnet.EnviarLoteDeAccesos(_accesoMapper.AccesoToAccesoDtoDx(ultimoLote).ToString());

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
            List<AccesoSocio> accesoSocios = await _contextBd.AccesosSocios.Take(limiteLote).ToListAsync();
            return new Acceso(int.Parse(CredencialesUtils.LeerCredencialEspecifica(4)), accesoSocios);
        }

        /*EMPLEADOS*/

        public async Task SincronizarEmpleados()
        {
            //1. Obtener de Dx los empleados
            ListadoEmpleadosDtoDx? listadoDeEmpleadosDx = await ObtenerEmpleadosDelWebserviceAsync();
            Console.WriteLine("Obtenemos empleados de dx");

            if(listadoDeEmpleadosDx == null || listadoDeEmpleadosDx.CountUsers == "0")
            {
                Console.WriteLine("El listado de empleados es null o esta vacio");
                return;
            }

            //2. Obtener el nombre de la sucursal
            await GuardarNombreSucursal(listadoDeEmpleadosDx.BranchName);
            

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
        private async Task GuardarNombreSucursal(string nombreSucursal)
        {
            ConfiguracionGeneral? config = _contextBd.ConfiguracionGeneral.FirstOrDefault(); // o por ID si lo tenés

            if (config == null)
            {
                return;
            }

            try
            {
                config.NombreSucursal = nombreSucursal;
                _contextBd.SaveChanges();
                
            }catch(Exception ex)
            {
                Console.WriteLine("Error al guardar el nombre de la sucursal: "+ ex.Message);
            }

        }

        private async Task InsertarEmpleadosEnTabla(List<Empleado> listadoEmpleados)
        {
            using var transaction = await _contextBd.Database.BeginTransactionAsync(); // Iniciar transacción
            try
            {
                await VerificarCambiosEnTablaEmpleados(listadoEmpleados);

                //Guardamos los cambios
                await _contextBd.SaveChangesAsync();

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
            List<Empleado> listadoEmpleadosLocal = await _contextBd.Empleados.ToListAsync();

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
                _contextBd.Empleados.RemoveRange(empleadosEliminados);
            }
            if (empleadosNuevos.Count > 0)
            {
                await _contextBd.Empleados.AddRangeAsync(empleadosNuevos);
            }
            if (empleadosActualizados.Count > 0)
            {
                foreach (var unEActualizado in empleadosActualizados)
                {
                    var membresiaLocal = listadoEmpleadosLocal.First(eLoc => eLoc.CompanyMemberId == unEActualizado.CompanyMemberId);
                    _contextBd.Entry(membresiaLocal).CurrentValues.SetValues(unEActualizado);
                }
            }
        }

        /*CONFIGURACIONES ACCESO*/

        public async Task SincronizarConfiguracionDeAcceso()
        {
            ConfiguracionDeAcceso? configAcceso = await ObtenerConfiguracionDeAccesoDelWebserviceAsync();

            if (configAcceso == null) {
                return;
            }



        }

        private async Task<ConfiguracionDeAcceso?> ObtenerConfiguracionDeAccesoDelWebserviceAsync()
        {
            if(idSucursal == null)
            {
                return null;
            }
            string json = await WebServicesDeportnet.ObtenerCofiguracionDeAccesoOffline(idSucursal);
            RespuestaConfigAcceso apiResponse = JsonConvert.DeserializeObject<RespuestaConfigAcceso>(json);

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

           

            return _configAccesoMapper.RespuestaConfigAccesoToConfiguracionDeAcceso(apiResponse);

        }

        private async Task InsertarConfigAccesoTabla(ConfiguracionDeAcceso configuracionDeAcceso)
        {
            using var transaction = await _contextBd.Database.BeginTransactionAsync(); // Iniciar transacción
            try
            {
                //1. Eliminamos datos de tabla ConfigAcceso
                _contextBd.ConfiguracionDeAcceso.RemoveRange(_contextBd.ConfiguracionDeAcceso);


                _contextBd.ConfiguracionDeAcceso.Add(configuracionDeAcceso);

                //Guardamos los cambios
                await _contextBd.SaveChangesAsync();

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

        /*CONFIGURACIONES LOCAL*/

    }
}
