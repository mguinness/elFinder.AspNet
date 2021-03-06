using System.Text.Json.Serialization;

namespace elFinder.AspNet.Models.Commands
{
    public class SizeResponseModel
    {
        [JsonPropertyName("size")]
        public long Size { get; set; }

        [JsonPropertyName("fileCnt")]
        public int FileCount { get; set; }

        [JsonPropertyName("dirCnt")]
        public int DirectoryCount { get; set; }
    }
}