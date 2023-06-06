namespace BreakoutTests;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
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

        block = new UnbreakableBlock (
            new DynamicShape(
                new Vec2F(0.0f, 0.3f), new Vec2F(0.2f, 0.3f)), 
            new Image(Path.Combine("Assets", "Images", "blue-block.png")));

        container = new EntityContainer<Entity>();
        container.AddEntity(block);
    }
    private Player player;
    private UnbreakableBlock block;
    private BallHandler handler;
    private EntityContainer<Entity> container;

    [Test]
    public void TestAddBall() {
        handler.AddBall(new Vec2F(0.5f, 0.5f));
        int count = handler.GetBalls().CountEntities();
        Assert.True(count > 0);
    }

    [Test]
    public void TestInitializeGame() {
        handler.InitializeGame();
        EntityContainer<Ball> balls = handler.GetBalls();
        bool res = true;
        balls.Iterate(ball => {
            if (ball.Shape.Position.Y != 0.16f) {
                res = false;
            }
            if (ball.Shape.Position.X != 0.45f) {
                res = false;
            }
            });
        Assert.True(res);
    }

    //Tests that multiple balls can collide with player
    [Test]
    public void TestCollidePlayer() {
        handler.AddBall(new Vec2F(0.40f, 0.33f));
        handler.AddBall(new Vec2F(0.45f, 0.33f));
        handler.AddBall(new Vec2F(0.5f, 0.33f));
        handler.AddBall(new Vec2F(0.55f, 0.33f));
        handler.AddBall(new Vec2F(0.60f, 0.33f));

        EntityContainer<Ball> balls = handler.GetBalls();
        balls.Iterate(ball => {ball.UpdateDirectionX(0.0f);});
        List<Ball> list = handler.GetBallsList();

        // Sets three balls with direction to collide with player
        list[1].UpdateDirectionY(-0.2f);
        list[2].UpdateDirectionY(-0.2f);
        list[3].UpdateDirectionY(-0.2f);
        handler.UpdateBalls(container, player);

        List<bool> listRes = new List<bool>{};
        balls.Iterate(ball => {
            if (ball.Shape.AsDynamicShape().Direction.Y > 0.0f) {
                listRes.Add(true);
            } else {
                listRes.Add(false);
            }
        });
        Assert.True(listRes.TrueForAll(ele => {return ele;}));
    }

    //Tests that multiple balls can collide with blocks
    [Test]
    public void TestCollideBlocks() {
        handler.AddBall(new Vec2F(0.22f, 0.35f));
        handler.AddBall(new Vec2F(0.22f, 0.4f));
        handler.AddBall(new Vec2F(0.22f, 0.45f));
        handler.AddBall(new Vec2F(0.22f, 0.5f));
        handler.AddBall(new Vec2F(0.22f, 0.55f));

        EntityContainer<Ball> balls = handler.GetBalls();
        balls.Iterate(ball => {ball.UpdateDirectionY(0.0f);});
        List<Ball> list = handler.GetBallsList();

        // Sets three balls with direction to collide with block
        list[0].UpdateDirectionX(-0.01f);
        list[1].UpdateDirectionX(-0.01f);
        list[2].UpdateDirectionX(-0.01f);
        list[3].UpdateDirectionX(-0.01f);
        list[4].UpdateDirectionX(-0.01f);
        handler.UpdateBalls(container, player);
        List<bool> listRes = new List<bool>{};
        balls.Iterate(ball => {
            if (ball.Shape.AsDynamicShape().Direction.X > 0.0f) {
                listRes.Add(true);
            } else {
                listRes.Add(false);
            }
        });
        Assert.True(listRes.TrueForAll(ele => {return ele;}));
    }

    [Test]
    public void TestReset_BallHandler() {
        EventBus.ResetBus();
        EventBus.GetBus().Subscribe(GameEventType.StatusEvent, handler);

        handler.AddBall(new Vec2F(0.22f, 0.35f));
        handler.AddBall(new Vec2F(0.22f, 0.4f));
        handler.AddBall(new Vec2F(0.22f, 0.45f));
        handler.AddBall(new Vec2F(0.22f, 0.5f));
        handler.AddBall(new Vec2F(0.22f, 0.55f));

        var lvlhandler = new LevelHandler();
        lvlhandler.NewGame();

        EventBus.GetBus().ProcessEvents();
        var after = handler.GetBalls().CountEntities();

        Assert.That(after, Is.EqualTo(1));
    }
}