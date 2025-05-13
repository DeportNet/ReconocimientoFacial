
using DeportNetReconocimiento.Api.Data.Dtos.Request;
using DeportNetReconocimiento.Api.Services.Interfaces;
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
            if (idCliente == null || idSucursal == null || nombreCliente == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede estar vacío.");
            }

            string detalle= deportnetReconocimientoService.AltaFacialCliente(new AltaFacialClienteRequest(idCliente,idSucursal,nombreCliente));
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

        //[HttpGet("baja-masiva-facial-cliente")]
        //public IActionResult BajaMasivaFacialCliente(
        //    [FromQuery] string[] arregloIdClientes,
        //    [FromQuery] int idSucursal
        //    )
        //{

        //    if (arregloIdClientes == null || idSucursal == null)
        //    {
        //        return BadRequest("El cuerpo de la solicitud no puede estar vacío.");
        //    }

        //    string detalle = deportnetReconocimientoService.BajaMasivaFacialCliente(new BajaFacialClienteRequest(arregloIdClientes, idSucursal));
        //    return Ok(detalle);
        //}

    }
}
