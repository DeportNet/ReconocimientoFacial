using DeportNetReconocimiento.Api.Dtos.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeportnetReconocimientoController : ControllerBase
    {
        [HttpPost("alta-facial-cliente")]
        public IActionResult PostFacialCliente(AltaFacialClienteRequest clienteRequest)
        {

            return Ok(new { message = "¡Hola desde el servidor API! Este es tu dto: "+ clienteRequest.ToString() });
        }

    }
}
