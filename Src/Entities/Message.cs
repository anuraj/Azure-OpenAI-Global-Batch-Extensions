using System.Text.Json.Serialization;

namespace BatchExtensions.Entities;

public class Message
{
    [JsonPropertyName("content")]
    public string? Content { get; set; }

    [JsonPropertyName("role")]
    public string? Role { get; set; }
}


