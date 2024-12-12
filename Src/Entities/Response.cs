using System.Text.Json.Serialization;

namespace BatchExtensions.Entities;

public class Response
{
    [JsonPropertyName("body")]
    public Body? Body { get; set; }

    [JsonPropertyName("request_id")]
    public string? RequestId { get; set; }

    [JsonPropertyName("status_code")]
    public int? StatusCode { get; set; }
}


