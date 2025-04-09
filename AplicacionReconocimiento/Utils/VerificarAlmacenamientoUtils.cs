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
    public class VerificarAlmacenamientoUtils
    {
        private static BdContext context = BdContext.CrearContexto();
        
        public static Hik_Resultado? VerificarHayAlmacenamiento()
        {

            Hik_Resultado resultado = new Hik_Resultado();


            int? capacidadMaximaNullable = null;
            int? carasActualesNullable = null;

            ConfiguracionGeneral? configGeneral = context.ConfiguracionGeneral.FirstOrDefault(cg => cg.Id == 1);
            ConfiguracionEstilos configEstilos = ConfiguracionEstilos.LeerJsonConfiguracion();
            
            if(configGeneral == null)
            {
                Console.WriteLine("No se encontró la configuración general en la base de datos. En VerificarAlmacenamientoUtils.");
                return null;
            }


            capacidadMaximaNullable = configGeneral.CapacidadMaximaRostros;
            carasActualesNullable = configGeneral.RostrosActuales;
            float porcentajeAlerta = configEstilos.PorcentajeAlertaCapacidad;

            //verificamos los nullable
            if(capacidadMaximaNullable == null)
            {
                Console.WriteLine("La capacidad máxima es null. En VerificarAlmacenamientoUtils.");
                return null;
            }

            if(carasActualesNullable == null)
            {
                Console.WriteLine("Las caras actuales es null. En VerificarAlmacenamientoUtils.");
                return null;
            }
            
            //calculamos el porcentaje
            float porcentajeActual = (float)((carasActualesNullable * 100) / capacidadMaximaNullable);

            //si hay almacenamiento
            if (porcentajeActual < porcentajeAlerta)
            {
                resultado.ActualizarResultado(true, $"- Capacidad al: {porcentajeActual}% - Socios: {carasActualesNullable}/{capacidadMaximaNullable}", "Hay almacenamiento");
            }
            else
            {
                //si no hay almacenamiento

                resultado.ActualizarResultado(false, $"- Capacidad al: {porcentajeActual}% - Socios: {carasActualesNullable}/{capacidadMaximaNullable}", "No hay almacenamiento");
            }

            return resultado;
        }





    }
}
