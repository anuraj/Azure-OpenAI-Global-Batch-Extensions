using System.Text.Json.Serialization;

namespace BatchExtensions.Entities;

public class ListBatchResponse
{
    [JsonPropertyName("data")]
    public Data[]? Data { get; set; }
    [JsonPropertyName("first_id")]
    public string? FirstId { get; set; }
    [JsonPropertyName("has_more")]
    public bool HasMore { get; set; }
    [JsonPropertyName("last_id")]
    public string? LastId { get; set; }
}
