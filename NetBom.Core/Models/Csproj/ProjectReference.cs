using System.Xml.Serialization;

namespace NetBom.Core.Models.Csproj;

[XmlRoot(ElementName = "ProjectReference")]
public class ProjectReference
{
    [XmlAttribute(AttributeName = "Include")]
    public string Include { get; set; }

    [XmlAttribute(AttributeName = "PrivateAssets")]
    public string PrivateAssets { get; set; }
}
