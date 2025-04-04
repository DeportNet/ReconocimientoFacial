using DeportNetReconocimiento.Api;
using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace DeportNetReconocimiento.Utils
{
    public class CredencialesUtils
    {
        
        private static string rutaArchivo = "credenciales.bin";
        private static BdContext? bdContext;

        public static Credenciales? LeerCredencialesBd()
        {
            

            if (!BdContext.BdInicializada())
            {
                Console.WriteLine("Base de datos no inicializada");
                return null;
            }

            
            if (bdContext == null)
            {
                Console.WriteLine("El bd context es null");
                bdContext = BdContext.CrearContexto();
            }

            Credenciales credObtenidas = new Credenciales();
            try
            {
                credObtenidas = bdContext.Credenciales.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se encontró credenciales en CredencialesUtils");
                return null;
            }

            if(credObtenidas == null)
            {
                Console.WriteLine("No se encontró credenciales en CredencialesUtils");
                return null;
            }

            return new Credenciales
            {
          
                Ip = credObtenidas.Ip,
                Port = credObtenidas.Port,
                Username = credObtenidas.Username,
                Password = credObtenidas.Password,
                BranchId = credObtenidas.BranchId,
                BranchToken = credObtenidas.BranchToken
            };
        }

        public static void EscribirCredencialesBd(Credenciales credenciales)
        {
            if (!BdContext.BdInicializada())
            {
                Console.WriteLine("Base de datos no inicializada");
                return;
            }

            
            if (bdContext == null)
            {
                bdContext = BdContext.CrearContexto();
            }

            Credenciales credObtenidas = new Credenciales();
            try
            {
                credObtenidas = bdContext.Credenciales.FirstOrDefault();

                if (credObtenidas.Username != null)
                {
                    //elimino todas las credenciales
                    bdContext.Credenciales.RemoveRange(bdContext.Credenciales);
                    bdContext.SaveChanges(); // Guardar inserción
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("No hay credenciales viejas para sobreescribir");
            }

            bdContext.Credenciales.Add(credenciales);
            bdContext.SaveChanges(); // Guardar inserción

            Console.WriteLine("Credenciales escritas");
        }


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
