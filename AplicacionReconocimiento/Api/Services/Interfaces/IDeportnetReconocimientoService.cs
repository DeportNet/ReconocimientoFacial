using DeportNetReconocimiento.Api.Dtos.Request;
using DeportNetReconocimiento.Api.Dtos.Response;
using DeportNetReconocimiento.Api.GlobalExceptionHandler;


namespace DeportNetReconocimiento.Api.Services.Interfaces
{
    public interface IDeportnetReconocimientoService
    {

        public DetallesResponse AltaFacialCliente(AltaFacialClienteRequest clienteRequest);

        public DetallesResponse BajaFacialCliente(BajaFacialClienteRequest clienteRequest);

    }
}
