using Microsoft.Extensions.Logging;
using NetBom.Core.Helpers;
using NetBom.Core.Models.Csproj;
using NetBom.Core.Models.Json;
using NetBom.Core.Models.Nuspec;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace NetBom.Core.Services;

/// <summary>
/// Defines the <see cref="ReportService" />.
/// </summary>
public class ReportService
{
    /// <summary>
    /// Gets the NuGetService.
    /// </summary>
    public INuGetService NuGetService { get; }

    /// <summary>
    /// Gets the CsprojService.
    /// </summary>
    public CsprojService CsprojService { get; }

    /// <summary>
    /// Gets the Logger.
    /// </summary>
    public ILogger<ReportService> Logger { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportService"/> class.
    /// </summary>
    /// <param name="nuGetService">The libraryAnalyzer<see cref="INuGetService"/>.</param>
    public ReportService(
        INuGetService nuGetService,
        CsprojService csprojService,
        ILogger<ReportService> logger
    )
    {
        NuGetService = nuGetService;
        CsprojService = csprojService;
        Logger = logger;
    }

    /// <inheritdoc/>
    /// <remarks>
    /// This method creates a report from a csproj file passed as a parameter.
    /// The report contains three folders, where are the files used to create the json,
    /// containing the information of the dependencies used in the csproj.
    /// </remarks>
    public void Create(string source)
    {
        // Load paths
        string reportsPath = Path.GetFullPath(".netbom");
        CreateDirectory(reportsPath);
        string csprojDirectory = Path.Combine(reportsPath, ".csproj");
        CreateDirectory(csprojDirectory);
        string nuspecDirectory = Path.Combine(reportsPath, ".nuspec");
        CreateDirectory(nuspecDirectory);
        string licenseDirectory = Path.Combine(reportsPath, ".licence");
        CreateDirectory(licenseDirectory);

        Logger.LogInformation("Output path on {path}.", reportsPath);

        // Initialize report
        var sourceFileInfo = new FileInfo(source);
        var report = new Report()
        {
            ReportInfo = new ReportInfo()
            {
                Date = DateTime.Now.ToString(),
                Project = sourceFileInfo.Name
            },
            Tree = new List<Models.Json.Package>(),
            List = new List<Models.Json.Package>(),
        };

        // Load csproj XML
        Project project = CsprojService.GetProjectInfo(source);

        Logger.LogInformation("Project {source} loaded.", source);

        // Copy the csproj to report
        File.Copy(source, Path.Combine(csprojDirectory, sourceFileInfo.Name));

        // Generate dependecy tree
        foreach (var itemGroup in project.ItemGroup)
        {
            // Add project references to report
            foreach (var projectReference in itemGroup.ProjectReference)
                AddPackageToTree(
                    projectReference,
                    report,
                    sourceFileInfo,
                    csprojDirectory,
                    nuspecDirectory,
                    source
                );

            // Add package references to report
            foreach (var packageReference in itemGroup.PackageReference)
                AddPackageToTree(
                    packageReference,
                    report,
                    sourceFileInfo,
                    csprojDirectory,
                    nuspecDirectory
                );
        }

        // Generate dependecy list
        foreach (var item in report.Tree)
            AddPackageToListoFromTree(report.List, item);

        // Write report to output
        string sourceJson = Path.Combine(
            reportsPath,
            $"{Path.GetFileNameWithoutExtension(sourceFileInfo.Name)}.json"
        );
        File.WriteAllText(
            sourceJson,
            JsonSerializer.Serialize(report, new JsonSerializerOptions() { WriteIndented = true })
        );
        Logger.LogInformation("Report listTree {listTree}.", report.List.Count);
        Logger.LogInformation("Report finished {json}.", sourceJson);
    }

    /// <summary>
    /// The CreateDirectories.
    /// </summary>
    /// <remarks>
    /// Checks if a path exists, if not, it creates it.
    /// </remarks>
    private void CreateDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            Logger.LogInformation("Created {path}.", path);
        }
    }

    /// <summary>
    /// The AddPackageToTree.
    /// </summary>
    /// <remarks>
    /// This method adds a tree dependencies on the corresponding packageReference.
    /// </remarks>
    private void AddPackageToTree(
        PackageReference reference,
        Report report,
        FileInfo sourceFileInfo,
        string csprojDirectory,
        string nuspecDirectory
    )
    {
        string nuspecPath = AddNuspec(reference.Include, reference.Version, nuspecDirectory);

        Logger.LogInformation("Nuspec {path}.", nuspecPath);

        var packages = CreateDependencies(
            nuspecPath,
            sourceFileInfo.Name,
            csprojDirectory,
            nuspecDirectory
        );

        try
        {
            report.Tree.Add(
                new Models.Json.Package()
                {
                    Name = reference.Include,
                    Version = reference.Version,
                    Dependencies = packages.Count() > 0 ? packages : null,
                }
            );
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Could not process {package} for tree.", reference.Include);
        }
    }

    /// <summary>
    /// The AddPackageToTree.
    /// </summary>
    /// <remarks>
    /// This method creates the tree of a project.
    /// </remarks>
    private void AddPackageToTree(
        ProjectReference reference,
        Report report,
        FileInfo sourceFileInfo,
        string csprojDirectory,
        string nuspecDirectory,
        string source
    )
    {
        try
        {
            var report2 = new Report()
            {
                Tree = new List<Models.Json.Package>(),
                List = new List<Models.Json.Package>()
            };
            var fileInfo2 = new FileInfo(
                Path.Combine(sourceFileInfo.FullName, "..", reference.Include)
            );
            Project project = XmlHelper.DeserializeXml<Project>(fileInfo2.FullName);

            File.Copy(source, Path.Combine(csprojDirectory, fileInfo2.Name));

            foreach (var itemGroup in project.ItemGroup)
                foreach (var packageReference in itemGroup.PackageReference)
                    AddPackageToTree(
                        packageReference,
                        report2,
                        fileInfo2,
                        csprojDirectory,
                        nuspecDirectory
                    );

            report.Tree.Add(
                new Models.Json.Package() { Name = fileInfo2.Name, Dependencies = report2.Tree }
            );
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error adding package {package}.", reference.Include);
        }
    }

    private void AddPackageToListoFromTree(
        List<Models.Json.Package> list,
        Models.Json.Package package
    )
    {
        // Add current package to list
        AddPackageToList(list, package);

        // Recurse package dependencies
        if (package.Dependencies != null)
            foreach (var item in package.Dependencies)
                AddPackageToListoFromTree(list, item);
    }

    private void AddPackageToList(List<Models.Json.Package> list, Models.Json.Package package)
    {
        if (!list.Any(o => o.Name == package.Name && o.Version == package.Version))
        {
            if (!string.IsNullOrEmpty(package.Version))
            {
                Logger.LogInformation(
                    "Adding package {package} {version} to list.",
                    package.Name,
                    package.Version
                );
                var nuspecInfo = NuGetService.GetPackageInfo(package.Name, package.Version);

                list.Add(
                    new Models.Json.Package()
                    {
                        Name = package.Name,
                        Version = package.Version,
                        License = nuspecInfo.Metadata.LicenseUrl,
                        Authors = nuspecInfo.Metadata.Authors,
                        ProjectUrl = nuspecInfo.Metadata.ProjectUrl,
                        Copyright = nuspecInfo.Metadata.Copyright
                    }
                );
            }
        }
    }

    public List<Models.Json.Package> CreateDependencies(
        string nuspecPath,
        string csprojName,
        string csprojDirectory,
        string nuspecDirectory
    )
    {
        var csprojPath = Path.Combine(csprojDirectory, csprojName);
        var dependencies = new List<Models.Json.Package>();
        var nuGetPackage = XmlHelper.DeserializeXml<Models.Nuspec.Package>(nuspecPath);
        var project = XmlHelper.DeserializeXml<Project>(csprojPath);

        foreach (Group group in nuGetPackage.Metadata.Dependencies.Group)
        {
            if (group.TargetFramework == project.PropertyGroup.TargetFramework)
                foreach (Dependency dependency in group.Dependency)
                {
                    string newNuspecPath = AddNuspec(
                        dependency.Id,
                        dependency.Version,
                        nuspecDirectory
                    );

                    var packages = CreateDependencies(
                        newNuspecPath,
                        csprojName,
                        csprojDirectory,
                        nuspecDirectory
                    );

                    dependencies.Add(
                        new Models.Json.Package()
                        {
                            Name = dependency.Id,
                            Version = dependency.Version,
                            Dependencies = packages.Count > 0 ? packages : null
                        }
                    );
                }
        }

        return dependencies;
    }

    public string AddNuspec(string contentInclude, string contentVersion, string nuspecDirectory)
    {
        string sourceFileName = NuGetService.GetNuspecPath(contentInclude, contentVersion);
        string destFileName = Path.Combine(nuspecDirectory, contentInclude.ToLower() + ".nuspec");

        File.Copy(sourceFileName, destFileName, true);

        return sourceFileName;
    }
}
