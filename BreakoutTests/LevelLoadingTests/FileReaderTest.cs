using Breakout;

namespace BreakoutTests;

public class FileReaderTest
{
    [SetUp]
    public void Setup() {
        reader = new FileReader();
    }

    FileReader reader;

    [Test]
    public void TestReturnTrue() {
        bool result = reader.Read("level1.txt");
        Assert.True(result);
    }
