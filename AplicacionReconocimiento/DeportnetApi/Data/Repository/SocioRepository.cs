using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;

namespace DeportNetReconocimiento.Api.Data.Repository
{
    public class SocioRepository
    {


        public SocioRepository( )
        {
        }


        public static async Task<bool> InsertarUnSocioEnTabla(Socio socio)
        {
            using var context = BdContext.CrearContexto();
            try
            {
                await context.Socios.AddAsync(socio);
                await context.SaveChangesAsync();
                
                return true;
            }
            catch
            {
                return false;
            }
        }



    }
}
