using DeportNetReconocimiento.Api.Dtos.Request;
using DeportNetReconocimiento.Api.Dtos.Response;
using DeportNetReconocimiento.Api.GlobalExceptionHandler;
using DeportNetReconocimiento.Api.Services.Interfaces;
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

        private readonly IDeportnetReconocimientoService deportnetReconocimientoService;

        public DeportnetReconocimientoController(IDeportnetReconocimientoService deportnetReconocimientoService)
        {
            this.deportnetReconocimientoService = deportnetReconocimientoService;
        }

        [HttpGet("alta-facial-cliente")]
        public IActionResult PostFacialCliente(
            [FromQuery] int idCliente,
            [FromQuery] int idGimnasio,
            [FromQuery] string nombreCliente
            )
        {

            if (idCliente == null || idGimnasio == null || nombreCliente == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede estar vacío.");
            }

            DetallesResponse detalle= deportnetReconocimientoService.AltaFacialCliente(new AltaFacialClienteRequest(idCliente,idGimnasio,nombreCliente));
            return Ok(detalle);
        }

        [HttpDelete("baja-facial-cliente")]
        public IActionResult DeleteFacialCliente(BajaFacialClienteRequest clienteRequest)
        {

            return Ok(new { message = "¡Hola desde el servidor API! Este es tu dto: " + clienteRequest.ToString() });
        }

    }
}
