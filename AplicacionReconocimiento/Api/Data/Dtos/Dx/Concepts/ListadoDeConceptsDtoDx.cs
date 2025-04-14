using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Data.Dtos.Dx.Concepts
{
    public class ListadoDeConceptsDtoDx
    {
        public List<ConceptDtoDx>? Concepts { get; set; }
        public string? Result { get; set; }
        public string? ErrorMessage { get; set; }
        public int? ConceptsCount { get; set; }

        public ListadoDeConceptsDtoDx() { }
    }
}
