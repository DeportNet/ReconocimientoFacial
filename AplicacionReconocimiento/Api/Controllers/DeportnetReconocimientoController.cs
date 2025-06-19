using DeportNetReconocimiento.Api.Dtos.Request;
using DeportNetReconocimiento.Api.Dtos.Response;
using DeportNetReconocimiento.Api.Services;
using DeportNetReconocimiento.Api.Services.Interfaces;
using DeportNetReconocimiento.Utils;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
            if (idCliente == null || idSucursal == null || nombreCliente == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede estar vacío.");
            }
            string detalle = "F";

            if(DispositivoEnUsoUtils.EstaLibre())
            {
                Log.Information("Proceso la peticion de alta con id cliente  " + idCliente + ".");
                DispositivoEnUsoUtils.Ocupar();
                detalle = deportnetReconocimientoService.AltaFacialCliente(new AltaFacialClienteRequest(idCliente, idSucursal, nombreCliente));
            }
            else
            {
                RespuestaAltaBajaCliente respuestaAlta = new RespuestaAltaBajaCliente(
                    idSucursal: idSucursal.ToString(),
                    idCliente: idCliente.ToString(),
                    mensaje: "El dispositivo se encuentra ocupado",
                    exito: "F");

                _ = WebServicesDeportnet.AltaClienteDeportnet(respuestaAlta.ToJson());

                Log.Information($"No se procesa la peticion de alta con id cliente {idCliente} debido a que el dispositivo esta ocupado.");
            }

            return Ok(detalle);
        }

        [HttpGet("baja-facial-cliente")]
        public IActionResult BajaFacialCliente(
            [FromQuery] int idCliente,
            [FromQuery] int idSucursal
            )
        {

          

            if (idCliente == null || idSucursal == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede estar vacío.");
            }

            string detalle = deportnetReconocimientoService.BajaFacialCliente(new BajaFacialClienteRequest(idCliente, idSucursal));
            return Ok(detalle);
        }

    }
}
