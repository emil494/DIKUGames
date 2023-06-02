namespace BreakoutTests;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using Breakout;
using Breakout.States;

public class PlayerTests{
    private Player player;

    [SetUp] 
    public void Setup(){
        Window.CreateOpenGLContext();
        GameRunning.GetInstance().ResetState();
        EventBus.ResetBus();
        player = new Player();
        EventBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
    }

    [Test]
    public void TestMoveRight(){
        var start = player.Shape.Position;
        GameRunning.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Right);
        EventBus.GetBus().ProcessEvents();
        player.Move();
        var temp = player.Shape.Position;
        var OtherTemp = start + new Vec2F(0.03f, 0.0f); 
        Assert.That(temp.X, Is.EqualTo(OtherTemp.X));
    }

    [Test]
    public void TestStopMoveRight(){
        GameRunning.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Right);
        EventBus.GetBus().ProcessEvents();
        var start = player.GetMoveRight();
        GameRunning.GetInstance().HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Right);
        EventBus.GetBus().ProcessEvents();
        var temp = player.GetMoveRight();
        Assert.That(start - start, Is.EqualTo(temp));
    }

    [Test]
    public void TestMoveRightOOB(){
        for (var i = 0; i <= 44; i++){
            GameRunning.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Right);
            EventBus.GetBus().ProcessEvents();
            player.Move();
        }
        var temp = player.Shape.Position;
        GameRunning.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Right);
        EventBus.GetBus().ProcessEvents();
        player.Move();
        var OtherTemp = player.Shape.Position;
        Assert.That(temp.X, Is.EqualTo(OtherTemp.X));
    }

    [Test]
    public void TestMoveLeft(){
        var start = player.Shape.Position;
        GameRunning.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Left);
        EventBus.GetBus().ProcessEvents();
        player.Move();
        var temp = player.Shape.Position;
        var OtherTemp = start + new Vec2F(-0.03f, 0.0f); 
        Assert.That(temp.X, Is.EqualTo(OtherTemp.X));
    }

    [Test]
    public void TestStopMoveLeft(){
        GameRunning.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Left);
        EventBus.GetBus().ProcessEvents();
        var start = player.GetMoveLeft();
        GameRunning.GetInstance().HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Left);
        EventBus.GetBus().ProcessEvents();
        var temp = player.GetMoveLeft();
        Assert.That(start - start, Is.EqualTo(temp));
    }

    [Test]
    public void TestMoveLeftOOB(){
        for (var i = 0; i <= 44; i++){
            GameRunning.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Left);
            EventBus.GetBus().ProcessEvents();
            player.Move();
        }
        var temp = player.Shape.Position;
        GameRunning.GetInstance().HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Left);
        EventBus.GetBus().ProcessEvents();
        player.Move();
        var OtherTemp = player.Shape.Position;
        Assert.That(temp.X, Is.EqualTo(OtherTemp.X));
    }
}