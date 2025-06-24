using DeportNetReconocimiento.Api.Data.Dtos.Response;
using DeportNetReconocimiento.Hikvision.SDKHikvision;
using DeportNetReconocimiento.Utils;
using System.Dynamic;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace DeportNetReconocimiento.Api.Services
{
    public class WebServicesDeportnet
    {
        /*Testing*/
        const string urlEntradaClienteTest = "https://testing.deportnet.com/facialAccess/facialAccessCheckUserEnter";
        const string urlBajaClienteTest = "https://testing.deportnet.com/facialAccess/facialAccessDeleteResult";
        const string urlAltaClienteTest = "https://testing.deportnet.com/facialAccess/facialAccessLectureResult";
        //a partir de aca no estan en prod
        const string urlClientesTest = "https://testing.deportnet.com/offlineAccess/offlineAccessGetMembers";
        const string urlConceptsTest = "https://testing.deportnet.com/offlineAccess/offlineAccessGetConcepts";
        const string urlEmpleadosTest = "https://testing.deportnet.com/offlineAccess/offlineAccessGetUsers";
        const string urlEnviarAccesosTest = "https://testing.deportnet.com/offlineAccess/offlineSetAccess";
        const string urlConfiguracionAccesoTest = "https://testing.deportnet.com/offlineAccess/offlineAccessGetConfiguration";
        const string urlBajaMasivaClienteTest = "https://testing.deportnet.com/facialAccess/massiveMembersDelete";
        const string urlVerificarEstadoLoteTest = "https://testing.deportnet.com/offlineAccess/offlineGetMembersAccessResult";

        /*Produccion*/
        const string urlEntradaCliente = "https://deportnet.com/facialAccess/facialAccessCheckUserEnter";
        const string urlBajaCliente = "https://deportnet.com/facialAccess/facialAccessDeleteResult";
        const string urlAltaCliente = "https://deportnet.com/facialAccess/facialAccessLectureResult";

        public static async Task<string> ControlDeAcceso(string nroTarjeta, string idSucursal, string? rtaManual = null, string? idEmpleado = null, string? lector = null)
        {
            dynamic data = new ExpandoObject();
            data.memberId = nroTarjeta;
            data.activeBranchId = idSucursal;

            if (rtaManual != null)
            {
                data.isSuccessful = rtaManual;
                data.manualAllowedAccess = rtaManual;
            }

            if (idEmpleado != null)
            {
                data.companyMemberId = idEmpleado;
            }

            if (lector != null)
            {
                data.lectorNumber = lector;
            }

            string json = JsonSerializer.Serialize(data);
            Console.WriteLine("JSON serializado a enviar:\n" + json);

            return await FetchInformacion(json, urlEntradaClienteTest, HttpMethod.Post);
        }

        public static async Task<string> ObtenerCofiguracionDeAccesoOffline(string idSucursal)
        {
            object data = new
            {
                activeBranchId = idSucursal
            };

            return await FetchInformacion(JsonSerializer.Serialize(data), urlConfiguracionAccesoTest, HttpMethod.Post);
        }

        public static async Task<string> ObtenerEmpleadosSucursalOffline(string idSucursal)
        {
            object data = new
            {
                activeBranchId = idSucursal
            };
          
            return await FetchInformacion(JsonSerializer.Serialize(data), urlEmpleadosTest, HttpMethod.Post);
        }

        public static async Task<string> ObtenerClientesOffline(string idSucursal)
        {
            object data = new
            {
                activeBranchId = idSucursal
            };
            return await FetchInformacion(JsonSerializer.Serialize(data), urlClientesTest, HttpMethod.Post);
        }

        public static async Task<string> ObtenerConceptsOffline(string idSucursal)
        {
            object data = new
            {
                activeBranchId = idSucursal
            };
            return await FetchInformacion(JsonSerializer.Serialize(data), urlConceptsTest, HttpMethod.Post);
        }

        public static async Task<string> EnviarLoteDeAccesos(string json)
        {
            return await FetchInformacion(json, urlEnviarAccesosTest, HttpMethod.Post);
        }

        public static async Task<string> VerificarEstadoLoteAcceso(string json)
        {
            return await FetchInformacion(json, urlVerificarEstadoLoteTest, HttpMethod.Post);
        }

        public static async Task<string> AltaFacialClienteDeportnet(RespuestaAltaBajaCliente rta)
        {
            return await FetchInformacion(rta.ToJson(), urlAltaCliente, HttpMethod.Post);
        }

        public static async Task<string> BajaFacialClienteDeportnet(RespuestaAltaBajaCliente rta)
        {
            return await FetchInformacion(rta.ToJson(), urlBajaCliente, HttpMethod.Post);
        }

        public static async Task<string> BajaFacialMasivaClienteDeportnet(string idSucursal, string? lectorNumber = null, string? maxToDelete = null)
        {
            dynamic data = new ExpandoObject();
            data.activeBranchId = idSucursal;

            if (lectorNumber != null)
            {
                data.lectorNumber = lectorNumber;
            }

            if (maxToDelete != null) { 
                data.maxToDelete = maxToDelete;
            }


            return await FetchInformacion(JsonSerializer.Serialize(data), urlBajaMasivaClienteTest, HttpMethod.Post);

        }

        public static async Task<Hik_Resultado> TestearConexionDeportnet(string tokenSucursal, string idSucursal)
        {

            Console.WriteLine(tokenSucursal + " " + idSucursal);
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

                resultado.ActualizarResultado(true, dataRecibida, "200");
            }
            else
            {
                resultado = CapturarErroresDeportnet(dataRecibida);
            }
            return resultado;
        }

        //Errores específicos de Deportnet
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

            using HttpClient client = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(10) // o 5, según prefieras
            };

            string token = CredencialesUtils.LeerCredencialesBd().BranchToken;//CredencialesUtils.LeerCredencialEspecifica(5); //"H7gVA3r89jvaMuDd";
            
          
            if (string.IsNullOrWhiteSpace(token))
            {
                Console.WriteLine("ERROR: No se encontró el token de la sucursal");
                return "ERROR: No se encontró el token de la sucursal";
            }

            // Configurar el header HTTP_X_SIGNATURE
            client.DefaultRequestHeaders.Add("X-Signature", token);
            
            //creamos el contenido
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //respuesta fetch, la inicializamos con error
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);

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
                    default:
                        break;
                }


                resultado = await VerificarResponseDeportnet(response);
                


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al hacer fetch de informacion: "+ex.Message);

            }

            return resultado.Mensaje;
        }




    }
}
