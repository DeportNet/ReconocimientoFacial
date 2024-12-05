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
            string rutaAEscuchar = @"D:\DeportNet\DeportNetReconocimiento\AplicacionReconocimiento\Eventos\"; //Aca hay que poner la ruta correspondiente 

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

        }


        private static void AltaCliente(string rutaCompelta)
        {

            try
            {
                string contenido = File.ReadAllText(rutaCompelta);
                string[] partes = contenido.Split(",");
                string id = partes[0].Split(":")[1];
                string nombre = partes[1].Split(":")[1];

                if(nombre.Length > 0 && id.Length > 0)
                {
                        Hik_Controladora_General.InstanciaControladoraGeneral.AltaCliente(id, nombre);
                }


                if (File.Exists(rutaCompelta))
                { 
                    File.Delete(rutaCompelta);
                }

            }
            catch
            {
                Console.WriteLine("Error al procesar el alta del cliente");
            }
        }

        private static void BajaCliente(string rutaCompelta)
        {
            Console.WriteLine(rutaCompelta);
            try
            {
                string id = File.ReadAllText(rutaCompelta);
                Console.WriteLine("Esto es el id leido desde el documento" + id);

                if (id.Length > 0)
                {
                    Hik_Controladora_General.InstanciaControladoraGeneral.BajaCliente(id);
                }
            }
            catch
            {
                Console.WriteLine("Error al procesar la baja del cliente");
            }


            if (File.Exists(rutaCompelta))
            {
                File.Delete(rutaCompelta);
            }
        }

        private static void BajaMasivaCliente(string rutaCompelta)
        {
            try
            {
                string contenido = File.ReadAllText(rutaCompelta);
                string[] ids = contenido.Split(",");
                Hik_Controladora_General.InstanciaControladoraGeneral.BajaMasivaClientes(ids);
            }
            catch
            {
                Console.WriteLine("Error al procesar la baja masiva de clientes");
            }


            if (File.Exists(rutaCompelta))
            {
                File.Delete(rutaCompelta);
            }
        }


    }
}
