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
using System.IO;
using Breakout;



public class ExtraLifeTests{
    private Player player;
    private Health health;
    private ExtraLife xtraLife;
    private Vec2F pos;

    [SetUp] 
    public void Setup(){
        Window.CreateOpenGLContext();
        GameRunning.GetInstance().ResetState();
        EventBus.ResetBus();
        player = new Player();
        EventBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
        pos = new Vec2F(0.45f, 0.135f);
        xtraLife = new ExtraLife(pos);
        health = new Health();
        EventBus.GetBus().Subscribe(GameEventType.StatusEvent, health);
        
        //EventBus.GetBus().Subscribe(GameEventType.StatusEvent, xtraLife);
    }

    [Test]
    public void TestFall(){
        xtraLife.Move();
        var Temp = pos + new Vec2F(0.0f, -0.005f); 
        Assert.That(xtraLife.Shape.Position.Y,Is.EqualTo(Temp.Y));
    }

    [Test]
    public void TestGainHealth(){
        xtraLife.Move();       
        xtraLife.PlayerCollision(player);
        EventBus.GetBus().ProcessEvents();
        Assert.That(health.GetHealth(),Is.EqualTo(4));
    }

    [Test]
    public void TestsColision(){
        xtraLife.Move();    
        xtraLife.PlayerCollision(player);
        Assert.True(xtraLife.IsDeleted());
    }

}
