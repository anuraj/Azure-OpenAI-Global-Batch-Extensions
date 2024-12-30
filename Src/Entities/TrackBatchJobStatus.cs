using System.Text.Json.Serialization;

namespace BatchExtensions.Entities;

[JsonConverter(typeof(JsonStringEnumConverter<TrackBatchJobStatus>))]
public enum TrackBatchJobStatus
{
    [JsonPropertyName("validating")]
    Validating,
    [JsonPropertyName("failed")]
    Failed,
    [JsonPropertyName("in_progress")]
    InProgress,
    [JsonPropertyName("finalizing")]
    Finalizing,
    [JsonPropertyName("completed")]
    Completed,
    [JsonPropertyName("expired")]
    Expired,
    [JsonPropertyName("cancelling")]
    Cancelling,
    [JsonPropertyName("cancelled")]
    Cancelled
}
