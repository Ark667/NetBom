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
    /// Defines the _nugetPackagesPath.
    /// </summary>
    private string _nugetPackagesPath;

    /// <summary>
    /// Gets the Logger.
    /// </summary>
    public ILogger<NuGetService> Logger { get; private set; }

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
        if (string.IsNullOrEmpty(_nugetPackagesPath))
        {
            _nugetPackagesPath = Environment.GetEnvironmentVariable("NUGET_PACKAGES");
            if (!string.IsNullOrEmpty(_nugetPackagesPath))
            {
                Logger.LogDebug("NUGET_PACKAGES environment variable: {path}", _nugetPackagesPath);
                return _nugetPackagesPath;
            }

            _nugetPackagesPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".nuget",
                "packages"
            );
            Logger.LogDebug("Default NuGet path: {path}", _nugetPackagesPath);
        }

        return _nugetPackagesPath;
    }

    /// <inheritdoc/>
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
}
