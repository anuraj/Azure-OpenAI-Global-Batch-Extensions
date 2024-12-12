using System.Text.Json.Serialization;

namespace BatchExtensions.Entities;

public class BatchJobResponse
{
    [JsonPropertyName("custom_id")]
    public string? CustomId { get; set; }

    [JsonPropertyName("response")]
    public Response? Response { get; set; }

    [JsonPropertyName("error")]
    public object? Error { get; set; }
}


