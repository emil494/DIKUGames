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



public class ShrinkTests{
    private Player player;
    private Shrink shrink;
    private Vec2F pos;


    [SetUp] 
    public void Setup(){
        Window.CreateOpenGLContext();
        //GameRunning.GetInstance().ResetState();
        EventBus.ResetBus();
        player = new Player();
        pos = new Vec2F(0.45f, 0.135f);
        shrink = new Shrink(pos);
        EventBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
    }

    [Test]
    public void TestFall(){
        shrink.Move();
        var Temp = pos + new Vec2F(0.0f, -0.005f); 
        Assert.That(shrink.Shape.Position.Y,Is.EqualTo(Temp.Y));
    }

    [Test]
    public void TestLoseSize(){
        var Expected = player.Shape.Extent.X/2;      
        shrink.Move();    
        shrink.PlayerCollision(player);
        EventBus.GetBus().ProcessEvents();
        Assert.That(Expected, Is.EqualTo(player.Shape.Extent.X));
    }

    [Test]
    public void TestsColision(){
        shrink.Move();    
        shrink.PlayerCollision(player);
        Assert.True(shrink.IsDeleted());
    }
}