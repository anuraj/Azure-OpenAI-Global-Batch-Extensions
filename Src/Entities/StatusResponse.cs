using System.Text.Json.Serialization;

namespace BatchExtensions.Entities;

public class StatusResponse
{
    [JsonPropertyName("status")]
    public string? Status { get; set; }
    [JsonPropertyName("bytes")]
    public int Bytes { get; set; }
    [JsonPropertyName("purpose")]
    public string? Purpose { get; set; }
    [JsonPropertyName("filename")]
    public string? Filename { get; set; }
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    [JsonPropertyName("created_at")]
    public int CreatedAt { get; set; }
    [JsonPropertyName("object")]
    public string? Object { get; set; }
}
