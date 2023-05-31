using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NetBom.Core.Models.Json;

public class Report
{
    [JsonPropertyName("report")]
    public ReportInfo ReportInfo { get; set; }

    [JsonPropertyName("tree")]
    public List<Package> Tree { get; set; }

    [JsonPropertyName("list")]
    public List<Package> List { get; set; }
}
