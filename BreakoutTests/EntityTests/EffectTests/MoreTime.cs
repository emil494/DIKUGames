using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Blocks;
using Breakout.Powers;
using Breakout;
using DIKUArcade.Events;
using DIKUArcade.GUI;

namespace BreakoutTests;

public class MoreTimeTests {
    private MoreTime power;
    private Player player;
    private Level lvl;
    private FileReader reader;

    [SetUp]
    public void Setup(){
        Window.CreateOpenGLContext();
        EventBus.ResetBus();
        player = new Player();
        reader = new FileReader();
        reader.Read(Path.Combine("..", "..", "..", "Assets", "Levels", "levelf1.txt"));
        lvl = new Level(reader.map, reader.meta, reader.legend);
        power = new MoreTime(player.Shape.Position + 
            new Vec2F(0.0f, 0.005f) + new Vec2F(0.0f, player.Shape.Extent.Y));
        EventBus.GetBus().Subscribe(GameEventType.StatusEvent, lvl);
    }

    [Test]
    public void TestMove() {
        player.DeleteEntity();
        var start = power.Shape.Position.Y;
        power.Move();
        var after = power.Shape.Position.Y;
        Assert.That(after, Is.EqualTo(start + -0.005f));
    }

    [Test]
    public void TestMoveOOB() {
        player.DeleteEntity();
        for (int i = 0; i <= 38; i++){
            power.Move();
        }
        Assert.True(power.IsDeleted());
    }

    [Test]
    public void TestPlayerCollision() {
        power.Move();
        power.PlayerCollision(player);
        Assert.True(power.IsDeleted());
    }

    [Test]
    public void TestApply() {
        power.Move();
        power.PlayerCollision(player);
        var start = lvl.GetRemaningTime();
        EventBus.GetBus().ProcessEvents();
        var after = lvl.GetRemaningTime();
        Assert.That(after, Is.EqualTo(start + 30));
    }
}