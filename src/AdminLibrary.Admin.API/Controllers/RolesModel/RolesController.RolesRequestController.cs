using System.Text.Json.Serialization;

namespace AdminLibrary.Admin.API.Controllers.RolesModel
{
    public class RolesRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        [JsonPropertyName("status")]
        public bool status { get; set; } = true;
    }

    public class RolesRequestUpdate
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        [JsonPropertyName("status")]
        public bool status { get; set; }
    }
}
