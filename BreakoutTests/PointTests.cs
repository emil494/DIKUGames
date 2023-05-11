using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Blocks;
using Breakout;
using DIKUArcade.Events;

namespace BreakoutTests;

public class PointTests {
    [SetUp]
    public void Setup(){

        EventBus.GetBus();
        block = new Block (
            new DynamicShape(
                new Vec2F(0.0f, 0.0f), new Vec2F(1/12.0f, 1/25.0f)), 
            new Image(Path.Combine("Assets", "Images", "blue-block.png")), false);
        points = new Points();
        EventBus.GetBus().Subscribe(GameEventType.StatusEvent, points);
    }

    private Points points;
    private Block block;

    [Test]
    public void TestGainPoint() {
        int start = points.GetScore();
        block.hp = 1;
        block.DeleteBlock();
        EventBus.GetBus().ProcessEventsSequentially();
        int after = points.GetScore();
        Assert.True(start < after);
    }

}