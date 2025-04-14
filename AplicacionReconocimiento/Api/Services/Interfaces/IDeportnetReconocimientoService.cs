using DeportNetReconocimiento.Api.Data.Dtos.Request;

namespace DeportNetReconocimiento.Api.Services.Interfaces
{
    public interface IDeportnetReconocimientoService
    {

        public string AltaFacialCliente(AltaFacialClienteRequest clienteRequest);

        public string BajaFacialCliente(BajaFacialClienteRequest clienteRequest);

        //public string BajaMasivaFacialCliente(BajaFacialClienteRequest clienteRequest);
    }
}
