using DeportNetReconocimiento.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Service
{
    internal class DxService
    {

        //Envío a Dx una confirmación de que se agregó con exito un usuario
        public static async Task RegistrarAltaSocioAsync(string id)
        {
            HttpClient client = new HttpClient();

            var jsonContent = JsonSerializer.Serialize(new { id });
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            //Aca hay que poner la url de deportnet
            var response = await client.PostAsync("https://dxserver.com/api/confirmarRostro", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Socio confirmado con éxito.");
            }
            else
            {
                Console.WriteLine($"Error al confirmar: {response.StatusCode}");
            }
        }


        //Envío a Dx una confirmación de que se dio de baja un socio 
        public static async Task RegistrarBajaSocioAsync(string socioId)
        {
            HttpClient client = new HttpClient();

            // Crear el objeto que se enviará como JSON
            var data = new
            {
                id = socioId,
                fechaHora = DateTime.UtcNow.ToString("o") // Formato ISO 8601
            };

            // Serializar el objeto a JSON
            var jsonContent = JsonSerializer.Serialize(data);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Realizar la solicitud POST
            var response = await client.PostAsync("api/bajaSocio", content);

            // Validar la respuesta
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al registrar la baja del socio: {response.ReasonPhrase}");
            }
        }


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

        public async Task<Persona> ValidacionAperturaAsync(string idSocio)
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
    }
}
