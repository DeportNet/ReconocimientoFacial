using Serilog;
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
            bool resultado = true;
            try
            {
                Console.WriteLine("- - - - - Ocupo dispositivo - - - - - ");
                resultado = _semaforo.Wait(0); // No bloquea: si no puede entrar, devuelve false
                Log.Information($"Intento ocupar el dispositivo. Exito: {resultado}");

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





        //private static readonly string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "enUso.txt");
        //Lee el contenido del archivo 
        //private static string Leer()
        //{
        //    if (File.Exists(rutaArchivo))
        //    {
        //        return File.ReadAllText(rutaArchivo);
        //    }
        //    return string.Empty;
        //}
        //Cambia el estado a ocupado
        //public static void Ocupar()
        //{
        //    File.WriteAllText(rutaArchivo, "1");
        //    Log.Information("Ocupo el dispositivo, con un archivo temporal.");
        //}
        //cambia el estado a desocupado
        //public static void Desocupar()
        //{
        //    File.WriteAllText(rutaArchivo, "0");
        //    Log.Information("Desocupo el dispositivo, eliminando el archivo temporal.");
        //}
        //Devuelve si esta ocupado el dispositivo 
        //public static bool EstaOcupado()
        //{
        //    return Leer() == "1" ? true : false;
        //}
    }
}
