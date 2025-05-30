using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportnetOffline.Data.Dto.Table
{
    public class PaginadoResultado<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalRegistros { get; set; }
        public int TotalPaginas { get; set; }
        public int PaginaActual { get; set; }

        public PaginadoResultado() { }

        public PaginadoResultado(List<T> items, int totalRegistros, int paginaActual, int totalPaginas)
        {
            Items = items;
            TotalRegistros = totalRegistros;
            PaginaActual = paginaActual;
            TotalPaginas = totalPaginas;
        }

    }
}
