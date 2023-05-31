using System.Xml.Serialization;

namespace NetBom.Core.Models.Nuspec
{
    [XmlRoot(ElementName = "license")]
    public class License
    {
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
