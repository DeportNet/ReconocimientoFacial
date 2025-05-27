using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Utils
{
    public class DispositivoEnUsoUtils
    {
        // Semáforo estático compartido
        private static readonly SemaphoreSlim _semaforo = new SemaphoreSlim(1, 1);

        // Intenta ocupar el dispositivo sin esperar. Devuelve true si pudo.
        public static bool Ocupar()
        {
            Console.WriteLine("- - - - - Ocupo dispositivo - - - - - ");
            return _semaforo.Wait(0); // No bloquea: si no puede entrar, devuelve false
        }

        // Libera el dispositivo. Sólo debe llamarse si se sabe que está ocupado.
        public static void Liberar()
        {
            // Validación opcional: solo liberar si hay uno ocupado
            if (_semaforo.CurrentCount == 0)
            {
                Console.WriteLine("- - - - - Desocupo dispositivo - - - - - ");
                _semaforo.Release();
            }
        }

        // Permite consultar si el dispositivo está libre
        public static bool EstaLibre()
        {
            return _semaforo.CurrentCount > 0;
        }
    }
}
