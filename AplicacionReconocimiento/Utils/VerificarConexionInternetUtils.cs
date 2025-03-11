using DeportNetReconocimiento.Api.Services;
using DeportNetReconocimiento.SDK;
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

    
        private static VerificarConexionInternetUtils? instanciaVerificarConexionInternet;
        private int intentosVelocidadInternet;

        private VerificarConexionInternetUtils()
        {
            intentosVelocidadInternet = 0;
        }

        
        public static VerificarConexionInternetUtils InstanciaVerificarConexionInternet
        {
            get
            {
                if (instanciaVerificarConexionInternet == null)
                {
                    instanciaVerificarConexionInternet = new VerificarConexionInternetUtils();
                }
                return instanciaVerificarConexionInternet;
            }
        }

        public int IntentosVelocidadInternet { get; }

        public async Task<bool> ComprobarConexionInternetConDeportnet()
        {
            Hik_Resultado resultado = new Hik_Resultado();
            string[] credenciales = CredencialesUtils.LeerCredenciales();

            if (credenciales.Length == 0)
            {
                return false;
            }

            //verificamos y asignamos la conexion a internet

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            resultado = await WebServicesDeportnet.TestearConexionDeportnet(credenciales[5], credenciales[4]);

            stopwatch.Stop();

            if (!resultado.Exito)
            {
                Console.WriteLine(resultado.Mensaje);
            }
            else
            {
                Console.WriteLine("Tiempo de respuesta de Deportnet: " + stopwatch.ElapsedMilliseconds + " ms");
                if(stopwatch.ElapsedMilliseconds > 300)
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
                //Console.WriteLine("Dirección: " + reply.Address.ToString());


            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            return flag;
        }








    }
}
