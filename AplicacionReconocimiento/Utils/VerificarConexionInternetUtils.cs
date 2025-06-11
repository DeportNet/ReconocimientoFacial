using DeportNetReconocimiento.Api.Services;
using DeportNetReconocimiento.SDK;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

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
            string[] credenciales = CredencialesUtils.LeerCredenciales();

            if (credenciales == null || credenciales.Length == 0) {
                Log.Error("No hay credenciales de Deportnet en ComprobarConexionInternetConDeportnet, probamos con Ping a Google.");
                return ComprobarConexionInternet();
            }

            //verificamos y asignamos la conexion a internet
            try
            {

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                resultado = await WebServicesDeportnet.TestearConexionDeportnet(credenciales[5], credenciales[4]);

                stopwatch.Stop();

                if (!resultado.Exito)
                {
                    Log.Error(resultado.Mensaje);
                }
                else
                {
                    Log.Information("Tiempo de respuesta de Deportnet: " + stopwatch.ElapsedMilliseconds + " ms");
                    if (stopwatch.ElapsedMilliseconds > 500)
                    {
                        Log.Warning("La velocidad de internet es lenta, intentos: " + intentosVelocidadInternet);
                        intentosVelocidadInternet += 1;
                    }
                    else
                    {
                        Log.Information("La velocidad de internet es aceptable, reiniciamos los intentos");
                        intentosVelocidadInternet = 0;
                    }
                }
            }
            catch (Exception ex)
            {

                Log.Error("Error al validar la conexión a internet");
                return false;
            }

            return resultado.Exito;
        }


        public bool TieneConexionAInternet()
        {
            bool exito = ComprobarConexionInternetConDeportnet().ConfigureAwait(false).GetAwaiter().GetResult();

            return exito;
        }

        //Verificar conexión a internet o en general
        public bool ComprobarConexionInternet()
        {

            //ponemos flag en false como predeterminado
            bool flag = false;

            Ping pingSender = new Ping();
            string direccion = "8.8.8.8"; // IP de Google

            try
            {
                //respuesta que nos da el enviador de ping
                PingReply reply = pingSender.Send(direccion);

                if (reply.Status != IPStatus.Success)
                {
                    Log.Error("No se pudo conectar: " + reply.Status);
                    return flag;
                }

                flag = true;

                //ms
                int velocidadAceptable = 500;
                if (reply.RoundtripTime > velocidadAceptable)
                {
                    Log.Warning($"Velocidad mayor a {velocidadAceptable}, se suma 1 a los intentos de velocidad de internet.");
                    intentosVelocidadInternet += 1;
                }
                else
                {
                    Log.Information($"Velocidad aceptable, reiniciamos los intentos.");
                    intentosVelocidadInternet = 0;
                }

                Log.Information("Tenemos conexion a internet; Tiempo: " + reply.RoundtripTime + " ms.");

            }
            catch (Exception e)
            {
                Log.Error("Error: " + e.Message);
            }

            return flag;
        }


    }
}
