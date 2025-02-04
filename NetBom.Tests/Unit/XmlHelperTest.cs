using NetBom.Core.Helpers;
using NetBom.Core.Models.Csproj;
using NetBom.Tests.Properties;

namespace NetBom.Tests.Unit;

[TestClass]
[TestCategory("CI")]
[TestCategory("Unit")]
public class XmlHelperTest
{
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
