using Microsoft.Extensions.Logging;
using NetBom.Core.Helpers;
using NetBom.Core.Models.Csproj;

namespace NetBom.Core.Services;

/// <summary>
/// Defines the <see cref="CsprojService" />.
/// </summary>
public class CsprojService
{
    /// <summary>
    /// Gets the Logger.
    /// </summary>
    public ILogger<CsprojService> Logger { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CsprojService"/> class.
    /// </summary>
    public CsprojService(ILogger<CsprojService> logger)
    {
        Logger = logger;
    }

    /// <summary>
    /// The GetProjectInfo.
    /// </summary>
    /// <param name="projectPath"></param>
    public Project GetProjectInfo(string projectPath)
    {
        return XmlHelper.DeserializeXml<Project>(projectPath);
    }
}
