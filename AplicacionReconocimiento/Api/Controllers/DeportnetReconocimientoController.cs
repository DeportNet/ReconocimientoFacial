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
        public IActionResult AltaFacialCliente(
            [FromQuery] int idCliente,
            [FromQuery] int idSucursal,
            [FromQuery] string nombreCliente
            )
        {
            //VALIDAR TOKEN
            //[FromHeader(Name = "HTTP_X_SIGNATURE")] string signature
            // if (signature != "1234")
            //{
            //      return Unauthorized(new { Mensaje = "Token inválido" });
            //}

            if (idCliente == null || idSucursal == null || nombreCliente == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede estar vacío.");
            }

            string detalle= deportnetReconocimientoService.AltaFacialCliente(new AltaFacialClienteRequest(idCliente,idSucursal,nombreCliente));
            return Ok(detalle);
        }

        [HttpDelete("baja-facial-cliente")]
        public IActionResult BajaFacialCliente(
            [FromQuery] int idCliente,
            [FromQuery] int idSucursal
            )
        {
            if (idCliente == null || idSucursal == null || nombreCliente == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede estar vacío.");
            }

            string detalle = deportnetReconocimientoService.BajaFacialCliente(new BajaFacialClienteRequest(idCliente, idSucursal));
            return Ok(detalle);
        }

    }
}
