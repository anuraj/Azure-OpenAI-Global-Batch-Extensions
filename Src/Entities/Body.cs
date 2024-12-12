using System.Text.Json.Serialization;

namespace BatchExtensions.Entities;

public class Body
{
    [JsonPropertyName("choices")]
    public List<Choice>? Choices { get; set; }

    [JsonPropertyName("created")]
    public int? Created { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("model")]
    public string? Model { get; set; }

    [JsonPropertyName("object")]
    public string? Object { get; set; }

    [JsonPropertyName("prompt_filter_results")]
    public List<PromptFilterResult>? PromptFilterResults { get; set; }

    [JsonPropertyName("system_fingerprint")]
    public string? SystemFingerprint { get; set; }

    [JsonPropertyName("usage")]
    public Usage? Usage { get; set; }
}


