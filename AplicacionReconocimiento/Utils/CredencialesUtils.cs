using DeportNetReconocimiento.Api;
using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;


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

            Credenciales? credObtenidas = null;

                credObtenidas = bdContext.Credenciales.FirstOrDefault();
            //try
            //{

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error en LeerCredencialesbd." + ex.Message);
            //}


           
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
                BranchToken = credObtenidas.BranchToken,
                CurrentCompanyMemberId = credObtenidas.CurrentCompanyMemberId
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

            Credenciales? credObtenidas = bdContext.Credenciales.FirstOrDefault();

            if (credObtenidas != null)
            {
                //elimino todas las credenciales anteriores
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

            Credenciales? credenciales = new Credenciales();
            bool flag = false;
            using (var cotexto = BdContext.CrearContexto())
            {
                credenciales = cotexto.Credenciales.FirstOrDefault();
            }

            return credenciales != null ? true : false;

        }

        public static string? LeerCredencialEspecifica(int unaCredencial)
        {

            Credenciales? credenciales = LeerCredencialesBd();


            if (/*!CredecialesCargadasEnBd()*/ credenciales == null)
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
                    case 6:
                        //id empleado actual
                        string credencial = credenciales.CurrentCompanyMemberId;
                        return credencial;
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
