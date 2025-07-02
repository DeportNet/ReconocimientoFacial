namespace DeportNetReconocimiento.Api.Services.Interfaces
{
    public interface IFuncionesSincronizacionService
    {
        public Task RecibirTodasLasTablasDx();
        public bool SeSincronizoHoy();
        public void ActualizarFechaSincronizacion();

    }
}
