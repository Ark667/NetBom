using System.Xml.Serialization;

namespace NetBom.Core.Models.Csproj;

[XmlRoot(ElementName = "Target")]
public class Target
{
    [XmlElement(ElementName = "ItemGroup")]
    public ItemGroup ItemGroup { get; set; }

    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "DependsOnTargets")]
    public string DependsOnTargets { get; set; }
}
