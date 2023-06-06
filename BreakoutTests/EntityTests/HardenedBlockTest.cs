using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using Breakout;
using Breakout.Blocks;
using Breakout.Powers;

namespace BreakoutTests;

public class TestHardenedBlock {
    private HardenedBlock block;
    private EffectGenerator generator;

    [SetUp]
    public void Setup(){
        Window.CreateOpenGLContext();
        EventBus.ResetBus();
        block = new HardenedBlock (
            new DynamicShape(
                new Vec2F(0.0f, 0.0f), new Vec2F(1/12.0f, 1/25.0f)), 
            new Image(Path.Combine("Assets", "Images", "blue-block.png")), false, 
            "blue-block.png");
        generator = new EffectGenerator();
        EventBus.GetBus().Subscribe(GameEventType.InputEvent, generator);
    }

    [Test]
    public void TestLoseHealth() {
        block.hp = 4;
        var start = block.hp;
        block.LoseHealth();
        var after = block.hp;
        Assert.That(after, Is.LessThan(start));
    }

    [Test]
    public void TestDamaged() {
        var start = block.Image;
        block.LoseHealth();
        var after = block.Image;
        Assert.That(after, Is.Not.EqualTo(start));
    }

    [Test]
    public void TestDeleteBlock() {
        block.hp = 0;
        block.LoseHealth();
        Assert.True(block.IsDeleted());
    }

    [Test]
    public void TestPowerUp() {
        block = new HardenedBlock (
            new DynamicShape(
                new Vec2F(0.0f, 0.0f), new Vec2F(1/12.0f, 1/25.0f)), 
            new Image(Path.Combine("Assets", "Images", "blue-block.png")), true, "blue-block.png");
        block.DeleteBlock();
        EventBus.GetBus().ProcessEvents();
        var count = generator.GetEffects().CountEntities();
        Assert.That(count, Is.EqualTo(1));
    }
}