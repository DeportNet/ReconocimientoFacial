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
            credObtenidas = bdContext.Credenciales.FirstOrDefault();


            if (credObtenidas == null)
            {
                WFRgistrarDispositivo wFRgistrarDispositivo = WFRgistrarDispositivo.ObtenerInstancia;

                if (!wFRgistrarDispositivo.Visible)
                {
                    wFRgistrarDispositivo.tipoApertura = 0;
                    wFRgistrarDispositivo.ShowDialog();
                }
                credObtenidas = WFRgistrarDispositivo.ObtenerInstancia.credenciales;
                Console.WriteLine(credObtenidas.Ip);
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

            credObtenidas = bdContext.Credenciales.FirstOrDefault();

            if (credObtenidas != null)
            {
                //elimino todas las credenciales
                bdContext.Credenciales.RemoveRange(bdContext.Credenciales);
                bdContext.SaveChanges(); // Guardar inserción
            }
            else
            {
                Console.WriteLine("No hay credenciales viejas para sobreescribir");

            }


            bdContext.Credenciales.Add(credenciales);
            bdContext.SaveChanges(); // Guardar inserción
            Console.WriteLine("Credenciales escritas");

        }

        public static bool CredecialesCargadasEnBd()
        {

            Credenciales credenciales = new Credenciales();
            bool flag = false;
            using (var cotexto = BdContext.CrearContexto())
            {
                credenciales = cotexto.Credenciales.FirstOrDefault();
            }

            return credenciales != null ? true : false;

        }

        public static string? LeerCredencialEspecifica(int unaCredencial)
        {

            Credenciales credenciales = LeerCredencialesBd();

            if (!CredecialesCargadasEnBd())
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
                        return credenciales.Ip;
                    case 1:
                        //puerto
                        return credenciales.Port;
                    case 2:
                        //usuario
                        return credenciales.Username;
                    case 3:
                        //contraseña
                        return credenciales.Password;
                    case 4:
                        //sucursalId
                        return credenciales.BranchId;
                    case 5:
                        //tokenSucursal
                        return credenciales.BranchToken;
                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Posicion en el arreglo de credenciales inexistente" + ex.ToString());
            }
            return null;
        }
    }
}
