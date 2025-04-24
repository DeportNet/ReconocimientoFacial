using DeportnetOffline.Data.Dto.Table;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Utils
{
    public class PaginadorUtils
    {

        public static async Task<PaginadoResultado<T>> ObtenerPaginadoAsync<T>(
              IQueryable<T> query,
              int pagina,
              int cantidadPorPagina
            ) where T : class
        {
            int totalRegistros = await query.CountAsync();
            int totalPaginas = (int)Math.Ceiling(totalRegistros / (double)cantidadPorPagina);

          



            List<T> items = await query
                .Skip((pagina - 1) * cantidadPorPagina)
                .Take(cantidadPorPagina)
                .ToListAsync();

            return new PaginadoResultado<T>
            {
                Items = items,
                TotalRegistros = totalRegistros,
                PaginaActual = pagina,
                TotalPaginas = totalPaginas
            };
        }

    }
}
