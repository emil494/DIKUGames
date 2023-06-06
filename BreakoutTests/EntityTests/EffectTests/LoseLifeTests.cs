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


public class LoseLifeTests{
    private Player player;
    private Health health;
    private LoseLife minustraLife;
    private Vec2F pos;

    [SetUp] 
    public void Setup(){
        Window.CreateOpenGLContext();
        EventBus.ResetBus();
        player = new Player();
        EventBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
        pos = new Vec2F(0.45f, 0.135f);
        minustraLife = new LoseLife(pos);
        health = new Health();
        EventBus.GetBus().Subscribe(GameEventType.StatusEvent, health);
    }

    [Test]
    public void TestFall(){
        minustraLife.Move();
        var Temp = pos + new Vec2F(0.0f, -0.005f); 
        Assert.That(minustraLife.Shape.Position.Y,Is.EqualTo(Temp.Y));
    }

    [Test]
    public void TestLoseHealth(){
        minustraLife.Move();       
        minustraLife.PlayerCollision(player);
        EventBus.GetBus().ProcessEvents();
        Assert.That(health.GetHealth(),Is.EqualTo(2));
    }

    [Test]
    public void TestsColision(){
        minustraLife.Move();    
        minustraLife.PlayerCollision(player);
        Assert.True(minustraLife.IsDeleted());
    }

    [Test]
    public void TestMoveOOB() {
        player.DeleteEntity();
        for (int i = 0; i <= 100; i++){
            minustraLife.Move();
        }
        Assert.True(minustraLife.IsDeleted());
    }
}
