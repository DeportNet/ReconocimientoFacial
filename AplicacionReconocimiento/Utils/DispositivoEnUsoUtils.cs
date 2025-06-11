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

        private static readonly string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "enUso.txt");


        //Lee el contenido del archivo 
        private static string Leer()
        {
            if (File.Exists(rutaArchivo))
            {
                return File.ReadAllText(rutaArchivo);
            }

            return string.Empty;
        }

        //Cambia el estado a ocupado
        public static void Ocupar()
        {
            File.WriteAllText(rutaArchivo, "1");
            Log.Information("Ocupo el dispositivo, con un archivo temporal.");
        }

        //cambia el estado a desocupado
        public static void Desocupar()
        {
            File.WriteAllText(rutaArchivo, "0");
            Log.Information("Desocupo el dispositivo, eliminando el archivo temporal.");
        }

        //Devuelve si esta ocupado el dispositivo 
        public static bool EstaOcupado()
        {
            return Leer() == "1" ? true : false; 
        }

    }
}
