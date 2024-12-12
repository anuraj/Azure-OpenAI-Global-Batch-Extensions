using System.Text.Json.Serialization;

namespace BatchExtensions.Entities;

public class Jailbreak
{
    [JsonPropertyName("filtered")]
    public bool? Filtered { get; set; }

    [JsonPropertyName("detected")]
    public bool? Detected { get; set; }
}


