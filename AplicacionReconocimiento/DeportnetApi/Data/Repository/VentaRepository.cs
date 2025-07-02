using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace DeportNetReconocimiento.Api.Data.Repository
{
    public class VentaRepository
    {


        public VentaRepository( )
        {
        }


        public static async Task<bool> RegistrarVenta(Venta venta)
        {
            using var context = BdContext.CrearContexto();
            try
            {
                await context.Ventas.AddAsync(venta);
                await context.SaveChangesAsync();
                
                return true;
            }
            catch(DbUpdateException ex)
            {
                Console.Write(ex.InnerException );
                return false;
            }
        }

    }
}
