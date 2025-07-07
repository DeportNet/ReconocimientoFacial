using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Hikvision.SDKHikvision;
using Serilog;

namespace DeportNetReconocimiento.Utils
{
    public class ConfiguracionGeneralUtils
    {

        private static ConfiguracionGeneral CrearRegistroConfiguracionGeneral()
        {
            using var bdContext = BdContext.CrearContexto();


            ConfiguracionGeneral config = new ConfiguracionGeneral(
                200,
                "!MiClaveSegura123!",
                "",
                null,
                null,
                Hik_Controladora_General.Instancia.ObtenerCapacidadCarasDispositivo(),
                1,
                "1",
                false
                );

            bdContext.Add(config);
            bdContext.SaveChanges();

            return config;
        }


        public static int? ObtenerCantMaxCarasBd()
        {

            ConfiguracionGeneral? config = ObtenerConfiguracionGeneral();

            //Si no hay configuracion, no se puede obtener la capacidad maxima de caras desde la BD
            if (config == null)
            {
                Console.WriteLine("Se creo un registro de ConfiguracionGeneral en ObtenerCantMaxCarasBd");
            }

            return config.CapacidadMaximaRostros;

        }

        public static ConfiguracionGeneral ObtenerConfiguracionGeneral()
        {
            using var bdContext = BdContext.CrearContexto();


            ConfiguracionGeneral? config = bdContext.ConfiguracionGeneral.FirstOrDefault(c => c.Id == 1);

            if (config == null)
            {
                config = CrearRegistroConfiguracionGeneral();
            }


            return config;
        }


        public static int SumarRegistroCara()
        {
            using var bdContext = BdContext.CrearContexto();

            int? rostrosActuales = null;
            ConfiguracionGeneral config = ObtenerConfiguracionGeneral();

            config.RostrosActuales += 1;

            rostrosActuales = config.RostrosActuales;

            bdContext.SaveChanges();
            return (int)rostrosActuales;
        }

        public static int RestarRegistroCara()
        {
            using var bdContext = BdContext.CrearContexto();

            int? rostrosActuales = null;

            ConfiguracionGeneral config = ObtenerConfiguracionGeneral();

            config.RostrosActuales -= 1;
            rostrosActuales = config.RostrosActuales;


            bdContext.SaveChanges();

            return (int)rostrosActuales;
        }

        public static string ObtenerLectorActual()
        {
            string? lectorActual = ObtenerConfiguracionGeneral().LectorActual;

            if (lectorActual == null)
            {
                Console.WriteLine("nro lector es null");
                lectorActual = "1";
            }

            return lectorActual.ToString();
        }

        public static void ActualizarLectorActual(string? lectorNuevo)
        {
            using var bdContext = BdContext.CrearContexto();

            ConfiguracionGeneral? config = bdContext.ConfiguracionGeneral.FirstOrDefault(c => c.Id == 1);

            if (config == null)
            {
                Console.WriteLine("Configuracion General es null, en ActualizarLectorFacial");
                config = CrearRegistroConfiguracionGeneral();
            }

            if (lectorNuevo == null)
            {
                Log.Error("Lector nuevo es null, en ActualizarLectorFacial");
                return;
            }

            config.LectorActual = lectorNuevo;
            bdContext.SaveChanges();
        }


        //Cambia el estado de modulo offline activo
        public static void CambiarEstadoModuloActivo()
        {
            using var bdContext = BdContext.CrearContexto();
            ConfiguracionGeneral config = bdContext.ConfiguracionGeneral.FirstOrDefault();
            if (config == null)
            {
                Console.Error.WriteLine("Configuración general es null");
                return;
            }

            config.IsOffline = !config.IsOffline;
            bdContext.SaveChanges();
        }

        public static bool ModuloOfflineActivo()
        {
            using var bdContext = BdContext.CrearContexto();
            ConfiguracionGeneral config = ConfiguracionGeneralUtils.ObtenerConfiguracionGeneral();
            return config.IsOffline;

        }
    }
}
