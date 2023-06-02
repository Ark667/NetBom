using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetBom.Cli.Options;
using NetBom.Core.Services;
using System;

namespace NetBom.Cli;

internal static class Program
{
    internal static IServiceProvider ServiceProvider { get; set; }

    /// <summary>
    /// The Main.
    /// </summary>
    /// <param name="args">The args<see cref="string[]"/>.</param>
    /// <returns>The <see cref="int"/>.</returns>
    public static int Main(string[] args)
    {
        try
        {
            // Configuration
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .AddJsonFile("appSettings.Development.json", optional: true)
                .Build();

            // Services
            var serviceCollection = new ServiceCollection()
                .AddTransient<INuGetService, NuGetService>()
                .AddTransient<ReportService>()
                .AddTransient<CsprojService>()
                .AddTransient<ProvisionService>()
                .AddScoped<IConfiguration>(_ => configuration);

            // Logging
            serviceCollection.AddLogging(configure =>
            {
                configure
                    .AddConfiguration(configuration.GetSection("Logging"))
                    .AddConsole()
                    .AddDebug();
            });

            ServiceProvider = serviceCollection.BuildServiceProvider();

            // Command line parser
            return Parser.Default
                .ParseArguments<ReportOption>(args)
                .MapResult(
                    (opts) => opts.Report(),
                    errs =>
                    {
                        return 1;
                    }
                );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }
}
