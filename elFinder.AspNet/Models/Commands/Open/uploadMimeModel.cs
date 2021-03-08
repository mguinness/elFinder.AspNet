using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace elFinder.AspNet.Models.Commands
{
    public class UploadMimeModel
    {
        [JsonPropertyName("allow")]
        public List<string> Allow { get; protected set; }

        [JsonPropertyName("deny")]
        public List<string> Deny { get; protected set; }

        [JsonPropertyName("firstOrder"), JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderType FirstOrder { get; protected set; }

        public UploadMimeModel(IEnumerable<string> allow, IEnumerable<string> deny, OrderType first = OrderType.Deny)
        {
            Allow = new List<string>(allow);
            Deny = new List<string>(deny);
            FirstOrder = first;
        }

        public enum OrderType
        {
            Allow,
            Deny
        }
    }
}