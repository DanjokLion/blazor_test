using Newtonsoft.Json;

namespace Template.Domain.Models
{
    public class TemplateModel
    {
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("error")]
        public object Error { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }
}