using BatchExtensions.Entities;

using System.Buffers;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BatchExtensions.Core;

/// <summary>
/// Service for batch processing operations.
/// </summary>
public class BatchProcessingService(string resourceName, string apiKey, string apiVersion = "2024-10-21", HttpClient? httpClient = default) : IBatchProcessingService
{
    private readonly HttpClient _httpClient = httpClient ?? new HttpClient();

    /// <summary>
    /// Uploads a file asynchronously.
    /// </summary>
    /// <param name="filePath">The path to the file to upload.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="UploadResponse"/>.</returns>
    public async Task<UploadResponse?> UploadFileAsync(string filePath, CancellationToken cancellationToken = default)
    {
        using var form = new MultipartFormDataContent();
        _httpClient.DefaultRequestHeaders.Add("api-key", apiKey);
        form.Add(new StringContent("batch"), "purpose");
        var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filePath, cancellationToken));
        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
        form.Add(fileContent, "file", Path.GetFileName(filePath));
        var url = $"https://{resourceName}.openai.azure.com/openai/files?api-version={apiVersion}";
        var response = await _httpClient.PostAsync(url, form, cancellationToken);
        var uploadResponse = await response.Content.ReadFromJsonAsync<UploadResponse>(cancellationToken);
        return response.IsSuccessStatusCode ? uploadResponse : null;
    }

    /// <summary>
    /// Gets the status of an uploaded file asynchronously.
    /// </summary>
    /// <param name="inputFileId">The ID of the uploaded file.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="StatusResponse"/>.</returns>
    public Task<StatusResponse?> GetUploadFileStatusAsync(string inputFileId, CancellationToken cancellationToken = default)
    {
        _httpClient.DefaultRequestHeaders.Add("api-key", apiKey);
        var url = $"https://{resourceName}.openai.azure.com/openai/files/{inputFileId}?api-version={apiVersion}";
        return _httpClient.GetFromJsonAsync<StatusResponse>(url, cancellationToken);
    }

    /// <summary>
    /// Creates a batch job asynchronously.
    /// </summary>
    /// <param name="inputFileId">The ID of the input file.</param>
    /// <param name="completionWindow">The completion window for the batch job.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="CreateBatchJobResponse"/>.</returns>
    public async Task<CreateBatchJobResponse?> CreateBatchJobAsync(string inputFileId, string completionWindow = "24h", CancellationToken cancellationToken = default)
    {
        _httpClient.DefaultRequestHeaders.Add("api-key", apiKey);
        var url = $"https://{resourceName}.openai.azure.com/openai/batches?api-version={apiVersion}";
        var request = new CreateBatchJobRequest
        {
            InputFileId = inputFileId,
            Endpoint = "/chat/completions",
            CompletionWindow = completionWindow
        };

        var response = await _httpClient.PostAsJsonAsync(url, request, cancellationToken);
        return await response.Content.ReadFromJsonAsync<CreateBatchJobResponse>(cancellationToken);
    }

    /// <summary>
    /// Tracks the progress of a batch job asynchronously.
    /// </summary>
    /// <param name="batchJobId">The ID of the batch job.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="TrackBatchJobResponse"/>.</returns>
    public Task<TrackBatchJobResponse?> TrackBatchJobProgressAsync(string batchJobId, CancellationToken cancellationToken = default)
    {
        _httpClient.DefaultRequestHeaders.Add("api-key", apiKey);
        var url = $"https://{resourceName}.openai.azure.com/openai/batches/{batchJobId}?api-version={apiVersion}";
        return _httpClient.GetFromJsonAsync<TrackBatchJobResponse>(url, cancellationToken);
    }

    /// <summary>
    /// Lists all batch jobs asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="ListBatchResponse"/>.</returns>
    public async Task<ListBatchResponse?> ListBatchJobsAsync(CancellationToken cancellationToken = default)
    {
        _httpClient.DefaultRequestHeaders.Add("api-key", apiKey);
        var url = $"https://{resourceName}.openai.azure.com/openai/batches?api-version={apiVersion}";
        var content = await _httpClient.GetFromJsonAsync<ListBatchResponse>(url, cancellationToken);
        return content;
    }

    /// <summary>
    /// Downloads a file asynchronously.
    /// </summary>
    /// <param name="outputFileId">The ID of the output file.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the byte array of the downloaded file.</returns>
    public Task<byte[]> DownloadFileAsync(string outputFileId, CancellationToken cancellationToken = default)
    {
        _httpClient.DefaultRequestHeaders.Add("api-key", apiKey);
        var url = $"https://{resourceName}.openai.azure.com/openai/files/{outputFileId}/content?api-version={apiVersion}";
        return _httpClient.GetByteArrayAsync(url, cancellationToken);
    }

    /// <summary>
    /// Downloads a batch response file asynchronously.
    /// </summary>
    /// <param name="outputFileId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<BatchJobResponse>?> DownloadBatchResponseAsync(string outputFileId, CancellationToken cancellationToken = default)
    {
        _httpClient.DefaultRequestHeaders.Add("api-key", apiKey);
        var url = $"https://{resourceName}.openai.azure.com/openai/files/{outputFileId}/content?api-version={apiVersion}";
        var jsonResponse = await _httpClient.GetStringAsync(url, cancellationToken);
        var jsonItems = jsonResponse.Split(['\n'], StringSplitOptions.RemoveEmptyEntries);
        if (jsonItems.Length > 0)
        {
            var batchJobResponses = new List<BatchJobResponse>();
            foreach (var jsonItem in jsonItems)
            {
                var batchJobResponse = JsonSerializer.Deserialize<BatchJobResponse>(jsonItem);
                if (batchJobResponse != null)
                {
                    batchJobResponses.Add(batchJobResponse);
                }
            }

            return batchJobResponses;
        }

        return null;
    }

    /// <summary>
    /// Uploads a file asynchronously.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="systemPrompt"></param>
    /// <param name="userPrompts"></param>
    /// <param name="method"></param>
    /// <param name="url"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<UploadResponse?> UploadFileAsync(string model, string systemPrompt, string[] userPrompts, string method = "POST", string url = "/chat/completions", CancellationToken cancellationToken = default)
    {
        var stringBuilder = new StringBuilder();
        for (int i = 0; i < userPrompts.Length; i++)
        {
            var fileRequest = new FileRequest()
            {
                CustomId = $"task-{i}",
                Method = method,
                Url = url,
                Body = new FileRequestBody()
                {
                    Model = model,
                    Messages =
                    [
                        new Message()
                        {
                            Role = "system",
                            Content = systemPrompt
                        },
                        new Message()
                        {
                            Role = "user",
                            Content = userPrompts[i]
                        }
                    ]
                }
            };
            stringBuilder.AppendLine(JsonSerializer.Serialize(fileRequest));
        }

        var tempFilePath = Path.ChangeExtension(Path.GetTempFileName(), ".jsonl");
        await File.WriteAllTextAsync(tempFilePath, stringBuilder.ToString(), cancellationToken);
        return await UploadFileAsync(tempFilePath, cancellationToken);
    }
}
