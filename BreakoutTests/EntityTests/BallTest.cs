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
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.2f, 0.03f)),
            new Image(Path.Combine("Assets", "Images", "player.png")));
        
        ball = new Ball(
            new DynamicShape(new Vec2F(0.44f, 0.17f), new Vec2F(0.06f, 0.06f)),
            new Image(Path.Combine("Assets", "Images", "ball.png")));

        block = new Block (
            new DynamicShape(
                new Vec2F(0.0f, 0.0f), new Vec2F(1/12.0f, 1/25.0f)), 
            new Image(Path.Combine("Assets", "Images", "blue-block.png")), false);

        colHandler = new CollisionHandler();
    }
    private Player player;
    private Ball ball;
    private Block block;
    private CollisionHandler colHandler;

    [Test]
    public void TestMoveSpeed() {
        Vec2F pos1 = ball.Shape.Position;
        ball.MoveBall();
        Vec2F pos2 = ball.Shape.Position;
        float dist = System.MathF.Sqrt(
            System.MathF.Pow((pos1.X + pos2.X), 2.0f) + System.MathF.Pow((pos1.Y + pos2.Y), 2.0f));
        Console.WriteLine(dist);
    }
}