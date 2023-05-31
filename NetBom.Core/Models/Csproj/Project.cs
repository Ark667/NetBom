using System.Collections.Generic;
using System.Xml.Serialization;

namespace NetBom.Core.Models.Csproj;

[XmlRoot(ElementName = "Project")]
public class Project
{
    [XmlElement(ElementName = "PropertyGroup")]
    public PropertyGroup PropertyGroup { get; set; }

    [XmlElement(ElementName = "ItemGroup")]
    public List<ItemGroup> ItemGroup { get; set; }

    [XmlElement(ElementName = "Target")]
    public Target Target { get; set; }

    [XmlAttribute(AttributeName = "Sdk")]
    public string Sdk { get; set; }
}
