using System.Text.Json.Serialization;

namespace BatchExtensions.Entities;

public class TrackBatchJobResponse
{
    [JsonPropertyName("cancelled_at")]
    public int? CancelledAt { get; set; }
    [JsonPropertyName("cancelling_at")]
    public int? CancellingAt { get; set; }
    [JsonPropertyName("completed_at")]
    public int? CompletedAt { get; set; }
    [JsonPropertyName("completion_window")]
    public string? CompletionWindow { get; set; }
    [JsonPropertyName("created_at")]
    public int? CreatedAt { get; set; }
    [JsonPropertyName("error_file_id")]
    public string? ErrorFileId { get; set; }
    [JsonPropertyName("expired_at")]
    public int? ExpiredAt { get; set; }
    [JsonPropertyName("expires_at")]
    public int? ExpiresAt { get; set; }
    [JsonPropertyName("failed_at")]
    public int? FailedAt { get; set; }
    [JsonPropertyName("finalizing_at")]
    public int? FinalizingAt { get; set; }
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    [JsonPropertyName("in_progress_at")]
    public int? InProgressAt { get; set; }
    [JsonPropertyName("input_file_id")]
    public string? InputFileId { get; set; }
    [JsonPropertyName("errors")]
    public string? Errors { get; set; }
    [JsonPropertyName("metadata")]
    public string? Metadata { get; set; }
    [JsonPropertyName("object")]
    public string? Object { get; set; }
    [JsonPropertyName("output_file_id")]
    public string? OutputFileId { get; set; }
    [JsonPropertyName("request_counts")]
    public RequestCounts? RequestCounts { get; set; }
    [JsonPropertyName("status")]
    public TrackBatchJobStatus Status { get; set; }
}
