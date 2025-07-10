using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.Socios;
using DeportNetReconocimiento.Api.Services;
using DeportNetReconocimiento.GUI;
using DeportNetReconocimiento.Hikvision.SDKHikvision;
using Newtonsoft.Json;

namespace DeportNetReconocimiento.Utils
{
    public class VerificarAlmacenamientoUtils
    {
        private static ConfiguracionGeneral configGeneral = ConfiguracionGeneralUtils.ObtenerConfiguracionGeneral();
        public static Hik_Resultado? VerificarHayAlmacenamiento()
        {

            Hik_Resultado resultado = new Hik_Resultado();


            int? capacidadMaximaNullable = null;
            int? carasActualesNullable = null;

            ConfiguracionEstilos configEstilos = ConfiguracionEstilos.LeerJsonConfiguracion();

            if (configGeneral == null)
            {
                Console.WriteLine("No se encontró la configuración general en la base de datos. En VerificarAlmacenamientoUtils.");
                return null;
            }


            capacidadMaximaNullable = configGeneral.CapacidadMaximaRostros;
            carasActualesNullable = configGeneral.RostrosActuales;
            float porcentajeAlerta = configEstilos.PorcentajeAlertaCapacidad;

            if (porcentajeAlerta == 0 || porcentajeAlerta == null)
            {
                Console.WriteLine("El porcentaje de alerta es 0 o null. En VerificarAlmacenamientoUtils.");
            }

            //verificamos los nullable
            if (capacidadMaximaNullable == null)
            {
                Console.WriteLine("La capacidad máxima es null. En VerificarAlmacenamientoUtils.");
                return null;
            }

            if (carasActualesNullable == null)
            {
                Console.WriteLine("Las caras actuales es null. En VerificarAlmacenamientoUtils.");
                return null;
            }

            //calculamos el porcentaje
            float porcentajeActual = (float)((carasActualesNullable * 100) / capacidadMaximaNullable);

            //si hay almacenamiento
            if (porcentajeActual < porcentajeAlerta)
            {
                resultado.ActualizarResultado(true, $"- Capacidad al: {porcentajeActual}%     - Socios: {carasActualesNullable}/{capacidadMaximaNullable}", "Hay almacenamiento");
            }
            else
            {

                if (WFPrincipal.ObtenerInstancia.ConexionInternet)
                {
                    //logica para disminuir el almacenamiento (baja masiva)
                    //Task.Run(() => BajaMasivaClientes());
                }
                else
                {
                    Console.WriteLine("No puedo hacer peticion de baja masiva debido a que no hay internet");
                }

                //si no hay almacenamiento
                resultado.ActualizarResultado(false, $"- Capacidad al: {porcentajeActual}%     - Socios: {carasActualesNullable}/{capacidadMaximaNullable}", "No hay almacenamiento");
            }

            return resultado;
        }

        private static async Task BajaMasivaClientes()
        {
            if (configGeneral == null)
            {
                Console.WriteLine("No se encontró la configuración general en la base de datos. En VerificarAlmacenamientoUtils.");
                return;
            }

            Credenciales? credenciales = CredencialesUtils.LeerCredencialesBd();



            string? idSucursal = credenciales.BranchId;


            if (idSucursal == null)
            {
                Console.WriteLine("No se encontró idSucursal en la base de datos. En VerificarAlmacenamientoUtils.");
                return;
            }




            //hago la pegada
            string json = await WebServicesDeportnet.BajaFacialMasivaClienteDeportnet(idSucursal, configGeneral.LectorActual);

            //recibo el arreglo de ids a borrar
            ListadoBajaSociosDtoDx? listado = JsonConvert.DeserializeObject<ListadoBajaSociosDtoDx>(json);

            if (listado == null)
            {
                Console.WriteLine("Listado de socios a eliminar es null. En VerificarAlmacenamientoUtils.");
                return;
            }

            //funcion de hikControladoraGeneral que borra en bucle

            Hik_Controladora_General.Instancia.BajaMasivaClientes(listado.DeletedBranchMembers);


        }



    }
}
