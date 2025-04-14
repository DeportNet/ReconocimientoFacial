using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Data.Dtos.Response
{
    public class RespuestaAccesoManual
    {
        [JsonPropertyName("activeBranchId")]
        private string activeBranchId;

        [JsonPropertyName("memberId")]
        private string memberId;

        [JsonPropertyName("manualAllowedAccess")]
        private string manualAllowedAccess;

        [JsonPropertyName("isSuccessful")]
        private string isSuccessful;

        public RespuestaAccesoManual(string idSucursal, string idCliente, string exito)
        {
            activeBranchId = idSucursal;
            memberId = idCliente;
            manualAllowedAccess = idCliente;
            isSuccessful = exito;
        }

        public string ActiveBranchId
        {
            get { return activeBranchId; }
            set { activeBranchId = value; }
        }
        public string MemberId
        {
            get { return memberId; }
            set { memberId = value; }
        }

        public string ManualAllowedAccess
        {
            get { return manualAllowedAccess; }
            set { manualAllowedAccess = value; }
        }

        public string IsSuccessful
        {
            get { return isSuccessful; }
            set { isSuccessful = value; }
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
