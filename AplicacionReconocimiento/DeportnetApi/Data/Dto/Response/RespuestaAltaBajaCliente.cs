using System.Text.Json;
using System.Text.Json.Serialization;

namespace DeportNetReconocimiento.Api.Data.Dtos.Response
{
    public class RespuestaAltaBajaCliente
    {
        [JsonPropertyName("activeBranchId")]
        public string ActiveBranchId { get; set; }

        [JsonPropertyName("memberId")]
        public string MemberId { get; set; }

        [JsonPropertyName("errorMessage")]
        public string? ErrorMessage { get; set; }

        [JsonPropertyName("successMessage")]
        public string? SuccessMessage { get; set; }

        [JsonPropertyName("isSuccessful")]
        public string IsSuccessful { get; set; }

        [JsonPropertyName("lectorNumber")]
        public string LectorNumber { get; set; }

        public RespuestaAltaBajaCliente(string idSucursal, string idCliente, string mensaje, string exito, string lector)
        {
            ActiveBranchId = idSucursal;
            MemberId = idCliente;
            IsSuccessful = exito;
            LectorNumber = lector;

            switch (exito)
            {
                case "T":
                    SuccessMessage = mensaje;
                    ErrorMessage = null;
                    break;
                case "F":
                    ErrorMessage = mensaje;
                    SuccessMessage = null;
                    break;
                default:
                    ErrorMessage = mensaje;
                    SuccessMessage = null;
                    break;
            }
        }



        public string ToJson()
        {
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, // Ignorar valores nulos
                WriteIndented = true // Opcional: Formatear JSON
            };

            return JsonSerializer.Serialize(this, options);
        }
    }
}
