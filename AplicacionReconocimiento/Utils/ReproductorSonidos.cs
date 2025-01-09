using DeportNetReconocimiento.Modelo;
using NAudio.Wave;


namespace DeportNetReconocimiento.Utils
{
    public class ReproductorSonidos
    {
        private IWavePlayer wavePlayer;
        private AudioFileReader audioFile;

        // Diccionario con rutas de sonidos
        //public Dictionary<string, string> Sonidos { get; private set; } = new Dictionary<string, string>();

        // Diccionario para el estado de activación de cada sonido
        //public Dictionary<string, bool> EstadoSonidos { get; private set; } = new Dictionary<string, bool>();

        public ReproductorSonidos()
        {
      
        }

        public void ReproducirSonido(Sonido sonido)
        {
            //si el sonido es nulo o no esta activo, no se reproduce
            if (sonido == null || !sonido.Estado || string.IsNullOrEmpty(sonido.RutaArchivo))
            {
                return;
            }
            
            try
            {
                // Detenemos si hay algun sonido reproduciendose actualmente
                DetenerSonido();

                // Cargar el archivo de sonido
                audioFile = new AudioFileReader(sonido.RutaArchivo);

                // Crear el reproductor
                wavePlayer = new WaveOutEvent();

                // Asignar el archivo al reproductor
                wavePlayer.Init(audioFile);

                // Reproducir
                wavePlayer.Play();

                wavePlayer.PlaybackStopped += (sender, args) =>
                {
                    // Liberar recursos cuando termine de reproducir
                    LiberarRecursos();
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al reproducir el sonido: {ex.Message}");
            }
        }

        public void DetenerSonido()
        {
            if (wavePlayer != null && wavePlayer.PlaybackState == PlaybackState.Playing)
            {
                wavePlayer.Stop();
            }

            LiberarRecursos();
        }

        private void LiberarRecursos()
        {
            wavePlayer?.Dispose();
            audioFile?.Dispose();
            wavePlayer = null;
            audioFile = null;
        }

    }
}
