using DeportNetReconocimiento.Api.Dtos.Request;
using DeportNetReconocimiento.Api.Dtos.Response;
using DeportNetReconocimiento.GUI;
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
	                "manualAllowedAccess": "17393", (T o F)
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

        private static async Task<string> FetchInformacion(string json, string url, HttpMethod metodo)
        {

            using HttpClient client = new HttpClient();

            // Configurar el header HTTP_X_SIGNATURE con el valor "1234"
            client.DefaultRequestHeaders.Add("X-Signature", "1234");


            //creamos el contenido
            var content = new StringContent(json, Encoding.UTF8, "application/json");




            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
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


            //response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }




    }
}
