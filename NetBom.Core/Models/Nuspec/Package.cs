using System.Xml;
using System.Xml.Serialization;

namespace NetBom.Core.Models.Nuspec;

[XmlRoot(ElementName = "package")]
public class Package
{
    [XmlElement(ElementName = "metadata")]
    public Metadata Metadata { get; set; }

    [XmlAttribute(AttributeName = "xmlns")]
    public string Xmlns { get; set; }

    [XmlText]
    public string Text { get; set; }
}
