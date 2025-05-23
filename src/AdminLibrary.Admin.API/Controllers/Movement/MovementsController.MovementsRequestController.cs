using System.Text.Json.Serialization;

namespace AdminLibrary.Admin.API.Controllers.Movement
{
    public class MovementsRequest
    {

        [JsonPropertyName("movementType")]
        public string? MovementType { get; set; }
        [JsonPropertyName("materialId")]
        public int MaterialId { get; set; }
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        [JsonPropertyName("observations")]
        public string? Observations { get; set; }
    }
}
