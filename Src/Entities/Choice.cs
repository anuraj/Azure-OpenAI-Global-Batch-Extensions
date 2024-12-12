using System.Text.Json.Serialization;

namespace BatchExtensions.Entities;

public class Choice
{
    [JsonPropertyName("content_filter_results")]
    public ContentFilterResults? ContentFilterResults { get; set; }

    [JsonPropertyName("finish_reason")]
    public string? FinishReason { get; set; }

    [JsonPropertyName("index")]
    public int? Index { get; set; }

    [JsonPropertyName("logprobs")]
    public object? Logprobs { get; set; }

    [JsonPropertyName("message")]
    public Message? Message { get; set; }
}


