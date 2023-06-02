using System.Text.Json.Serialization;

namespace NetBom.Core.Models.Json;

public class License
{
    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}
