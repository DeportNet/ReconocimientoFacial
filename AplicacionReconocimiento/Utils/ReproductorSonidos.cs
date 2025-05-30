using DeportNetReconocimiento.Utils.Modelo;
using NAudio.Wave;


namespace DeportNetReconocimiento.Utils
{
    public class ReproductorSonidos
    {
        private static IWavePlayer? wavePlayer;
        private AudioFileReader? audioFile;
        private static ReproductorSonidos? instanciaReproductorSonidos;
        
        private ReproductorSonidos()
        {
      
        }

        public static ReproductorSonidos InstanciaReproductorSonidos
        {
            get
            {
                if (instanciaReproductorSonidos == null)
                {
                    instanciaReproductorSonidos = new ReproductorSonidos();
                }
                return instanciaReproductorSonidos;
            }
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

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al reproducir el sonido: {ex.Message}");
            }
        }

        public void DetenerSonido()
        {
            if (wavePlayer != null /*&& wavePlayer.PlaybackState == PlaybackState.Playing*/)
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
