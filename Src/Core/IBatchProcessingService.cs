using BatchExtensions.Entities;

namespace BatchExtensions.Core;
public interface IBatchProcessingService
{
    Task<UploadResponse?> UploadFileAsync(string filePath, CancellationToken cancellationToken = default);
    Task<StatusResponse?> GetUploadFileStatusAsync(string inputFileId, CancellationToken cancellationToken = default);
    Task<CreateBatchJobResponse?> CreateBatchJobAsync(string inputFileId, string completionWindow = "24h", CancellationToken cancellationToken = default);
    Task<TrackBatchJobResponse?> TrackBatchJobProgressAsync(string batchJobId, CancellationToken cancellationToken = default);
    Task<ListBatchResponse?> ListBatchJobsAsync(CancellationToken cancellationToken = default);
    Task<byte[]> DownloadFileAsync(string outputFileId, CancellationToken cancellationToken = default);
}