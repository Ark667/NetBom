using System.Xml.Serialization;

namespace NetBom.Core.Models.Nuspec
{
    [XmlRoot(ElementName = "repository")]
    public class Repository
    {
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [XmlAttribute(AttributeName = "url")]
        public string Url { get; set; }

        [XmlAttribute(AttributeName = "commit")]
        public string Commit { get; set; }
    }
}
