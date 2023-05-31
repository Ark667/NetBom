using System.Xml.Serialization;

namespace NetBom.Core.Models.Csproj
{
    [XmlRoot(ElementName = "BuildOutputInPackage")]
    public class BuildOutputInPackage
    {
        [XmlAttribute(AttributeName = "Include")]
        public string Include { get; set; }
    }
}
