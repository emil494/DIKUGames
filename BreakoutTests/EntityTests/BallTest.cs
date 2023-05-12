namespace BreakoutTests;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout;
using Breakout.Blocks;
using DIKUArcade.GUI;
using System;

public class BallTest{
    [SetUp] 
    public void Setup(){
        Window.CreateOpenGLContext();
        player = new Player(
            new DynamicShape(new Vec2F(0.5f, 0.46f), new Vec2F(0.2f, 0.03f)),
            new Image(Path.Combine("Assets", "Images", "player.png")));
        
        ball = new Ball(
            new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.05f, 0.05f)),
            new Image(Path.Combine("Assets", "Images", "ball.png")));

        block = new Block (
            new DynamicShape(
                new Vec2F(0.56f, 0.5f), new Vec2F(0.05f, 0.05f)), 
            new Image(Path.Combine("Assets", "Images", "blue-block.png")), false);

        container = new EntityContainer<Block>();

        colHandler = new CollisionHandler();
    }
    private Player player;
    private Ball ball;
    private Block block;
    private EntityContainer<Block> container;
    private CollisionHandler colHandler;

    [Test]
    public void TestMoveSpeed() {
        Vec2F pos1 = ball.Shape.Position;
        ball.MoveBall();
        Vec2F pos2 = ball.Shape.Position;
        float dist = System.MathF.Round(System.MathF.Sqrt(
            System.MathF.Pow((pos2.X - pos1.X), 2.0f) + System.MathF.Pow((pos2.Y - pos1.Y), 2.0f)), 6);
        Assert.That(dist, Is.EqualTo(System.MathF.Round(Ball.MOVEMENT_SPEED, 6)));
    }

    [Test]
    public void TestUpdateDirectionX() {
        ball.UpdateDirection(1.0f,2.0f);
        Vec2F exRes = new Vec2F(1.0f,2.0f); 
        Assert.That((ball.Shape.AsDynamicShape()).Direction.X, Is.EqualTo(exRes.X));
    }

    [Test]
    public void TestUpdateDirectionY() {
        ball.UpdateDirection(1.0f,2.0f);
        Vec2F exRes = new Vec2F(1.0f,2.0f); 
        Assert.That((ball.Shape.AsDynamicShape()).Direction.Y, Is.EqualTo(exRes.Y));
    }

    [Test]
    public void TestBlockCollison() {
        ball.UpdateDirection(0.01f,0.0f);
        float dir = ball.Shape.AsDynamicShape().Direction.X;
        container.AddEntity(block);
        ball.MoveBall();
        colHandler.BlockCollision(container, ball);
        Assert.That(ball.Shape.AsDynamicShape().Direction.X, Is.EqualTo(-dir));
    }

    [Test]
    public void TestPlayerCollison() {
        ball.UpdateDirection(0.0f,-0.01f);
        ball.MoveBall();
        colHandler.PlayerCollision(player, ball);
        Assert.IsTrue(ball.Shape.AsDynamicShape().Direction.Y > 0.0f);
    }
}