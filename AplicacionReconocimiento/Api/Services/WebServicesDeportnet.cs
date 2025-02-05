using DeportNetReconocimiento.Api.Dtos.Request;
using DeportNetReconocimiento.Api.Dtos.Response;
using DeportNetReconocimiento.GUI;
using DeportNetReconocimiento.SDK;
using DeportNetReconocimiento.Utils;
using NAudio.Gui;
using NAudio.Wave;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.DataFormats;


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

            object dataEnviar = new { };

            dataEnviar = new { memberId = 1, activeBranchId = idSucursal };
            
            
            using HttpClient client = new HttpClient();

            // Configurar el header HTTP_X_SIGNATURE con el valor "1234"
            client.DefaultRequestHeaders.Add("X-Signature", tokenSucursal);
            
            //creamos el contenido
            var content = new StringContent(JsonSerializer.Serialize(dataEnviar), Encoding.UTF8, "application/json");


            //respuesta fetch, la inicializamos con error
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            try
            {
                //309 = No se envió HTTP_X_SIGNATURE
                //308 = El HTTP_X_SIGNATURE es nulo o vacío
                //400 = No se pudo procesar el request(por no venir en formato json por ejemplo)
                //401 = No se envió el Id de sucursal
                //402 = No se envió el Id de socio
                //403 = El Id de sucursal es nulo o vacío
                //404 = El Id de socio es nulo o vacío
                //501 = El módulo de acceso facial no existe o ha sido inactivado en DeportNet(es un campo en una tabla)
                //503 = La sucursal no tiene asignada el módulo de acceso facial
                //504 = La sucursal no tiene configurado el token
                //507 = Token inválido(no coinciden el que está configurado con el que se envía)
                //505 = La sucursal no tiene la configuración de acceso facial

                response = await client.PostAsync(urlEntradaCliente, content);

                string dataRecibida = await response.Content.ReadAsStringAsync();
                
                using JsonDocument doc = JsonDocument.Parse(dataRecibida);
                
                JsonElement root = doc.RootElement;

                //Busco la propiedad branchAcces y digo que el elemento  es de tipo arreglo
                if (root.TryGetProperty("branchAccess", out JsonElement branchAccess) && branchAccess.ValueKind == JsonValueKind.Array)
                {
                    //en realidad arroja "No se encontro el Socio" pero es justamente lo que necesitamos para saber si la conexión fue exitosa
                    resultado.ActualizarResultado(true, "Conexión exitosa.", "200");
                }
                else
                {
                    // Capturar errores HTTP específicos
                    switch (dataRecibida)
                    {
                        case "200":
                            resultado.ActualizarResultado(true, "Conexión exitosa.", "200");
                            break;
                        case "309":
                            resultado.ActualizarResultado(false, "No se envio en la cabecera el X-Signature.", "309");
                            break;
                        case "400":
                            resultado.ActualizarResultado(false, "No se pudo procesar el request(por no venir en formato json por ejemplo).", "400");
                            break;
                        case "403":
                            resultado.ActualizarResultado(false, "El Id de sucursal es nulo o vacío.", "403");
                            break;
                        case "503":
                            resultado.ActualizarResultado(false, "La sucursal con el Id: " + idSucursal + " no tiene asignada el módulo de acceso facial.", "503");
                            break;
                        case "504":
                            resultado.ActualizarResultado(false, "La sucursal con el Id: " + idSucursal + " no tiene configurado el token.", "504");
                            break;
                        case "505":
                            resultado.ActualizarResultado(false, "La sucursal no tiene la configuración de acceso facial.", "505");
                            break;
                        case "507":
                            resultado.ActualizarResultado(false, "El X-Signature: " + tokenSucursal + " proporcionado es invalido, no coincide con el de la sucursal.", "507");
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
