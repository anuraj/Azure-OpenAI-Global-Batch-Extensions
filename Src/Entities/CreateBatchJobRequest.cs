using System.Text.Json.Serialization;

namespace BatchExtensions.Entities;

public class CreateBatchJobRequest
{
    [JsonPropertyName("input_file_id")]
    public string? InputFileId { get; set; }
    [JsonPropertyName("endpoint")]
    public string? Endpoint { get; set; }
    [JsonPropertyName("completion_window")]
    public string? CompletionWindow { get; set; } = "24h";
}
