using DIKUArcade.Entities;
using DIKUArcade.GUI;
using Breakout;
using Breakout.Blocks;

namespace BreakoutTests;

public class LevelTest
{
    [SetUp]
    public void Setup() {
    Window.CreateOpenGLContext();
    reader = new FileReader();
    reader.Read(Path.Combine("..", "..", "..", "Assets", "Levels", "level1.txt"));
    lvl1 = new Level(reader.map, reader.meta, reader.legend);
    blocks = lvl1.GetBlocks();
    }
    FileReader reader;
    Level lvl1;
    EntityContainer<Block> blocks;

    [Test]
    public void TestBlockNum() {
        int num = blocks.CountEntities();
        Assert.That(num, Is.EqualTo(76));
    }
}