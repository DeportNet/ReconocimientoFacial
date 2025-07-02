using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.DeportnetApi.Data.Dto.Dx.Socios.NuevosSocios
{
    public class RespuestaAltaNuevosSocios
    {
        public string ProcessResult { get; set; }
        public string? ErrorMessage { get; set; }
        public List<ErrorItem>? ErrorItems { get; set; }
        public List<UpdatedMember>? UpdatedMembers { get; set; }
    }

    public class ErrorItem
    {
        public int Position { get; set; }
        public string Error { get; set; }
    }

    public class UpdatedMember
    {
        public int Position { get; set; }
        public string LocalId { get; set; }
        public int NewId { get; set; }
        public string Email { get; set; } //LocalEmail
        public string? NewEmail { get; set; }

    }


}
