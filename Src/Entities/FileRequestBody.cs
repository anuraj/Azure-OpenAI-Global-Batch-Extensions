using System.Text.Json.Serialization;

namespace BatchExtensions.Entities
{
    public class FileRequestBody
    {
        [JsonPropertyName("model")]
        public string? Model { get; set; }
        [JsonPropertyName("messages")]
        public Message[]? Messages { get; set; }
    }
}
