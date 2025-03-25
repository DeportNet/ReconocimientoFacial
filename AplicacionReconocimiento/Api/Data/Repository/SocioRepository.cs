using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Data.Repository
{
    public class SocioRepository
    {

        private readonly BdContext _contextBd;

        public SocioRepository(BdContext contextDb)
        {
            _contextBd = contextDb;
        }


        public async Task<bool> InsertarUnSocioEnTabla(Socio socio)
        {
            try
            {
                await _contextBd.Socios.AddAsync(socio);
                await _contextBd.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }



    }
}
