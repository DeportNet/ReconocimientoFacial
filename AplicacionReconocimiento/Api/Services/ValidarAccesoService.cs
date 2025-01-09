using DeportNetReconocimiento.Api.Dtos.Response;
using DeportNetReconocimiento.GUI;
using System.Text;
using System.Text.Json;


namespace DeportNetReconocimiento.Api.Services
{
    internal class ValidarAccesoService
    {

        const string url = "https://testing.deportnet.com/facialAccess/facialAccessCheckUserEnter";


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
                        ValidarAccesoResponse jsonDeportnet = new ValidarAccesoResponse();


                        jsonDeportnet.Id = branchAccess[2].GetProperty("id").ToString();
                        jsonDeportnet.Nombre = branchAccess[2].GetProperty("firstName").ToString();
                        jsonDeportnet.Apellido = branchAccess[2].GetProperty("lastName").ToString();
                        jsonDeportnet.NombreCompleto = branchAccess[2].GetProperty("name").ToString();
                        jsonDeportnet.Estado = branchAccess[2].GetProperty("status").ToString();
                        jsonDeportnet.MensajeCrudo = branchAccess[2].GetProperty("accesStatus").ToString();
                        jsonDeportnet.MensajeAccesoDenegado = branchAccess[2].GetProperty("accessError").ToString();
                        jsonDeportnet.MensajeAccesoAceptado = branchAccess[2].GetProperty("accessOK").ToString();
                        jsonDeportnet.Mostrarcumpleanios = branchAccess[2].GetProperty("showBirthday").ToString();


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
