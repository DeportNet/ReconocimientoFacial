using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Utils
{
    public class CredencialesUtils
    {


        public static string[] LeerCredenciales()
        {
            var listaDatos = new System.Collections.Generic.List<string>();
            string rutaArchivo = "credenciales.bin";


            //si el archivo no existe, se abre la ventana para registrar el dispositivo
            if (!File.Exists(rutaArchivo))
            {
                WFRgistrarDispositivo wFRgistrarDispositivo = new WFRgistrarDispositivo();
                wFRgistrarDispositivo.ShowDialog();
            }
            else
            {

                // Leer desde un archivo binario
                using (BinaryReader reader = new BinaryReader(File.Open(rutaArchivo, FileMode.Open)))
                {

                    while (reader.BaseStream.Position != reader.BaseStream.Length) // Lee hasta el final del archivo
                    {
                        string unDato = reader.ReadString(); // Lee cada string
                        listaDatos.Add(unDato);

                        Console.WriteLine($"Leído: {unDato}");
                    }
                }
            }

            return listaDatos.ToArray();
        }

    }
}
