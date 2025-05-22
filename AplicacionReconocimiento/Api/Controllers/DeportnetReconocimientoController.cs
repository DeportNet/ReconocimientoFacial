using DeportNetReconocimiento.Api.Dtos.Request;
using DeportNetReconocimiento.Api.Services.Interfaces;
using DeportNetReconocimiento.Utils;
using Microsoft.AspNetCore.Mvc;

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
            string detalle = "F";

            if (idCliente == null || idSucursal == null || nombreCliente == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede estar vacío.");
            }


            if(!DispositivoEnUsoUtils.EstaOcupado())
            {
                Console.WriteLine("Proceso el evento con id cliente  " + idCliente);
                DispositivoEnUsoUtils.Ocupar();
                detalle = deportnetReconocimientoService.AltaFacialCliente(new AltaFacialClienteRequest(idCliente, idSucursal, nombreCliente));
            }
            else
            {
                Console.WriteLine("No se procesa  el evento con id cliente " + idCliente);
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
