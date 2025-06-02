using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.Concepts;
using DeportNetReconocimiento.Api.Services.Interfaces;
using DeportNetReconocimiento.Utils;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DeportNetReconocimiento.Api.Services
{
    public class ConceptService : ISincronizacionConceptsService
    {

        private readonly BdContext _bdContext;
        private string? idSucursal;

        public ConceptService(BdContext bdContext)
        {
            _bdContext = bdContext;
            idSucursal = CredencialesUtils.LeerCredencialEspecifica(4);
        }


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
            Console.WriteLine("\n\n\n\n\nCantidad de membresias = " + listadoDeConceptsDx.Concepts.Count + "\n\n\n\n\n\n");


            //recorremos la lista de concepts, y separamos las listas de membresias y articulos

            listadoDeConceptsDx.Concepts.ForEach(c =>
            {

                switch (c.IsSaleItem.ToLower())
                {
                    case "f":
                        membresias.Add(new Membresia
                        {
                            IdDx = c.Id,
                            Name = c.Name,
                            Amount = c.Amount,
                            IsSaleItem = c.IsSaleItem[0],
                            Period = c.Period,
                            Days = c.Days,
                        });
                        break;
                    case "t":
                        articulos.Add(new Articulo
                        {
                            IdDx = c.Id,
                            Name = c.Name,
                            Amount = c.Amount,
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
            using var transaction = await _bdContext.Database.BeginTransactionAsync(); // Iniciar transacción
            try
            {
                await VerificarCambiosEnTablaArticulos(listadoArticulosDx);

                //Guardamos los cambios
                await _bdContext.SaveChangesAsync();

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
            List<Articulo> listadoArticulosLocal = await _bdContext.Articulos.ToListAsync();

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
                _bdContext.Articulos.RemoveRange(articulosEliminados);
            }
            if (nuevosArticulos.Count > 0)
            {
                await _bdContext.Articulos.AddRangeAsync(nuevosArticulos);
            }
            if (articulosActualizados.Count > 0)
            {
                foreach (var articulo in articulosActualizados)
                {
                    var articuloLocal = listadoArticulosLocal.First(al => al.IdDx == articulo.IdDx);
                    _bdContext.Entry(articuloLocal).CurrentValues.SetValues(articulo);
                }
            }
        }
        private async Task InsertarMembresiasEnTabla(List<Membresia> listadoMembresias)
        {
            using var transaction = await _bdContext.Database.BeginTransactionAsync(); // Iniciar transacción
            try
            {   
                await VerificarCambiosEnTablaMembresias(listadoMembresias);

                //Guardamos los cambios
                await _bdContext.SaveChangesAsync();

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
            List<Membresia> listadoMembresiasLocal = await _bdContext.Membresias.ToListAsync();

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
                _bdContext.Membresias.RemoveRange(membresiasEliminadas);
            }
            if (nuevasMembresias.Count > 0)
            {
                await _bdContext.Membresias.AddRangeAsync(nuevasMembresias);
            }
            if (membresiasActualizadas.Count > 0)
            {
                foreach (var unaMActualizada in membresiasActualizadas)
                {
                    var membresiaLocal = listadoMembresiasLocal.First(l => l.IdDx == unaMActualizada.IdDx);
                    _bdContext.Entry(membresiaLocal).CurrentValues.SetValues(unaMActualizada);
                }
            }
        }

    }
}
