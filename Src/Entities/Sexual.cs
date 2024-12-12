﻿using System.Text.Json.Serialization;

namespace BatchExtensions.Entities;

public class Sexual
{
    [JsonPropertyName("filtered")]
    public bool? Filtered { get; set; }

    [JsonPropertyName("severity")]
    public string? Severity { get; set; }
}


