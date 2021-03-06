using System.Text.Json.Serialization;

namespace elFinder.AspNet.Models
{
    public class RootModel : DirectoryModel
    {
        [JsonPropertyName("isroot")]
        public byte IsRoot => 1;
    }
}