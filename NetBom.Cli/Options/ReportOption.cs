﻿using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetBom.Core.Services;
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
    /// Gets or sets the Output.
    /// </summary>
    [Option('o', "output", HelpText = "Output path for report.", Default = "./.netbom")]
    public string Output { get; set; }

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
            var output = Path.GetFullPath(Output);
            logger.LogInformation("Processing file {file}...", source);
            Program.ServiceProvider.GetService<ReportService>().Create(source, output);

            logger.LogInformation("Report succesfull on {path}", output);
            return 0;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Something went wrong :(");
            return 1;
        }
    }
}
