using System.Xml.Serialization;

namespace NetBom.Core.Models.Csproj
{
    [XmlRoot(ElementName = "Content")]
    public class Content
    {
        [XmlElement(ElementName = "CopyToOutputDirectory")]
        public string CopyToOutputDirectory { get; set; }

        [XmlAttribute(AttributeName = "Update")]
        public string Update { get; set; }
    }
}
