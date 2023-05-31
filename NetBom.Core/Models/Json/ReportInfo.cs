using System.Text.Json.Serialization;

namespace NetBom.Core.Models.Json
{
    public class ReportInfo
    {
        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("project")]
        public string Project { get; set; }
    }
}
