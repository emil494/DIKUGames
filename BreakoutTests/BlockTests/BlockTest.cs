using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Blocks;

namespace BreakoutTests;

public class TestBlock {
    [SetUp]
    public void Setup(){
        block = new Block (
            new StationaryShape(
                new Vec2F(0.0f, 0.0f), new Vec2F(1/12.0f, 1/25.0f)), 
            new Image(Path.Combine("Assets", "Images", "blue-block.png")), false);
    }

    private Block block;

    [Test]
    public void TestLoseHealth() {
        var start = block.hp;
        block.LoseHealth();
        var after = block.hp;
        Assert.Less(after, start);
    }

    [Test]
    public void TestDeleteBlock() {
        block.hp = 0;
        block.LoseHealth();
        Assert.True(block.IsDeleted());
    }
}