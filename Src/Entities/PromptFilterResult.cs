using System.Text.Json.Serialization;

namespace BatchExtensions.Entities;

public class PromptFilterResult
{
    [JsonPropertyName("prompt_index")]
    public int? PromptIndex { get; set; }

    [JsonPropertyName("content_filter_results")]
    public ContentFilterResults? ContentFilterResults { get; set; }
}


