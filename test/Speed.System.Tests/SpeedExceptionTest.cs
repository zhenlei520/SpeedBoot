using GeneratorSource;
using Speed.SourceGenerator;

namespace Speed.System.Tests;

[TestClass]
public class SpeedExceptionTest
{
    [TestMethod]
    public void TestMethod1()
    {
        var exceptionMessage = SpeedExceptionGenerator.GetExceptionMessage();
        Assert.AreEqual(10, exceptionMessage.Count);
    }
}
