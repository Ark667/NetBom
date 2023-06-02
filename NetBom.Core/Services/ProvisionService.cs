using Microsoft.Extensions.Logging;
using NetBom.Core.Helpers;
using NetBom.Core.Models.Nuspec;
using System.IO;
using System.Net.Http;

namespace NetBom.Core.Services;

/// <summary>
/// Defines the <see cref="ProvisionService" />.
/// </summary>
public class ProvisionService
{
    /// <summary>
    /// Gets the CsprojDirectory.
    /// </summary>
    public string CsprojDirectory { get; private set; }

    /// <summary>
    /// Gets the NuspecDirectory.
    /// </summary>
    public string NuspecDirectory { get; private set; }

    /// <summary>
    /// Gets the LicenseDirectory.
    /// </summary>
    public string LicenseDirectory { get; private set; }

    /// <summary>
    /// Gets the Logger.
    /// </summary>
    public ILogger<ProvisionService> Logger { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProvisionService"/> class.
    /// </summary>
    /// <param name="logger">The logger<see cref="ILogger{ProvisionService}"/>.</param>
    public ProvisionService(ILogger<ProvisionService> logger)
    {
        Logger = logger;
    }

    /// <summary>
    /// The Configure.
    /// </summary>
    public void Configure(string output)
    {
        CsprojDirectory = Path.Combine(output, ".csproj");
        FileHelper.CreateDirectory(CsprojDirectory);
        NuspecDirectory = Path.Combine(output, ".nuspec");
        FileHelper.CreateDirectory(NuspecDirectory);
        LicenseDirectory = Path.Combine(output, ".license");
        FileHelper.CreateDirectory(LicenseDirectory);
    }

    /// <summary>
    /// The CopyCsproj.
    /// </summary>
    public void CopyCsproj(string sourcePath)
    {
        string outputPath = Path.Combine(
            CsprojDirectory,
            $"{Path.GetFileNameWithoutExtension(sourcePath)}.csproj.xml"
        );
        File.Copy(sourcePath, outputPath, true);
        Logger.LogInformation("{file} copied on {path}.", sourcePath, outputPath);
    }

    /// <summary>
    /// The CopyNuspec.
    /// </summary>
    public void CopyNuspec(string sourcePath)
    {
        string targetPath = Path.Combine(
            NuspecDirectory,
            $"{Path.GetFileName(sourcePath).ToLower()}.nuspec.xml"
        );

        File.Copy(sourcePath, targetPath, true);
        Logger.LogInformation("{file} copied on {path}.", sourcePath, targetPath);
    }

    /// <summary>
    /// The CopyLicense.
    /// </summary>
    public void CopyLicense(Package nuGetPackage, string nugetPackagesPath)
    {
        string outputPath = Path.Combine(
            LicenseDirectory,
            $"{nuGetPackage.Metadata.Title}.{nuGetPackage.Metadata.License.Text}".ToLower()
        );

        if (nuGetPackage.Metadata.License.Type == "file")
        {
            string sourcePath = Path.Combine(
                nugetPackagesPath,
                nuGetPackage.Metadata.Id.ToLower(),
                nuGetPackage.Metadata.Version,
                nuGetPackage.Metadata.License.Text
            );

            File.Copy(sourcePath, outputPath, true);
            Logger.LogInformation("{file} copied on {path}.", sourcePath, outputPath);
        }
        else
        {
            if (nuGetPackage.Metadata.License.Text == "custom")
            {
                using var httpClient = new HttpClient();
                File.WriteAllBytes(
                    outputPath,
                    httpClient.GetByteArrayAsync(nuGetPackage.Metadata.LicenseUrl).Result
                );
                Logger.LogInformation(
                    "License {url} downloaded on {path}.",
                    nuGetPackage.Metadata.LicenseUrl,
                    outputPath
                );
            }
        }
    }
}
