using DeportNetReconocimiento.GUI;
using DeportNetReconocimiento.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DeportNetReconocimiento.Utils
{
    public class Escuchador_Directorio
    {

        private static FileSystemWatcher escuchador;

        public static void InicializarEscuchadorEnHilo()
        {
            Task.Run(() => inicializarEscuchador());
        }


        public static void inicializarEscuchador()
        {

            string rutaBase = AppDomain.CurrentDomain.BaseDirectory; // Raíz de ejecutable.
            string rutaAEscuchar = Path.Combine(rutaBase, "Eventos");

            escuchador = new FileSystemWatcher
            {
                Path = rutaAEscuchar,
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName,
                Filter = "*.txt"
            };


            escuchador.Created += OnChanged;
            escuchador.Changed += OnChanged;

            escuchador.EnableRaisingEvents = true;

        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            string nombreArchvio = Path.GetFileName(e.FullPath);
            Console.WriteLine($"Se detectó un cambio: {nombreArchvio}");

            switch (nombreArchvio)
            {
                case "altaCliente.txt":
                    AltaCliente(e.FullPath);
                    break;

                case "bajaCliente.txt":
                    BajaCliente(e.FullPath);
                    break;

               case "bajaMasivaClientes.txt":
                   BajaMasivaCliente(e.FullPath);
                    break;
            }

            WFPrincipal.ObtenerInstancia.VerificarAlmacenamiento();

        }


        private static void AltaCliente(string rutaCompelta)
        {
            Hik_Resultado resultado = new Hik_Resultado();

            try
            {
                bool bloqueado = archvioBloqueado(rutaCompelta);

                if (!bloqueado)
                {


                    // Leer el archivo dentro de un bloque using para garantizar que se cierre
                    string contenido;
                    using (FileStream fs = new FileStream(rutaCompelta, FileMode.Open, FileAccess.Read, FileShare.None))
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        contenido = reader.ReadToEnd();
                    }


                    Console.WriteLine("Contenido: " + contenido);
                string[] partes = contenido.Split(",");
                string id = partes[0].Split(":")[1];
                string nombre = partes[1].Split(":")[1];

                if(nombre.Length > 0 && id.Length > 0)
                {
                     resultado = Hik_Controladora_General.InstanciaControladoraGeneral.AltaCliente(id, nombre);
                }

                if (resultado.Exito)
                {
                    resultado.MessageBoxResultado("Se agregó al cliente con exito");
                }
                else
                {
                    resultado.MessageBoxResultado("Error al procesar el alta del cliente");
                }

                }
                else
                {

                    Console.WriteLine("Está bloqueado");
                }
            }
            catch
            {
                Console.WriteLine("El error es aca");
                Console.WriteLine("Error al procesar el alta del cliente");
            } 


            if (File.Exists(rutaCompelta))
            {
                File.Delete(rutaCompelta);
            }
        }

        private static bool archvioBloqueado(string ruta)
        {

            try
            {
                using(FileStream stream = File.Open(ruta, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    return false;
                }
            }catch(IOException)
            {
                return true;
            }
        }

        private static void BajaCliente(string rutaCompelta)
        {

                Hik_Resultado res = new Hik_Resultado();
            Console.WriteLine(rutaCompelta);
            try
            {
                string id = File.ReadAllText(rutaCompelta);
                if (id.Length > 0)
                {
                    res = Hik_Controladora_General.InstanciaControladoraGeneral.BajaCliente(id);


                    if (res.Exito)
                    {
                        res.MessageBoxResultado("Cliente eliminado con exito");
                    }
                    else
                    {
                        res.MessageBoxResultado("Error al elimianr el cliente");
                    }
                }
            }
            catch
            {
                Console.WriteLine("Error al procesar la baja de clientes");
            }


            if (File.Exists(rutaCompelta))
            {
                File.Delete(rutaCompelta);
            }
        }

        private static void BajaMasivaCliente(string rutaCompelta)
        {
            Hik_Resultado res = new Hik_Resultado();
            try
            {
                string contenido = File.ReadAllText(rutaCompelta);
                string[] ids = contenido.Split(",");
                res = Hik_Controladora_General.InstanciaControladoraGeneral.BajaMasivaClientes(ids);
            }
            catch
            {
                Console.WriteLine("Error al procesar la baja masiva de clientes");
                
            }

            res.EscribirResultado("Baja masiva");

            if (File.Exists(rutaCompelta))
            {
                File.Delete(rutaCompelta);
            }
        }


    }
}
