namespace BreakoutTests;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.GUI;
using Breakout.States;
using Breakout.Powers;
using DIKUArcade.Timers;
using System.IO;
using Breakout;



public class WideTests{
    private Player player;
    private Wide wide;
    private Vec2F pos;


    [SetUp] 
    public void Setup(){
        EventBus.ResetBus();
        player = new Player();
        pos = new Vec2F(0.45f, 0.135f);
        wide = new Wide(pos);
        EventBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
    }

    [Test]
    public void TestFall(){
        wide.Move();
        var Temp = pos + new Vec2F(0.0f, -0.005f); 
        Assert.That(wide.Shape.Position.Y,Is.EqualTo(Temp.Y));
    }

    [Test]
    public void TestGainSize(){
        var Expected = player.Shape.Extent.X*2;      
        wide.Move();    
        wide.PlayerCollision(player);
        EventBus.GetBus().ProcessEvents();
        Assert.That(Expected, Is.EqualTo(player.Shape.Extent.X));
    }

    [Test]
    public void TestsColision(){
        wide.Move();    
        wide.PlayerCollision(player);
        Assert.True(wide.IsDeleted());
    }

    [Test]
    public void TestMoveOOB() {
        player.DeleteEntity();
        for (int i = 0; i <= 100; i++){
            wide.Move();
        }
        Assert.True(wide.IsDeleted());
    }
}
