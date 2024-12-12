using System.Text.Json.Serialization;

namespace BatchExtensions.Entities;

public class ContentFilterResults
{
    [JsonPropertyName("hate")]
    public Hate? Hate { get; set; }

    [JsonPropertyName("self_harm")]
    public SelfHarm? SelfHarm { get; set; }

    [JsonPropertyName("sexual")]
    public Sexual? Sexual { get; set; }

    [JsonPropertyName("violence")]
    public Violence? Violence { get; set; }

    [JsonPropertyName("jailbreak")]
    public Jailbreak? Jailbreak { get; set; }
}


