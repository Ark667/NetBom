using System.Collections.Generic;
using System.Xml.Serialization;

namespace NetBom.Core.Models.Nuspec;

[XmlRoot(ElementName = "dependencies")]
public class Dependencies
{
    [XmlElement(ElementName = "group")]
    public List<Group> Group { get; set; }

    [XmlAttribute(AttributeName = "targetFramework")]
    public string TargetFramework { get; set; }
}
