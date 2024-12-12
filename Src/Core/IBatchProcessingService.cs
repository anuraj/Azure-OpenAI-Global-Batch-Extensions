using BatchExtensions.Entities;

namespace BatchExtensions.Core;

/// <summary>
/// Interface for batch processing service.
/// </summary>
public interface IBatchProcessingService
{
    /// <summary>
    /// Uploads a file asynchronously.
    /// </summary>
    /// <param name="filePath">The path to the file to upload.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="UploadResponse"/>.</returns>
    Task<UploadResponse?> UploadFileAsync(string filePath, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the status of an uploaded file asynchronously.
    /// </summary>
    /// <param name="inputFileId">The ID of the uploaded file.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="StatusResponse"/>.</returns>
    Task<StatusResponse?> GetUploadFileStatusAsync(string inputFileId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a batch job asynchronously.
    /// </summary>
    /// <param name="inputFileId">The ID of the input file.</param>
    /// <param name="completionWindow">The completion window for the batch job.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="CreateBatchJobResponse"/>.</returns>
    Task<CreateBatchJobResponse?> CreateBatchJobAsync(string inputFileId, string completionWindow = "24h", CancellationToken cancellationToken = default);

    /// <summary>
    /// Tracks the progress of a batch job asynchronously.
    /// </summary>
    /// <param name="batchJobId">The ID of the batch job.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="TrackBatchJobResponse"/>.</returns>
    Task<TrackBatchJobResponse?> TrackBatchJobProgressAsync(string batchJobId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists all batch jobs asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="ListBatchResponse"/>.</returns>
    Task<ListBatchResponse?> ListBatchJobsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Downloads a file asynchronously.
    /// </summary>
    /// <param name="outputFileId">The ID of the output file.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the byte array of the downloaded file.</returns>
    Task<byte[]> DownloadFileAsync(string outputFileId, CancellationToken cancellationToken = default);

    Task<List<BatchJobResponse>?> DownloadBatchResponseAsync(string outputFileId, CancellationToken cancellationToken = default);
}