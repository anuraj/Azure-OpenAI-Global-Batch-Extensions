# Azure OpenAI Global Batch Extensions

The Azure OpenAI Batch API is designed to handle large-scale and high-volume processing tasks efficiently. Process asynchronous groups of requests with separate quota, with 24-hour target turnaround, at **50% less cost** than global standard. With batch processing, rather than send one request at a time you send a large number of requests in a single file. Global batch requests have a separate enqueued token quota avoiding any disruption of your online workloads.

This is a C# wrapper on top of the REST endpoints.

```csharp
var batchProcessingService = new BatchProcessingService(resourceName, apiKey);

var uploadResponse = await batchProcessingService.UploadFileAsync(@"C:\BatchInput\test.jsonl");
var fileInputId = uploadResponse.Id;
Console.WriteLine($"File uploaded successfully with file input id: {fileInputId}");

var uploadFileStatus = await batchProcessingService.GetUploadFileStatusAsync(fileInputId);
Console.WriteLine($"File upload status: {uploadFileStatus.Status}");

var jobResponse = await batchProcessingService.CreateBatchJobAsync(fileInputId);
Console.WriteLine($"Job created successfully with job id: {jobResponse.Id}");

var jobs = await batchProcessingService.ListBatchJobsAsync();
foreach (var job in jobs.Data)
{
    Console.WriteLine($"Job Id: {job.Id}, Status: {job.Status}");
    if(job.Status == "completed")
    {
        var output = await batchProcessingService.DownloadFileAsync(job.Id);
        File.WriteAllBytes(@$"C:\BatchOutput\{job.Id}.jsonl", output);
    }
}
```

For the example file checkout [Prepare your batch file](https://learn.microsoft.com/azure/ai-services/openai/how-to/batch?tabs=standard-input%2Cpython-secure&pivots=rest-api&WT.mc_id=DT-MVP-5002040#preparing-your-batch-file).