using Breakout;

namespace BreakoutTests;

public class FileReaderTest
{
    [SetUp]
    public void Setup() {
        reader = new FileReader();
        lvl1Path = Path.Combine("..", "..", "..", "Assets", "Levels", "level1.txt");
    }
    FileReader reader;
    string lvl1Path;

    [Test]
    public void TestReturnTrue() {
        bool result = reader.Read(lvl1Path);
        Assert.True(result);
    }

    [Test]
    public void TestReturnFalseMap() {
        bool result = reader.Read(Path.Combine("..", "..", "..", "Assets", "Levels", "levelNoMap.txt"));
        Assert.False(result);
    }

    [Test]
    public void TestReturnFalseMeta() {
        bool result = reader.Read(Path.Combine("..", "..", "..", "Assets", "Levels", "levelNoMeta.txt"));
        Assert.False(result);
    }

    [Test]
    public void TestReturnFalseLegend() {
        bool result = reader.Read(Path.Combine("..", "..", "..", "Assets", "Levels", "levelNoLegend.txt"));
        Assert.False(result);
    }

    [Test]
    public void TestMapField() {
        reader.Read(lvl1Path);
        string estResult = "-aaaaaaaaaa-";
        string result = reader.map[2];
        Assert.That(estResult, Is.EqualTo(result));
    }

    [Test]
    public void TestMetaField() {
        reader.Read(lvl1Path);
        string estResult = "300";
        string result = reader.meta["Time"];
        Assert.That(estResult, Is.EqualTo(result));
    }

    [Test]
    public void TestLegendField() {
        reader.Read(lvl1Path);
        string estResult = "orange-block.png";
        string result = reader.legend['1'];
        Assert.That(estResult, Is.EqualTo(result));
    }
}
