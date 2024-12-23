using DeportNetReconocimiento.Modelo;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

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
            if (sonido == null || !sonido.Activo || string.IsNullOrEmpty(sonido.RutaArchivo))
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
        //// Método para agregar un nuevo sonido con su estado inicial pasandole la ruta
        //public void AgregarSonido(string nombre, string ruta, bool activo = true)
        //{
        //    if (File.Exists(ruta))
        //    {
        //        Sonidos[nombre] = ruta;
        //        EstadoSonidos[nombre] = activo; // Estado del sonido (activado/desactivado)
        //    }
        //    else
        //    {
        //        MessageBox.Show($"La ruta '{ruta}' no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //// Metodo para agregar un sonido con su estado inicial seleccionando el archivo
        //public void AgregarSonido(string nombre, bool activo = true)
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog
        //    {
        //        Title = "Seleccionar un archivo de sonido",
        //        Filter = "Archivos de sonido|*.mp3;*.wav;*.wma;*.aac;*.flac;*.ogg;*.aiff|Todos los archivos (*.*)|*.*"
        //    };

        //    if (openFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        Sonidos[nombre] = openFileDialog.FileName;
        //        EstadoSonidos[nombre] = activo; // Estado del sonido (activado/desactivado)
        //        MessageBox.Show($"Sonido '{nombre}' agregado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    else
        //    {
        //        Console.WriteLine("No se agrego ningun sonido");
        //    }
        //}

        // Método para cambiar el estado de un sonido
        //public void ActivarDesactivarSonido(string nombre, bool estado)
        //{
        //    if (EstadoSonidos.ContainsKey(nombre))
        //    {
        //        EstadoSonidos[nombre] = estado;
        //    }
        //}

        //// Método para reproducir un sonido si está activado
        //public void ReproducirSonido(string nombreSonido)
        //{
        //    if (!Sonidos.ContainsKey(nombreSonido) || !File.Exists(Sonidos[nombreSonido]))
        //    {
        //        MessageBox.Show($"El sonido '{nombreSonido}' no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    if (!EstadoSonidos[nombreSonido])
        //    {
        //        Console.WriteLine($"El sonido '{nombreSonido}' está desactivado y no se reproducirá.");
        //        return;
        //    }

        //    try
        //    {
        //        DetenerSonido(); // Detener sonido previo si existe

        //        audioFile = new AudioFileReader(Sonidos[nombreSonido]);
        //        wavePlayer = new WaveOutEvent();
        //        wavePlayer.Init(audioFile);
        //        wavePlayer.Play();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error al reproducir el sonido '{nombreSonido}': {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


    }
}
