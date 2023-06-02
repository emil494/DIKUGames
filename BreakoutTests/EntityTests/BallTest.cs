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
        player = new Player();
        
        ball = new Ball(
            new Vec2F(0.5f, 0.5f));

        block = new Block (
            new DynamicShape(
                new Vec2F(0.56f, 0.5f), new Vec2F(0.05f, 0.05f)), 
            new Image(Path.Combine("Assets", "Images", "blue-block.png")), false);

        container = new EntityContainer<Entity>();
    }
    private Player player;
    private Ball ball;
    private Block block;
    private EntityContainer<Entity> container;

    [Test] //R.3
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
        ball.UpdateDirectionX(1.0f);
        ball.UpdateDirectionY(2.0f);
        Vec2F exRes = new Vec2F(1.0f,2.0f); 
        Assert.That((ball.Shape.AsDynamicShape()).Direction.X, Is.EqualTo(exRes.X));
    }

    [Test]
    public void TestUpdateDirectionY() {
        ball.UpdateDirectionX(1.0f);
        ball.UpdateDirectionY(2.0f);
        Vec2F exRes = new Vec2F(1.0f,2.0f); 
        Assert.That((ball.Shape.AsDynamicShape()).Direction.Y, Is.EqualTo(exRes.Y));
    }

    [Test] //R.1
    public void TestBallDeleteCondition() {
        List<bool> list = new List<bool>{};
        player.DeleteEntity();
        block.DeleteBlock();
        Vec2F startPos = new Vec2F(0.5f, 0.5f);
        //Upper bound
        ball.UpdateDirectionY(0.5f);
        ball.MoveBall();
        if (ball.Shape.AsDynamicShape().Direction.Y < 0) {
            list.Add(true);
        } else {
            list.Add(false);
        }
        //Right bound
        ball.Shape.SetPosition(startPos);
        ball.UpdateDirectionX(0.46f);
        ball.MoveBall();
        if (ball.Shape.AsDynamicShape().Direction.X < 0) {
            list.Add(true);
        } else {
            list.Add(false);
        }
        //Left bound
        ball.Shape.SetPosition(startPos);
        ball.UpdateDirectionX(-0.51f);
        ball.MoveBall();
        if (ball.Shape.AsDynamicShape().Direction.X > 0) {
            list.Add(true);
        } else {
            list.Add(false);
        }
        //Lower bound
        ball.Shape.SetPosition(startPos);
        ball.UpdateDirectionY(-0.5f);
        ball.MoveBall();
        if (ball.IsDeleted()) {
            list.Add(true);
        } else {
            list.Add(false);
        }
        Assert.True(list.TrueForAll(ele => {return ele;}));
    }

    [Test]
    public void TestBlockCollison() {
        ball.UpdateDirectionX(0.01f);
        float dir = ball.Shape.AsDynamicShape().Direction.X;
        container.AddEntity(block);
        ball.MoveBall();
        ball.BlockCollision(container);
        Assert.That(ball.Shape.AsDynamicShape().Direction.X, Is.EqualTo(-dir));
    }

    [Test] //R.2
    public void TestBlockIsHit() {
        ball.UpdateDirectionX(0.01f);
        float dir = ball.Shape.AsDynamicShape().Direction.X;
        container.AddEntity(block);
        ball.MoveBall();
        ball.BlockCollision(container);
        Assert.IsTrue(block.IsDeleted());
    }

    [Test] //R.4
    public void TestWontStickToSameTrajectory() {
        ball.UpdateDirectionY(-0.01f);
        ball.MoveBall();
        ball.PlayerCollision(player);
        Assert.AreNotEqual(ball.Shape.AsDynamicShape().Direction.X,0.0f);
    }

    [Test]
    public void TestPlayerCollison() {
        ball.UpdateDirectionY(-0.01f);
        ball.MoveBall();
        ball.PlayerCollision(player);
        Assert.IsTrue(ball.Shape.AsDynamicShape().Direction.Y > 0.0f);
    }

    [Test] //R.5
    public void TestLaunchingDirection() {
        bool res = true;
        if (ball.Shape.AsDynamicShape().Direction.Y < 0.0f) {
            res = false;
        }
        if (ball.Shape.AsDynamicShape().Direction.Y < ball.Shape.AsDynamicShape().Direction.X) {
            res = false;
        }
        Assert.IsTrue(res);
    }
}