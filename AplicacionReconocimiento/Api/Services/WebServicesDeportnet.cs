using DeportNetReconocimiento.Api.Dtos.Request;
using DeportNetReconocimiento.Api.Dtos.Response;
using DeportNetReconocimiento.GUI;
using DeportNetReconocimiento.SDK;
using DeportNetReconocimiento.Utils;
using System;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace DeportNetReconocimiento.Api.Services
{
    internal class WebServicesDeportnet
    {

        const string urlEntradaCliente = "https://testing.deportnet.com/facialAccess/facialAccessCheckUserEnter";
        const string urlBajaCliente = "https://testing.deportnet.com/facialAccess/facialAccessDeleteResult";
        const string urlAltaCliente = "https://testing.deportnet.com/facialAccess/facialAccessLectureResult";

        public static async Task<string> ControlDeAcceso(string nroTarjeta, string idSucursal) 
        {
            object data = new { };

          
            
                data = new { memberId = nroTarjeta, activeBranchId = idSucursal };

          
            return await FetchInformacion(JsonSerializer.Serialize(data), urlEntradaCliente, HttpMethod.Post);
        }

        
        public static async Task<string> ControlDeAcceso(string nroTarjeta, string idSucursal, string rtaManual)
        {
            
                /*
                { 
	                "activeBranchId": "1",
	                "memberId": "17393",
	                "manualAllowedAccess": "17393", (opcional)
	                "isSuccessful": "T" ( T o F)
                }
                */
            
            object data = new { memberId = nroTarjeta, activeBranchId = idSucursal, manualAllowedAccess = nroTarjeta, isSuccessful = rtaManual};
            
            
            return await FetchInformacion(JsonSerializer.Serialize(data), urlEntradaCliente, HttpMethod.Post);
        }


        public static async Task<string> AltaClienteDeportnet(string json)
        {
            return await FetchInformacion(json, urlAltaCliente, HttpMethod.Post);
        }

        public static async Task<string> BajaClienteDeportnet(string json)
        {
            return await FetchInformacion(json, urlBajaCliente, HttpMethod.Post);
        }

        public static async Task<Hik_Resultado> TestearConexionDeportnet(string tokenSucursal, string idSucursal)
        {
            Hik_Resultado resultado = new Hik_Resultado();

            object data = new { };

            data = new { memberId = 1, activeBranchId = idSucursal };
            
            
            using HttpClient client = new HttpClient();

            // Configurar el header HTTP_X_SIGNATURE con el valor "1234"
            client.DefaultRequestHeaders.Add("X-Signature", tokenSucursal);
            
            //creamos el contenido
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");


            //respuesta fetch, la inicializamos con error
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            try
            {
                response = await client.PostAsync(urlEntradaCliente, content);

                // Verificar el estado de la respuesta
                if (response.IsSuccessStatusCode)
                {
                    resultado.ActualizarResultado(true, "Conexión exitosa", "200");

                    //return true; // Éxito
                }
                else
                {
                    //503 sucursal no existe
                    //309 no tenemos x-signature
                    //505 x-signature erroneo

                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                    string error = await response.Content.ReadAsStringAsync();
                    // Capturar errores HTTP específicos
                    switch (error)
                    {
                        case "503":
                            resultado.ActualizarResultado(false, "La sucursal con el Id: " + idSucursal + " proporcionado no existe.", "503");
                            break;
                        case "309":
                            resultado.ActualizarResultado(false, "No se envio en la cabecera el X-Signature", "309");
                            break;
                        case "505":
                            resultado.ActualizarResultado(false, "El X-Signature: " + tokenSucursal + " proporcionado es erroneo.", "505");
                            break;
                        default:
                            resultado.ActualizarResultado(false, $"Código de estado HTTP {(int)response.StatusCode} - {response.ReasonPhrase}", ((int)response.StatusCode).ToString());
                            break;
                    }

                }
            }
            catch (HttpRequestException e)
            {
                resultado.ActualizarResultado(false, $"Error de conexión: No se pudo conectar al servidor. Detalles: {e.Message}", "500");
                
            }
            catch (Exception e)
            {
                resultado.ActualizarResultado(false, $"Error inesperado: {e.Message}", "500");
            }


            return resultado;
        }

        private static async Task<string> FetchInformacion(string json, string url, HttpMethod metodo)
        {

            using HttpClient client = new HttpClient();

            string token= CredencialesUtils.LeerCredenciales()[5];

            // Configurar el header HTTP_X_SIGNATURE con el valor "1234"
            client.DefaultRequestHeaders.Add("X-Signature", token);

            //creamos el contenido
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //respuesta fetch, la inicializamos con error
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);

            try
            {

                switch (metodo.Method)
                {
                    case "POST":
                        response = await client.PostAsync(url, content);
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync(url);
                        break;
                    case "GET":
                        response = await client.GetAsync(url);
                        break;
                    case "PUT":
                        response = await client.PutAsync(url, content);
                        break;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al hacer fetch de informacion: "+ex.Message);

            }

            //response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }




    }
}
