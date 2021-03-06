using System.Text.Json.Serialization;

namespace elFinder.AspNet.Models.Commands
{
    public class DebugResponseModel
    {
        [JsonPropertyName("connector")]
        public string Connector => ".net";
    }
}