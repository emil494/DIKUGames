using Breakout;

namespace BreakoutTests;

public class LevelHandlerTest
{
    [SetUp]
    FileReader reader;
    string lvl1Path;
    public void Setup() {
        reader = new FileReader();
        lvl1Path = Path.Combine("..", "..", "..", "Assets", "Levels", "level1.txt");
    }

    [Test]
    public void TestReturnTrue() {
        bool result = reader.Read(lvl1Path);
        Assert.True(result);
    }
}