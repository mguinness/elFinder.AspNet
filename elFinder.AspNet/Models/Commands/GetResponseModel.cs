using System.Text.Json.Serialization;

namespace elFinder.AspNet.Models.Commands
{
    public class GetResponseModel
    {
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}