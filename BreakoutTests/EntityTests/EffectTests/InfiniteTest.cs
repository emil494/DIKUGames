namespace BreakoutTests;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Breakout;
using Breakout.Powers;
using DIKUArcade.GUI;
using System;

public class InfiniteTest{
    [SetUp]
    public void Setup(){
        Window.CreateOpenGLContext();
        EventBus.ResetBus();

        handler = new BallHandler();

        player = new Player();
        EventBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);

        power = new Infinite(new Vec2F(0.5f, 0.135f));
        EventBus.GetBus().Subscribe(GameEventType.StatusEvent, handler);
    }
    private Player player;
    private BallHandler handler;
    private Infinite power;

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
    public void TestInfinite() {
        power.Move();
        power.PlayerCollision(player);
        player.Shape.Position = new Vec2F(0.0f, 0.5f);
        System.Threading.Thread.Sleep(1000);
        EventBus.GetBus().ProcessEvents();
        EventBus.GetBus().ProcessEvents();
        List<Ball> balls = handler.GetBallsList();
        Ball ball = balls[0];
        float ballX = ball.Shape.Position.X;
        Assert.That(ballX, Is.EqualTo(0.1f));
    }
}