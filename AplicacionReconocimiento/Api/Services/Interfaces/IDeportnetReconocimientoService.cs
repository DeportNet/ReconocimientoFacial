using DeportNetReconocimiento.Api.Dtos.Request;
using DeportNetReconocimiento.Api.Dtos.Response;
using DeportNetReconocimiento.Api.GlobalExceptionHandler;


namespace DeportNetReconocimiento.Api.Services.Interfaces
{
    public interface IDeportnetReconocimientoService
    {

        public string AltaFacialCliente(AltaFacialClienteRequest clienteRequest);

        public string BajaFacialCliente(BajaFacialClienteRequest clienteRequest);

    }
}
