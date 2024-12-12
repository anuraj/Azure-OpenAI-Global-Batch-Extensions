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
    public async Task UploadFileAsyncTest()
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
}
