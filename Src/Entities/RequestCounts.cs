using System.Text.Json.Serialization;

namespace BatchExtensions.Entities;

public class RequestCounts
{
    [JsonPropertyName("total")]
    public int? Total { get; set; }
    [JsonPropertyName("completed")]
    public int? Completed { get; set; }
    [JsonPropertyName("failed")]
    public int? Failed { get; set; }
}