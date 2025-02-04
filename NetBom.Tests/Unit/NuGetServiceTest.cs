using NetBom.Core.Services;
using NetBom.Tests.Helpers;

namespace NetBom.Tests.Unit;

[TestClass]
[TestCategory("CI")]
[TestCategory("Unit")]
public class NuGetServiceTest
{
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
