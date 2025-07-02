using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Hikvision.SDKHikvision;
using System.Diagnostics;
using System.Net.NetworkInformation;
using DeportNetReconocimiento.Api.Services;
using Serilog;

namespace DeportNetReconocimiento.Utils
{
    public class VerificarConexionInternetUtils
    {

        private static VerificarConexionInternetUtils? instancia;
        private int intentosVelocidadInternet;

        private VerificarConexionInternetUtils()
        {
            intentosVelocidadInternet = 0;
        }


        public static VerificarConexionInternetUtils Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new VerificarConexionInternetUtils();
                }
                return instancia;
            }
        }

        public int IntentosVelocidadInternet { get; }

        public async Task<bool> ComprobarConexionInternetConDeportnet()
        {
            Hik_Resultado resultado = new Hik_Resultado();
            Credenciales credenciales = CredencialesUtils.LeerCredencialesBd();

            if (!CredencialesUtils.CredecialesCargadasEnBd())
            {
                return false;
            }

            //verificamos y asignamos la conexion a internet

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            resultado = await WebServicesDeportnet.TestearConexionDeportnet(credenciales.BranchToken, credenciales.BranchId);

            stopwatch.Stop();

            if (!resultado.Exito)
            {
                Log.Error($"Error al probar la conexión {resultado.Mensaje}");
                Console.WriteLine(resultado.Mensaje);
            }
            else
            {
                Log.Information("Tiempo de respuesta de Deportnet: " + stopwatch.ElapsedMilliseconds + " ms");
                if (stopwatch.ElapsedMilliseconds > 300)
                {
                    intentosVelocidadInternet += 1;
                }
                else
                {
                    intentosVelocidadInternet = 0;
                }

            }
            return resultado.Exito;
        }


        public bool? TieneConexionAInternet()
        {
            try
            {
                bool? exito = ComprobarConexionInternetConDeportnet().ConfigureAwait(false).GetAwaiter().GetResult();
                return exito;
            }
            catch (Exception ex)
            {

                Log.Error("Error al validar la conexión a internet: " + ex.Message);
                return false;
            }
        }

        //Verificar conexión a internet o en general
        public bool ComprobarConexionInternet()
        {
            //ponemos flag en false como predeterminado

            bool flag = false;
            int velocidadAceptable = 500;

            Ping pingSender = new Ping();
            string direccion = "8.8.8.8"; // IP de Google

            try
            {
                //respuesta que nos da el enviador de ping
                PingReply reply = pingSender.Send(direccion);

                if (reply.Status != IPStatus.Success)
                {
                    Log.Error($"No se pudo conectar a internet (Google): {reply.Status}");
                    return flag;
                }
                //hay internet, ahora validamos la velocidad
                flag = true;

                //ms
                if (reply.RoundtripTime > velocidadAceptable)
                {
                    intentosVelocidadInternet += 1;
                    Log.Warning($"Velocidad de internet muy lenta: {reply.RoundtripTime} ms. Velocidad aceptable: {velocidadAceptable}. Sumamos 1 a los intentos.");
                }
                else
                {
                    intentosVelocidadInternet = 0;
                    //Log.Information($"Velocidad de internet aceptable: {reply.RoundtripTime} ms. Velocidad aceptable: {velocidadAceptable}. Reiniciamos los intentos.");
                }
            }
            catch (Exception e)
            {
                Log.Error("Error: " + e.Message);
            }

            return flag;
        }
    }
}
