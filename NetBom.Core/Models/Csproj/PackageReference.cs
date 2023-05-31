using System.Xml.Serialization;

namespace NetBom.Core.Models.Csproj
{
    [XmlRoot(ElementName = "PackageReference")]
    public class PackageReference
    {
        [XmlAttribute(AttributeName = "Include")]
        public string Include { get; set; }

        [XmlAttribute(AttributeName = "Version")]
        public string Version { get; set; }
    }
}
