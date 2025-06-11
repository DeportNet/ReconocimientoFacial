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

        public async Task<bool?> ComprobarConexionInternetConDeportnet()
        {
            Hik_Resultado resultado = new Hik_Resultado();
            string[] credenciales = CredencialesUtils.LeerCredenciales();

            if (credenciales == null || credenciales.Length == 0) {
                return null; // no hay credenciales
            }

            //verificamos y asignamos la conexion a internet

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            resultado = await WebServicesDeportnet.TestearConexionDeportnet(credenciales[5], credenciales[4]);

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

                Log.Error("Error al validar la conexión a internet");
                return false;
            }
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


                    Console.WriteLine("No se pudo conectar: " + reply.Status);
                    return flag;
                }

                flag = true;

                //300ms
                if (reply.RoundtripTime > 300)
                {
                    intentosVelocidadInternet += 1;
                }
                else
                {
                    intentosVelocidadInternet = 0;
                }


                Console.WriteLine("Tenemos conexion a internet; Tiempo: " + reply.RoundtripTime + " ms");


            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            return flag;
        }


    }
}
