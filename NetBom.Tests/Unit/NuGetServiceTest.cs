/* MyDance Zone S.L. © 2023 */

namespace NetBom.Tests.Unit;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetBom.Core.Services;
using NetBom.Tests.Helpers;

/// <summary>
/// Defines the <see cref="NuGetServiceTest" />.
/// </summary>
[TestClass]
[TestCategory("Unit")]
public class NuGetServiceTest
{
    /// <summary>
    /// The GetNugetPath.
    /// </summary>
    [TestMethod]
    public void GetNugetPath()
    {
        // Arrange
        var service = new NuGetService(new DebugLogger<NuGetService>());

        // Act
        var result = service.GetPackagesPath();

        // Assert
        Assert.IsTrue(Directory.Exists(result));
    }
}
