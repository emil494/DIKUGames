using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Blocks;
using Breakout.Powers;
using Breakout;
using DIKUArcade.Events;
using DIKUArcade.GUI;

namespace BreakoutTests;

public class ReduceTimeTests {
    private ReduceTime hazard;
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
        hazard = new ReduceTime(player.Shape.Position + new Vec2F(0.0f, 0.005f));
        EventBus.GetBus().Subscribe(GameEventType.StatusEvent, lvl);
    }

    [Test]
    public void TestMove() {
        player.DeleteEntity();
        var start = hazard.Shape.Position.Y;
        hazard.Move();
        var after = hazard.Shape.Position.Y;
        Assert.That(after, Is.EqualTo(start + -0.005f));
    }

    [Test]
    public void TestMoveOOB() {
        player.DeleteEntity();
        for (int i = 0; i <= 28; i++){
            hazard.Move();
        }
        Assert.True(hazard.IsDeleted());
    }

    [Test]
    public void TestPlayerCollision() {
        hazard.Move();
        Assert.True(hazard.IsDeleted());
    }
}