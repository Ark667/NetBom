using System.Xml.Serialization;

namespace NetBom.Core.Models.Csproj
{
    [XmlRoot(ElementName = "PropertyGroup")]
    public class PropertyGroup
    {
        [XmlElement(ElementName = "TargetFramework")]
        public string TargetFramework { get; set; }

        [XmlElement(ElementName = "RuntimeIdentifier")]
        public string RuntimeIdentifier { get; set; }

        [XmlElement(ElementName = "RuntimeFrameworkVersion")]
        public string RuntimeFrameworkVersion { get; set; }

        [XmlElement(ElementName = "TargetsForTfmSpecificBuildOutput")]
        public string TargetsForTfmSpecificBuildOutput { get; set; }

        [XmlElement(ElementName = "PreserveCompilationContext")]
        public string PreserveCompilationContext { get; set; }

        [XmlElement(ElementName = "AssemblyName")]
        public string AssemblyName { get; set; }

        [XmlElement(ElementName = "OutputType")]
        public string OutputType { get; set; }

        [XmlElement(ElementName = "PackageId")]
        public string PackageId { get; set; }

        [XmlElement(ElementName = "GenerateAssemblyTitleAttribute")]
        public string GenerateAssemblyTitleAttribute { get; set; }

        [XmlElement(ElementName = "GenerateAssemblyDescriptionAttribute")]
        public string GenerateAssemblyDescriptionAttribute { get; set; }

        [XmlElement(ElementName = "GenerateAssemblyConfigurationAttribute")]
        public string GenerateAssemblyConfigurationAttribute { get; set; }

        [XmlElement(ElementName = "GenerateAssemblyCompanyAttribute")]
        public string GenerateAssemblyCompanyAttribute { get; set; }

        [XmlElement(ElementName = "GenerateAssemblyProductAttribute")]
        public string GenerateAssemblyProductAttribute { get; set; }

        [XmlElement(ElementName = "GenerateAssemblyCopyrightAttribute")]
        public string GenerateAssemblyCopyrightAttribute { get; set; }

        [XmlElement(ElementName = "GenerateAssemblyVersionAttribute")]
        public string GenerateAssemblyVersionAttribute { get; set; }

        [XmlElement(ElementName = "GenerateAssemblyFileVersionAttribute")]
        public string GenerateAssemblyFileVersionAttribute { get; set; }

        [XmlElement(ElementName = "DocumentationFile")]
        public string DocumentationFile { get; set; }

        [XmlElement(ElementName = "Authors")]
        public string Authors { get; set; }

        [XmlElement(ElementName = "PublishWithAspNetCoreTargetManifest")]
        public string PublishWithAspNetCoreTargetManifest { get; set; }

        [XmlElement(ElementName = "VersionPrefix")]
        public string VersionPrefix { get; set; }

        [XmlElement(ElementName = "VersionSuffix")]
        public string VersionSuffix { get; set; }

        [XmlElement(ElementName = "IsPackable")]
        public string IsPackable { get; set; }
    }
}
