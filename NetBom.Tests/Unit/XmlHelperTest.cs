/* MyDance Zone S.L. © 2023 */

namespace NetBom.Tests.Unit;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sbom.Core.Helpers;
using Sbom.Core.Models.Csproj;
using Sbom.Tests.Properties;

/// <summary>
/// Defines the <see cref="XmlHelperTest" />.
/// </summary>
[TestClass]
[TestCategory("Unit")]
public class XmlHelperTest
{
    /// <summary>
    /// The DeserializeXml.
    /// </summary>
    [TestMethod]
    public void DeserializeXml()
    {
        // Arrange
        var file = Path.GetTempFileName();
        File.WriteAllText(file, Resources._test1);

        // Act
        var result = XmlHelper.DeserializeXml<Project>(file);
        File.Delete(file);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(9, result.ItemGroup.Count);
    }
}
