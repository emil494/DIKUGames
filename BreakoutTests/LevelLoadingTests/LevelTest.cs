using DIKUArcade.Entities;
using DIKUArcade.Graphics;
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
    reader.Read(Path.Combine("..", "..", "..", "Assets", "Levels", "level2.txt"));
    lvl2 = new Level(reader.map, reader.meta, reader.legend);
    blocks = lvl2.GetBlocks();
    }
    FileReader reader;
    Level lvl2;
    EntityContainer<Entity> blocks;

    [Test]
    public void TestBlockNum() {
        int num = blocks.CountEntities();
        Assert.That(num, Is.EqualTo(72));
    }

    [Test]
    public void TestPowerUpTrue() {
        bool res = lvl2.PowerUp('i');
        Assert.True(res);
    }

    [Test]
    public void TestPowerUpFalse() {
        bool res = lvl2.PowerUp('j');
        Assert.False(res);
    }

    [Test]
    public void TestIsEmptyFalse() {
        bool res = lvl2.IsEmpty();
        Assert.False(res);
    }

    [Test]
    public void TestIsEmptyTrue() {
        blocks.Iterate(block => {
            block.DeleteEntity();
        });
        bool res = lvl2.IsEmpty();
        Assert.True(res);
    }
}