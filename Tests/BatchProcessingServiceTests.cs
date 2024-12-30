using Moq.Protected;
using Moq;
using System.Net;
using System.Text;
using System.Text.Json;
using BatchExtensions.Core;
using BatchExtensions.Entities;

namespace BatchExtensions.Tests;

public class BatchProcessingServiceTests
{
    [Fact]
    public async Task UploadFileAsyncTestReturnsSuccessStatusCode()
    {
        var uploadRequest = new UploadResponse()
        {
            Id = "id",
            Status = "status",
            Filename = "filename",
            Purpose = "purpose",
            Bytes = 1,
            CreatedAt = 1,
            Object = "object",
        };

        var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(uploadRequest), Encoding.UTF8, "application/json")
            });

        var httpClient = new HttpClient(mockHandler.Object);
        var batchProcessingService = new BatchProcessingService("example", Guid.NewGuid().ToString("N"), httpClient: httpClient);

        var uploadResponse = await batchProcessingService.UploadFileAsync(Path.GetTempFileName());

        mockHandler.Protected().Verify("SendAsync", Times.Exactly(1),
            ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Post), ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task UploadFileAsyncTestReturnNoSuccessStatusCode()
    {
        var uploadRequest = new UploadResponse()
        {
            Id = "id",
            Status = "status",
            Filename = "filename",
            Purpose = "purpose",
            Bytes = 1,
            CreatedAt = 1,
            Object = "object",
        };

        var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(JsonSerializer.Serialize(uploadRequest), Encoding.UTF8, "application/json")
            });

        var httpClient = new HttpClient(mockHandler.Object);
        var batchProcessingService = new BatchProcessingService("example", Guid.NewGuid().ToString("N"), httpClient: httpClient);

        var uploadResponse = await batchProcessingService.UploadFileAsync(Path.GetTempFileName());

        mockHandler.Protected().Verify("SendAsync", Times.Exactly(1),
            ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Post), ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task GetUploadFileStatusAsyncReturnSuccess()
    {
        var statusRequest = new StatusResponse()
        {
            Id = "id",
            Status = "status",
            CreatedAt = 1,
            Object = "object",
        };
        var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(statusRequest), Encoding.UTF8, "application/json")
            });

        var httpClient = new HttpClient(mockHandler.Object);
        var batchProcessingService = new BatchProcessingService("example", Guid.NewGuid().ToString("N"), httpClient: httpClient);

        var statusResponse = await batchProcessingService.GetUploadFileStatusAsync("id");

        mockHandler.Protected().Verify("SendAsync", Times.Exactly(1),
            ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Get), ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task GetUploadFileStatusAsyncThrowsException()
    {
        var statusResponse = new StatusResponse()
        {
            Id = "id",
            Status = "status",
            CreatedAt = 1,
            Object = "object",
        };
        var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest
            });

        var httpClient = new HttpClient(mockHandler.Object);
        var batchProcessingService = new BatchProcessingService("example", Guid.NewGuid().ToString("N"), httpClient: httpClient);

        await Assert.ThrowsAsync<HttpRequestException>(() => batchProcessingService.GetUploadFileStatusAsync("id"));

        mockHandler.Protected().Verify("SendAsync", Times.Exactly(1),
            ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Get), ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task CreateBatchJobAsyncReturnSuccess()
    {
        var createBatchJobRequest = new CreateBatchJobResponse()
        {
            Id = "id",
            Status = "status",
            CreatedAt = 1,
            Object = "object",
        };
        var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(createBatchJobRequest), Encoding.UTF8, "application/json")
            });
        var httpClient = new HttpClient(mockHandler.Object);
        var batchProcessingService = new BatchProcessingService("example", Guid.NewGuid().ToString("N"), httpClient: httpClient);
        var createBatchJobResponse = await batchProcessingService.CreateBatchJobAsync("id");
        mockHandler.Protected().Verify("SendAsync", Times.Exactly(1),
            ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Post), ItExpr.IsAny<CancellationToken>());
    }
}
