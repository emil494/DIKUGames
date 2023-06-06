using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using Breakout;
using Breakout.Blocks;
using Breakout.Powers;

namespace BreakoutTests;

public class TestBlock {
    private Block block;
    private EffectGenerator generator;

    [SetUp]
    public void Setup(){
        Window.CreateOpenGLContext();
        EventBus.ResetBus();
        block = new Block (
            new DynamicShape(
                new Vec2F(0.0f, 0.0f), new Vec2F(1/12.0f, 1/25.0f)), 
            new Image(Path.Combine("Assets", "Images", "blue-block.png")), false);
        generator = new EffectGenerator();
        EventBus.GetBus().Subscribe(GameEventType.InputEvent, generator);
    }

    [Test]
    public void TestLoseHealth() {
        block.hp = 2;
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

    [Test]
    public void TestPowerUp() {
        block = new Block (
            new DynamicShape(
                new Vec2F(0.0f, 0.0f), new Vec2F(1/12.0f, 1/25.0f)), 
            new Image(Path.Combine("Assets", "Images", "blue-block.png")), true);
        block.DeleteBlock();
        EventBus.GetBus().ProcessEvents();
        var count = generator.GetEffects().CountEntities();
        Assert.That(count, Is.EqualTo(1));
    }
}