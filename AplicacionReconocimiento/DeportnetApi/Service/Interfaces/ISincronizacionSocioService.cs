namespace DeportNetReconocimiento.Api.Services.Interfaces
{
    public interface ISincronizacionSocioService
    {
        public Task EnviarNuevosSocios();
        public Task SincronizarSocios();

    }
}
