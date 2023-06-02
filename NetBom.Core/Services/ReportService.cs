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
    /// Gets the ProvisionService.
    /// </summary>
    public ProvisionService ProvisionService { get; }

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
        ProvisionService provisionService,
        ILogger<ReportService> logger
    )
    {
        NuGetService = nuGetService;
        CsprojService = csprojService;
        Logger = logger;
        ProvisionService = provisionService;
    }

    /// <inheritdoc/>
    /// <remarks>
    /// This method creates a report from a csproj file passed as a parameter.
    /// The report contains three folders, where are the files used to create the json,
    /// containing the information of the dependencies used in the csproj.
    /// </remarks>
    public void Create(string source, string output)
    {
        // Load paths
        FileHelper.CreateDirectory(output, clean: true);
        Logger.LogInformation("Output path on {path}.", output);
        ProvisionService.Configure(output);

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

        ProvisionService.CopyCsproj(source);

        // Generate dependecy tree
        foreach (var itemGroup in project.ItemGroup)
        {
            // Add project references to report
            foreach (var projectReference in itemGroup.ProjectReference)
                AddPackageToTree(projectReference, report.Tree, source);

            // Add package references to report
            foreach (var packageReference in itemGroup.PackageReference)
                AddPackageToTree(packageReference, report.Tree, source);
        }

        // Generate dependecy list
        foreach (var item in report.Tree)
            AddPackageToListoFromTree(report.List, item);

        // Write report to output
        string sourceJson = Path.Combine(
            output,
            $"{Path.GetFileNameWithoutExtension(sourceFileInfo.Name)}.json"
        );
        File.WriteAllText(
            sourceJson,
            JsonSerializer.Serialize(report, new JsonSerializerOptions() { WriteIndented = true })
        );
        Logger.LogInformation("{packages} packages.", report.List.Count);
        Logger.LogInformation("Report finished {json}.", sourceJson);
    }

    /// <summary>
    /// The AddPackageToTree.
    /// </summary>
    /// <remarks>
    /// This method adds a tree dependencies on the corresponding packageReference.
    /// </remarks>
    private void AddPackageToTree(
        PackageReference reference,
        List<Models.Json.Package> tree,
        string source
    )
    {
        string nuspecPath = NuGetService.GetNuspecPath(reference.Include, reference.Version);

        Logger.LogInformation("Nuspec {path}.", nuspecPath);

        var packages = CreateDependencies(nuspecPath, source);

        try
        {
            tree.Add(
                new Models.Json.Package()
                {
                    Name = reference.Include,
                    Version = reference.Version,
                    Dependencies = packages.Any() ? packages : null,
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
        List<Models.Json.Package> tree,
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

            Project project = XmlHelper.DeserializeXml<Project>(source);

            foreach (var itemGroup in project.ItemGroup)
                foreach (var packageReference in itemGroup.PackageReference)
                    AddPackageToTree(packageReference, report2.Tree, source);

            tree.Add(
                new Models.Json.Package()
                {
                    Name = Path.GetFileName(source),
                    Dependencies = report2.Tree
                }
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

        void AddPackageToList(List<Models.Json.Package> list, Models.Json.Package package)
        {
            if (!list.Any(o => o.Name == package.Name && o.Version == package.Version))
            {
                if (!string.IsNullOrEmpty(package.Version))
                {
                    ProvisionService.CopyNuspec(
                        NuGetService.GetNuspecPath(package.Name, package.Version)
                    );
                    ProvisionService.CopyLicense(
                        NuGetService.GetPackageInfo(package.Name, package.Version),
                        NuGetService.GetPackagesPath()
                    );

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
                            License = new Models.Json.License()
                            {
                                Url = nuspecInfo.Metadata.LicenseUrl,
                                Type =
                                    nuspecInfo.Metadata.License.Type == "expression"
                                        ? nuspecInfo.Metadata.License.Text
                                        : "custom",
                            },
                            Authors = nuspecInfo.Metadata.Authors,
                            ProjectUrl = nuspecInfo.Metadata.ProjectUrl,
                            Copyright = nuspecInfo.Metadata.Copyright
                        }
                    );
                }
            }
        }
    }

    public List<Models.Json.Package> CreateDependencies(string nuspecPath, string source)
    {
        var dependencies = new List<Models.Json.Package>();
        var nuGetPackage = XmlHelper.DeserializeXml<Models.Nuspec.Package>(nuspecPath);
        var project = XmlHelper.DeserializeXml<Project>(source);

        foreach (Group group in nuGetPackage.Metadata.Dependencies.Group)
        {
            if (group.TargetFramework == project.PropertyGroup.TargetFramework)
                foreach (Dependency dependency in group.Dependency)
                {
                    var packages = CreateDependencies(
                        NuGetService.GetNuspecPath(dependency.Id, dependency.Version),
                        source
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
}
