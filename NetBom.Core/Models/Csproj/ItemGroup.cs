using System.Collections.Generic;
using System.Xml.Serialization;

namespace NetBom.Core.Models.Csproj;

[XmlRoot(ElementName = "ItemGroup")]
public class ItemGroup
{
    [XmlElement(ElementName = "Content")]
    public List<Content> Content { get; set; }

    [XmlElement(ElementName = "PackageReference")]
    public List<PackageReference> PackageReference { get; set; }

    [XmlElement(ElementName = "ProjectReference")]
    public List<ProjectReference> ProjectReference { get; set; }

    [XmlElement(ElementName = "BuildOutputInPackage")]
    public BuildOutputInPackage BuildOutputInPackage { get; set; }
}
