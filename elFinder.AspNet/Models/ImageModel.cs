using System.Text.Json.Serialization;

namespace elFinder.AspNet.Models
{
    internal class ImageModel : FileModel
    {
        [JsonPropertyName("tmb")]
        public object Thumbnail { get; set; }

        [JsonPropertyName("dim")]
        public string Dimension { get; set; }
    }
}