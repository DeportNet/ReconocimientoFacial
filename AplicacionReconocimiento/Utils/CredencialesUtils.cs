using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Utils
{
    public class CredencialesUtils
    {

        public static void EscribirArchivoCredenciales(string[] arregloDeDatos)
        {
            //guardamos los datos en un archivo binario
            string rutaArchivo = "credenciales.bin";

            using (BinaryWriter writer = new BinaryWriter(File.Open(rutaArchivo, FileMode.Create)))
            {
                foreach (string dato in arregloDeDatos)
                {
                    writer.Write(dato);
                }
            }
        }

        public static bool ExisteArchivoCredenciales()
        {
            bool flag = false;
            if (File.Exists("credenciales.bin"))
            {
                flag = true;
            }
            return flag;
        }

        public static bool ExisteArchivoCredencialesYRegistrarDispositivo()
        {
            bool flag = false;
            if (File.Exists("credenciales.bin"))
            {
                flag = true;
            }
            return flag;
        }


        public static string[] LeerCredenciales()
        {
            //ip , puerto, usuario, contraseña, sucursalId, tokenSucursal

            var listaDatos = new System.Collections.Generic.List<string>();

            WFRgistrarDispositivo wFRgistrarDispositivo = WFRgistrarDispositivo.ObtenerInstancia;


            //si el archivo no existe, se abre la ventana para registrar el dispositivo
            if (!ExisteArchivoCredenciales())
            {

                //si no esta levantado el formulario, se levanta para que haya solo uno
                if (!wFRgistrarDispositivo.Visible)
                {

                    wFRgistrarDispositivo.ShowDialog();
                }

            }
            else
            {
                string rutaArchivo = "credenciales.bin";

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
