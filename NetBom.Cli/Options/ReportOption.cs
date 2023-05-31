using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sbom.Core.Services;
using System;
using System.IO;

namespace NetBom.Cli.Options;

/// <summary>
/// Defines the <see cref="ReportOption" />.
/// </summary>
[Verb("report", HelpText = "Create SBOM report.")]
public class ReportOption
{
    /// <summary>
    /// Gets or sets the Source.
    /// </summary>
    [Option('s', "source", HelpText = "Source path to csproj file.")]
    public string Source { get; set; }

    /// <summary>
    /// The Report.
    /// </summary>
    /// <returns>The return value.<see cref="int"/> Zero if successful.</returns>
    public int Report()
    {
        var logger = Program.ServiceProvider.GetService<ILogger<ReportOption>>();

        try
        {
            var source = Path.GetFullPath(Source);
            logger.LogInformation("Processing file {file}...", source);
            Program.ServiceProvider.GetService<ReportService>().Create(source);

            logger.LogInformation("Succesful :D", Source);
            return 0;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Something went wrong :(");
            return 1;
        }
    }
}
