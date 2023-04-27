using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Blocks;

namespace BreakoutTests;

public class TestBlock {
    [SetUp]
    public void Setup(){
        Window.CreateOpenGLContext();
        block = new Block (
            new Vec2F((i, j), new Vec2F(1/12.0f, 1/25.0f)), 
            new Image(Path.Combine("Assets", "Images", )));
    }

    private Block block;

    [Test]
    public void Test1() {
        Assert.Pass();
    }
}