using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Blocks;
using DIKUArcade.GUI;

namespace BreakoutTests;

public class TestUnbreakableBlock {
    private UnbreakableBlock block;

    [SetUp]
    public void Setup(){
        Window.CreateOpenGLContext();
        block = new UnbreakableBlock (
            new DynamicShape(
                new Vec2F(0.0f, 0.0f), new Vec2F(1/12.0f, 1/25.0f)), 
            new Image(Path.Combine("Assets", "Images", "blue-block.png")));
    }

    [Test]
    public void TestDeleteBlock() {
        block.DeleteBlock();
        Assert.True(block.IsDeleted());
    }
}