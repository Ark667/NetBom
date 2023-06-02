using NetBom.Core.Models.Nuspec;

namespace NetBom.Core.Services;

public interface INuGetService
{
    string GetPackagesPath();

    /// <summary>
    /// The GetNuspecPath.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="version"></param>
    string GetNuspecPath(string name, string version);

    /// <summary>
    /// The GetPackageInfo.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    /// <remarks>
    /// This method creates a Package model where it adds parameters passed to the method and returns it,
    /// if the deserialized Xml contains the File type, it will also copy that File into the report.
    /// </remarks>
    Package GetPackageInfo(string name, string version);
}
