using Breakout;

namespace BreakoutTests;

public class Tests
{
    [SetUp]
    public void Setup() {
        FileReader reader = new FileReader();
    }

    [Test]
    public void TestReturnTrue() {
        bool result = reader.Read("level1.txt");
        Assert.True(result);
    }

    [Test]
    public void TestReturnFalse() {
        bool result = reader.Read("levelNoMap.txt");
        Assert.False(result);
    }
}