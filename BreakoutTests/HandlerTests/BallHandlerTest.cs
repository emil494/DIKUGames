namespace BreakoutTests;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout;
using Breakout.Blocks;
using DIKUArcade.GUI;
using System;

public class BallHandlerTest{
    [SetUp] 
    public void Setup(){
        Window.CreateOpenGLContext();

        handler = new BallHandler();

        player = new Player();

        block = new Block (
            new DynamicShape(
                new Vec2F(0.56f, 0.5f), new Vec2F(0.05f, 0.05f)), 
            new Image(Path.Combine("Assets", "Images", "blue-block.png")), false);

        container = new EntityContainer<Entity>();
    }
    private Player player;
    private Block block;
    private BallHandler handler;
    private EntityContainer<Entity> container;

    // [Test]
    // public void TestMoveSpeed() {
    //     Vec2F pos1 = ball.Shape.Position;
    //     ball.MoveBall();
    //     Vec2F pos2 = ball.Shape.Position;
    //     float dist = System.MathF.Round(System.MathF.Sqrt(
    //         System.MathF.Pow((pos2.X - pos1.X), 2.0f) + System.MathF.Pow((pos2.Y - pos1.Y), 2.0f)), 6);
    //     Assert.That(dist, Is.EqualTo(System.MathF.Round(Ball.MOVEMENT_SPEED, 6)));
    // }
}