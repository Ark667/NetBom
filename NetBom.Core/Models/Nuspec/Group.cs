using System.Collections.Generic;
using System.Xml.Serialization;

namespace NetBom.Core.Models.Nuspec
{
    [XmlRoot(ElementName = "group")]
    public class Group
    {
        [XmlElement(ElementName = "dependency")]
        public List<Dependency> Dependency { get; set; }

        [XmlAttribute(AttributeName = "targetFramework")]
        public string TargetFramework { get; set; }
    }
}
