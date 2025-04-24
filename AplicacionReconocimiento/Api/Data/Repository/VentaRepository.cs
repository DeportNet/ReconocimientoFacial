using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Data.Repository
{
    public class VentaRepository
    {


        public VentaRepository( )
        {
        }


        public static async Task<bool> RegistrarVenta(Venta venta)
        {
            try
            {
                using (var context = BdContext.CrearContexto())
                {
                    await context.Ventas.AddAsync(venta);
                    await context.SaveChangesAsync();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
