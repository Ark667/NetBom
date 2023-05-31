using System.Xml.Serialization;

namespace NetBom.Core.Models.Nuspec
{
    [XmlRoot(ElementName = "metadata")]
    public class Metadata
    {
        [XmlElement(ElementName = "id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "version")]
        public string Version { get; set; }

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "authors")]
        public string Authors { get; set; }

        [XmlElement(ElementName = "requireLicenseAcceptance")]
        public bool RequireLicenseAcceptance { get; set; }

        [XmlElement(ElementName = "license")]
        public License License { get; set; }

        [XmlElement(ElementName = "licenseUrl")]
        public string LicenseUrl { get; set; }

        [XmlElement(ElementName = "icon")]
        public string Icon { get; set; }

        [XmlElement(ElementName = "readme")]
        public string Readme { get; set; }

        [XmlElement(ElementName = "projectUrl")]
        public string ProjectUrl { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "releaseNotes")]
        public string ReleaseNotes { get; set; }

        [XmlElement(ElementName = "copyright")]
        public string Copyright { get; set; }

        [XmlElement(ElementName = "tags")]
        public string Tags { get; set; }

        [XmlElement(ElementName = "repository")]
        public Repository Repository { get; set; }

        [XmlElement(ElementName = "dependencies")]
        public Dependencies Dependencies { get; set; }
    }
}
