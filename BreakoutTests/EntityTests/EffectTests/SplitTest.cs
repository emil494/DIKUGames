namespace BreakoutTests;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Breakout;
using Breakout.Powers;
using DIKUArcade.GUI;
using System;

public class SplitTest{
    [SetUp]
    public void Setup(){
        Window.CreateOpenGLContext();
        EventBus.ResetBus();

        handler = new BallHandler();

        player = new Player();

        power = new Split(new Vec2F(0.5f, 0.135f));
        EventBus.GetBus().Subscribe(GameEventType.StatusEvent, handler);
    }
    private Player player;
    private BallHandler handler;
    private Split power;

    [Test]
    public void TestMovePower() {
        float exRes = power.Shape.Position.Y - 0.005f;
        power.Move();
        float yPos = power.Shape.Position.Y;
        Assert.That(yPos, Is.EqualTo(exRes));
    }

    [Test]
    public void TestOOB() {
        for (int i = 0; i < 36; i++) {
            power.Move();
        }
        Assert.True(power.IsDeleted());
    }

    [Test]
    public void TestCollision() {
        power.Move();
        power.PlayerCollision(player);
        Assert.True(power.IsDeleted());
    }

    [Test]
    public void TestSplit() {
        handler.AddBall(new Vec2F(0.5f, 0.5f));
        power.Move();
        power.PlayerCollision(player);
        EventBus.GetBus().ProcessEvents();
        int res = handler.GetBalls().CountEntities();
        Assert.That(res, Is.EqualTo(3));
    }
}