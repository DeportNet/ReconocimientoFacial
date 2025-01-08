using DeportNetReconocimiento.GUI;
using DeportNetReconocimiento.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.ObjectiveC;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DeportNetReconocimiento.Service
{
    internal class DxService
    {

        const string url = "https://testing.deportnet.com/facialAccess/facialAccessCheckUserEnter";


        public static async Task ConfirmarAccesoPreguntaAsync(string idSocio, string rta)
        {
            HttpClient client = new HttpClient();

            var data = new
            {
                id = idSocio,
                fechaHora = DateTime.UtcNow.ToString("o"),
                respuesta = rta
            };


            var JsonContent = JsonSerializer.Serialize(data);
            var content = new StringContent(JsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/confirmacionPregunta", content);

            // Validar la respuesta
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al registrar al enviar la respuesta: {response.ReasonPhrase}");
            }

        }

        public static async Task<Persona> ValidacionAperturaAsync(string idSocio)
        {
            HttpClient client = new HttpClient();
            var data = new { id = idSocio };
            var jsonContent = JsonSerializer.Serialize(data);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/validarsocio", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<Persona>(jsonResponse);
            }
            else
            {
                throw new Exception($"Error al validar socio: {response.ReasonPhrase}");
            }

        }

        public static async Task manejarReconocimientoSociosAsync(object json)
        {
            try
            {
                var response = await SocioDetectadoAsync(json);
                ProcesarRespuesta(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error durante la comunicación");
            }
        }

        static void ProcesarRespuesta(string response)
        {

            using (JsonDocument doc = JsonDocument.Parse(response))
            {
                JsonElement root = doc.RootElement;

                //Busco la propiedad branchAcces y digo que el elemento  es de tipo arreglo
                if (root.TryGetProperty("branchAccess", out JsonElement branchAccess) && branchAccess.ValueKind == JsonValueKind.Array)
                {
                    if (branchAccess[0].ValueKind == JsonValueKind.String)
                    {
                        MessageBox.Show("Proceso todo el tema de la pregunta");
                    }

                    if (branchAccess[2].ValueKind != JsonValueKind.Null)
                    {
                        RespuestaDx jsonDeportnet = new RespuestaDx();


                        jsonDeportnet.Id = branchAccess[2].GetProperty("id").ToString();
                        jsonDeportnet.Nombre = branchAccess[2].GetProperty("firstName").ToString();
                        jsonDeportnet.Apellido = branchAccess[2].GetProperty("lastName").ToString();
                        jsonDeportnet.NombreCompleto = branchAccess[2].GetProperty("name").ToString();
                        jsonDeportnet.Estado = branchAccess[2].GetProperty("status").ToString();
                        jsonDeportnet.MensajeCrudo = branchAccess[2].GetProperty("accesStatus").ToString();
                        jsonDeportnet.MensajeAccesoDenegado = branchAccess[2].GetProperty("accessError").ToString();
                        jsonDeportnet.MensajeAccesoAceptado = branchAccess[2].GetProperty("accessOK").ToString();
                        jsonDeportnet.Mostrarcumpleanios= branchAccess[2].GetProperty("showBirthday").ToString();


                        WFPrincipal.ObtenerInstancia.ActualizarDatos(1, jsonDeportnet);
                    }

                }
                else
                {
                    Console.WriteLine("No está la propiedad branch access.");
                }


            }
        }


        public static async Task<string> SocioDetectadoAsync(object json)
        {

            using (HttpClient client = new HttpClient())
            {
                string jsonString = JsonSerializer.Serialize(json);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
