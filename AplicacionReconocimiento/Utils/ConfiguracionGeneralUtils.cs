using DeportNetReconocimiento.Api.BD;
using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Utils
{
    public class ConfiguracionGeneralUtils
    {
        private static BdContext _bdContext = BdContext.CrearContexto();

        private static ConfiguracionGeneral CrearRegistroConfiguracionGeneral()
        {
            ConfiguracionGeneral config = new ConfiguracionGeneral(
                200,
                "!MiClaveSegura123!",
                "",
                null,
                null,
                Hik_Controladora_General.InstanciaControladoraGeneral.ObtenerCapacidadCarasDispositivo(),
                1,
                null
                );

            _bdContext.Add(config);
            _bdContext.SaveChanges();

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
            if (_bdContext == null)
            {
                _bdContext = BdContext.CrearContexto();
            }

            ConfiguracionGeneral? config = _bdContext.ConfiguracionGeneral.FirstOrDefault(c => c.Id == 1);

            if(config == null)
            {
                config = CrearRegistroConfiguracionGeneral();
            }


            return config;
        }


        public static int SumarRegistroCara()
        {
            int? rostrosActuales = null;
            ConfiguracionGeneral config = ObtenerConfiguracionGeneral();

            config.RostrosActuales += 1;

            rostrosActuales = config.RostrosActuales;

            _bdContext.SaveChanges();
            return (int)rostrosActuales;
        }

        public static int RestarRegistroCara()
        {
            int? rostrosActuales = null;

            ConfiguracionGeneral config = ObtenerConfiguracionGeneral();

            config.RostrosActuales -= 1;
            rostrosActuales = config.RostrosActuales;


            _bdContext.SaveChanges();

            return (int)rostrosActuales;
        }

        public static string ObtenerLectorActual()
        {
            string? lectorActual = ObtenerConfiguracionGeneral().LectorActual;

            if (lectorActual == null) {
                Console.WriteLine("nro lector es null");
                lectorActual = "1";
            }

            return lectorActual.ToString();
        }

        public static void ActualizarLectorActual(string? lectorNuevo)
        {
            ConfiguracionGeneral config = ObtenerConfiguracionGeneral();
            if(config == null)
            {
                Console.WriteLine("Configuracion General es null, en ActualizarLectorFacial");
                return;
            }
            if (lectorNuevo == null)
            {
                Console.WriteLine("Lector nuevo es null, en ActualizarLectorFacial");
                return;
            }
            config.LectorActual = lectorNuevo;
            _bdContext.SaveChanges();
        }

    }
}
