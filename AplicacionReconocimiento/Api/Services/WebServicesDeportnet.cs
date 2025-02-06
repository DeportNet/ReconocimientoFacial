using DeportNetReconocimiento.SDK;
using DeportNetReconocimiento.Utils;
using System.Text;
using System.Text.Json;



namespace DeportNetReconocimiento.Api.Services
{
    public class WebServicesDeportnet
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
            var contenido = new StringContent(JsonSerializer.Serialize(dataEnviar), Encoding.UTF8, "application/json");


            try
            {
                //respuesta fetch
                HttpResponseMessage response = await client.PostAsync(urlEntradaCliente, contenido);


                resultado = await VerificarResponseDeportnet(response);

                
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


        private static async Task<Hik_Resultado> VerificarResponseDeportnet(HttpResponseMessage responseMessage)
        {
            Hik_Resultado resultado = new Hik_Resultado();

            

            //leo el json de la respuesta recibida
            string dataRecibida = await responseMessage.Content.ReadAsStringAsync();

            
            //si el status code es 200, entonces la conexión fue exitosa
            if (responseMessage.IsSuccessStatusCode)
            {
                //if(dataRecibida != "200")
                //{

                //    using JsonDocument doc = JsonDocument.Parse(dataRecibida);

                //    JsonElement root = doc.RootElement;

                //    //Busco la propiedad branchAcces y digo que el elemento  es de tipo arreglo

                //    if ((root.TryGetProperty("branchAccess", out JsonElement branchAccess) && branchAccess.ValueKind == JsonValueKind.Array))
                //    {
                //        //en realidad arroja "No se encontro el Socio" pero es justamente lo que necesitamos para saber si la conexión fue exitosa
                //    }
                //}
                
                resultado.ActualizarResultado(true, dataRecibida, "200");



            }
            else
            {
                
                resultado = CapturarErroresDeportnet(dataRecibida);
            }
            return resultado;
        }


        private static Hik_Resultado CapturarErroresDeportnet(string dataRecibida)
        {
            //200 = Proceso realizado correctamente
            //308 = El HTTP_X_SIGNATURE es nulo o vacío
            //309 = No se envió HTTP_X_SIGNATURE
            //400 = No se pudo procesar el request(por no venir en formato JSON, por ejemplo)
            //401 = No se envió el Id de sucursal
            //402 = No se envió el Id de socio
            //403 = El Id de sucursal es nulo o vacío
            //404 = El Id de socio es nulo o vacío
            //408 = No se encontró el socio
            //501 = El módulo de acceso facial no existe o ha sido inactivado en DeportNet(es un campo en una tabla)
            //502 = No se encontró la sucursal
            //503 = La sucursal no tiene asignada el módulo de acceso facial
            //504 = La sucursal no tiene configurado el token
            //505 = La sucursal no tiene la configuración de acceso facial
            //507 = Token inválido(no coinciden el que está configurado con el que se envía)

            Hik_Resultado resultado = new Hik_Resultado();
            // Capturar errores HTTP específicos
            switch (dataRecibida)
            {
                case "308":
                    resultado.ActualizarResultado(false, "El X-Signature es nulo o vacío.", "308");
                    break;
                case "309":
                    resultado.ActualizarResultado(false, "No se envio en la cabecera el X-Signature.", "309");
                    break;
                case "400":
                    resultado.ActualizarResultado(false, "No se pudo procesar el request(por no venir en formato json por ejemplo).", "400");
                    break;
                case "401":
                    resultado.ActualizarResultado(false, "No se envió el Id de sucursal.", "401");
                    break;
                case "402":
                    resultado.ActualizarResultado(false, "No se envió el Id de socio.", "402");
                    break;
                case "403":
                    resultado.ActualizarResultado(false, "El Id de sucursal es nulo o vacío.", "403");
                    break;
                case "404":
                    resultado.ActualizarResultado(false, "El Id de socio es nulo o vacío.", "404");
                    break;
                case "408":
                    resultado.ActualizarResultado(false, "No se encontró el socio.", "408");
                    break;
                case "501":
                    resultado.ActualizarResultado(false, "El módulo de acceso facial no existe o ha sido inactivado en DeportNet.", "501");
                    break;
                case "502":
                    resultado.ActualizarResultado(false, "No se encontró la sucursal.", "502");
                    break;
                case "503":
                    resultado.ActualizarResultado(false, "La sucursal con el ID ingresado no tiene asignada el módulo de acceso facial.", "503");
                    break;
                case "504":
                    resultado.ActualizarResultado(false, "La sucursal con el ID ingresado no tiene configurado el token.", "504");
                    break;
                case "505":
                    resultado.ActualizarResultado(false, "La sucursal no tiene la configuración de acceso facial.", "505");
                    break;
                case "507":
                    resultado.ActualizarResultado(false, "El token de la sucursal proporcionado es invalido, no coincide con el de la sucursal.", "507");
                    break;
                default:
                    resultado.ActualizarResultado(false, $"Error inesperado: "+ dataRecibida, "Inesperado.");
                    break;
            }

            return resultado;
        }


        private static async Task<string> FetchInformacion(string json, string url, HttpMethod metodo)
        {
            Hik_Resultado resultado = new Hik_Resultado();

            using HttpClient client = new HttpClient();

            string token= CredencialesUtils.LeerCredenciales()[5];

            // Configurar el header HTTP_X_SIGNATURE
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


                resultado = await VerificarResponseDeportnet(response);
                Console.WriteLine("VERIFICAMOS RESULTADOO"+ resultado.Mensaje);



            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al hacer fetch de informacion: "+ex.Message);

            }

            return resultado.Mensaje;
        }




    }
}
