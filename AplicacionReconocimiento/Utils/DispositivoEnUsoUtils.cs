using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Search;

namespace DeportNetReconocimiento.Utils
{
    public class DispositivoEnUsoUtils
    {
        // Semáforo estático compartido
        private static readonly SemaphoreSlim _semaforo = new SemaphoreSlim(1, 1);

        // Intenta ocupar el dispositivo sin esperar. Devuelve true si pudo.
        public static bool Ocupar(string queOcupo)
        {
            bool resultado = true;
            try
            {
                Console.WriteLine("- - - - - Ocupo dispositivo - - - - - ");
                resultado = _semaforo.Wait(0); // No bloquea: si no puede entrar, devuelve false
                Log.Information($"Intento ocupar el dispositivo para {queOcupo}. Exito: {resultado}");

            }
            catch (Exception ex) {
                Log.Error("Error al intentar ocupar el dispositivo: {Message}", ex.Message);
            }

            return resultado;
        }

        // Libera el dispositivo. Sólo debe llamarse si se sabe que está ocupado.
        public static void Desocupar()
        {

            try
            {
                // Validación opcional: solo liberar si hay uno ocupado
                if (_semaforo.CurrentCount == 0)
                {
                    Console.WriteLine("- - - - - Desocupo dispositivo - - - - - ");
                    _semaforo.Release();
                }

            }
            catch (Exception ex) {
                Log.Error("Error al intentar desocupar el dispositivo: {Message}", ex.Message);

            }

        }

        // Permite consultar si el dispositivo está libre
        public static bool EstaLibre()
        {
            return _semaforo.CurrentCount > 0;
        }
    }
}
