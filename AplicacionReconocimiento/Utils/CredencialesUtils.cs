using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Utils
{
    public class CredencialesUtils
    {

        private static string rutaArchivo = "credenciales.bin";

        public static void EscribirArchivoCredenciales(string[] arregloDeDatos)
        {
            //guardamos los datos en un archivo binario
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
            if (File.Exists(rutaArchivo))
            {
                flag = true;
            }
            return flag;
        }

        public static bool ExisteArchivoCredencialesYRegistrarDispositivo()
        {
            bool flag = false;
            if (File.Exists(rutaArchivo))
            {
                flag = true;
            }
            return flag;
        }

        public static string? LeerCredencialEspecifica(int unaCredencial)
        {
            
            string[] credenciales = LeerCredenciales();
            
            if (credenciales.Length == 0)
            {
                Console.WriteLine("No se encontraron credenciales");
                return null;
            }

            try
            {
                switch (unaCredencial)
                {
                    case 0:
                        //ip
                        return credenciales[0];
                    case 1:
                        //puerto
                        return credenciales[1];
                    case 2:
                        //usuario
                        return credenciales[2];
                    case 3:
                        //contraseña
                        return credenciales[3];
                    case 4:
                        //sucursalId
                        return credenciales[4];
                    case 5:
                        //tokenSucursal
                        return credenciales[5];
                    default:
                        return null;
                }
            }catch(Exception ex)
            {
                Console.WriteLine("Posicion en el arreglo de credenciales inexistente"+ex.ToString());
            }
            return null;
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
                    wFRgistrarDispositivo.tipoApertura = 0;
                    wFRgistrarDispositivo.ShowDialog();
                }

            }
            else
            {
                try
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
                }catch(Exception ex)
                {
                    Console.WriteLine("No se pudo leer archivo credenciales: "+ ex.Message);
                }
            }

            return listaDatos.ToArray();
        }

    }
}
