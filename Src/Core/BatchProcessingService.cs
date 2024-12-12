using BatchExtensions.Entities;

using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BatchExtensions.Core;
public class BatchProcessingService : IBatchProcessingService
{
    private readonly string _resourceName;
    private readonly string _apiKey;
    private const string apiVersion = "2024-10-21";

    private readonly HttpClient _httpClient;
    public BatchProcessingService(string resourceName, string apiKey, HttpClient? httpClient = default)
    {
        _resourceName = resourceName;
        _apiKey = apiKey;

        _httpClient = httpClient ?? new HttpClient();
    }
    public async Task<UploadResponse?> UploadFileAsync(string filePath, CancellationToken cancellationToken = default)
    {
        using var form = new MultipartFormDataContent();
        _httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);
        form.Add(new StringContent("batch"), "purpose");
        var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filePath, cancellationToken));
        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
        form.Add(fileContent, "file", Path.GetFileName(filePath));
        var url = $"https://{_resourceName}.openai.azure.com/openai/files?api-version={apiVersion}";
        var response = await _httpClient.PostAsync(url, form, cancellationToken);
        var uploadResponse = await response.Content.ReadFromJsonAsync<UploadResponse>(cancellationToken);
        return response.IsSuccessStatusCode ? uploadResponse : null;
    }
    public Task<StatusResponse?> GetUploadFileStatusAsync(string inputFileId, CancellationToken cancellationToken = default)
    {
        _httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);
        var url = $"https://{_resourceName}.openai.azure.com/openai/files/{inputFileId}?api-version={apiVersion}";
        return _httpClient.GetFromJsonAsync<StatusResponse>(url, cancellationToken);
    }
    public async Task<CreateBatchJobResponse?> CreateBatchJobAsync(string inputFileId, string completionWindow = "24h", CancellationToken cancellationToken = default)
    {
        _httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);
        var url = $"https://{_resourceName}.openai.azure.com/openai/batches?api-version={apiVersion}";
        var request = new CreateBatchJobRequest
        {
            InputFileId = inputFileId,
            Endpoint = "/chat/completions",
            CompletionWindow = completionWindow
        };

        var response = await _httpClient.PostAsJsonAsync(url, request, cancellationToken);
        return await response.Content.ReadFromJsonAsync<CreateBatchJobResponse>(cancellationToken);
    }
    public Task<TrackBatchJobResponse?> TrackBatchJobProgressAsync(string batchJobId, CancellationToken cancellationToken = default)
    {
        _httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);
        var url = $"https://{_resourceName}.openai.azure.com/openai/batches/{batchJobId}?api-version={apiVersion}";
        return _httpClient.GetFromJsonAsync<TrackBatchJobResponse>(url, cancellationToken);
    }
    public async Task<ListBatchResponse?> ListBatchJobsAsync(CancellationToken cancellationToken = default)
    {
        _httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);
        var url = $"https://{_resourceName}.openai.azure.com/openai/batches?api-version={apiVersion}";
        var content = await _httpClient.GetFromJsonAsync<ListBatchResponse>(url, cancellationToken);
        return content;
    }
    public Task<byte[]> DownloadFileAsync(string outputFileId, CancellationToken cancellationToken = default) 
    {
        _httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);
        var url = $"https://{_resourceName}.openai.azure.com/openai/files/{outputFileId}/content?api-version={apiVersion}";
        return _httpClient.GetByteArrayAsync(url, cancellationToken);
    }
}