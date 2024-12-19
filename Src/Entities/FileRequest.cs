using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BatchExtensions.Entities
{
    public class FileRequest
    {
        [JsonPropertyName("custom_id")]
        public string? CustomId { get; set; }
        [JsonPropertyName("method")]
        public string? Method { get; set; }
        [JsonPropertyName("url")]
        public string? Url { get; set; }
        [JsonPropertyName("body")]
        public FileRequestBody? Body { get; set; }
    }
}
