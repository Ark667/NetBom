using Microsoft.Extensions.Logging;
using NetBom.Core.Helpers;
using NetBom.Core.Models.Nuspec;
using System;
using System.IO;

namespace NetBom.Core.Services;

/// <summary>
/// Defines the <see cref="NuGetService" />.
/// </summary>
public class NuGetService : INuGetService
{
    /// <summary>
    /// Gets the Logger.
    /// </summary>
    public ILogger<NuGetService> Logger { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NuGetService"/> class.
    /// </summary>
    public NuGetService(ILogger<NuGetService> logger)
    {
        Logger = logger;
    }

    /// <summary>
    /// The GetPackagesPath.
    /// </summary>
    public string GetPackagesPath()
    {
        string nugetPackagesPath = Environment.GetEnvironmentVariable("NUGET_PACKAGES");
        if (!string.IsNullOrEmpty(nugetPackagesPath))
        {
            Logger.LogDebug("NUGET_PACKAGES environment variable: {path}", nugetPackagesPath);
            return nugetPackagesPath;
        }

        nugetPackagesPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ".nuget",
            "packages"
        );
        Logger.LogDebug("Default NuGet path: {path}", nugetPackagesPath);
        return nugetPackagesPath;
    }

    /// <summary>
    /// The GetNuspecPath.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="version"></param>
    public string GetNuspecPath(string name, string version)
    {
        string sourceNuspec = GetPackagesPath();
        var nuspecPath = Path.Combine(
            sourceNuspec,
            name.ToLower(),
            version ?? string.Empty,
            name.ToLower() + ".nuspec"
        );

        if (File.Exists(nuspecPath))
            return nuspecPath;

        return null;
    }

    /// <summary>
    /// The GetPackageInfo.
    /// </summary>
    /// <param name="nuspecPath"></param>
    /// <returns></returns>
    /// <remarks>
    /// This method creates a Package model where it adds parameters passed to the method and returns it,
    /// if the deserialized Xml contains the File type, it will also copy that File into the report.
    /// </remarks>
    public Package GetPackageInfo(string nuspecPath)
    {
        return XmlHelper.DeserializeXml<Package>(nuspecPath);
    }

    /// <inheritdoc/>
    public Package GetPackageInfo(string name, string version)
    {
        var nuspecPath = GetNuspecPath(name, version);
        return GetPackageInfo(nuspecPath);
    }

    private void CopyLicense(
        string contentInclude,
        string contentVersion,
        string licenseDirectory,
        Package nuGetPackage
    )
    {
        string sourceNuspec = GetPackagesPath();
        if (nuGetPackage.Metadata.License.Type == "file")
        {
            string sourceFileName = Path.Combine(
                sourceNuspec,
                contentInclude.ToLower(),
                contentVersion,
                nuGetPackage.Metadata.License.Text
            );

            // Copy license to output TODO esto no deberia hacerse aqui
            if (!string.IsNullOrEmpty(licenseDirectory))
            {
                string destFileName = Path.Combine(
                    licenseDirectory,
                    $"{contentInclude}.{nuGetPackage.Metadata.License.Text}".ToLower()
                );

                File.Copy(sourceFileName, destFileName, true);
            }
        }
    }
}
