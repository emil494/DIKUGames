namespace BreakoutTests;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Math;
using Breakout;
using DIKUArcade.GUI;

public class PlayerTests{
    [SetUp] 
    public void Setup(){
        Window.CreateOpenGLContext();
        player = new Player();
        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> 
            {GameEventType.PlayerEvent});
        eventBus.Subscribe(GameEventType.PlayerEvent, player);
    }
    private Player player;
    private GameEventBus eventBus;

    [Test]
    public void TestMoveRight(){
        var start = player.Shape.Position;
        eventBus.RegisterEvent(
            new GameEvent {EventType = GameEventType.PlayerEvent, Message = "MOVE",
            StringArg1 = "RIGHT"}
        );
        eventBus.ProcessEvents();
        player.Move();
        var temp = player.Shape.Position;
        var OtherTemp = start + new Vec2F(0.03f, 0.0f); 
        Assert.That(temp.X, Is.EqualTo(OtherTemp.X));
    }

    [Test]
    public void TestStopMoveRight(){
        eventBus.RegisterEvent(
            new GameEvent {EventType = GameEventType.PlayerEvent, Message = "MOVE",
            StringArg1 = "RIGHT"}
        );
        var start = player.GetMoveRight();
        eventBus.RegisterEvent(
            new GameEvent {EventType = GameEventType.PlayerEvent, Message = "STOP_MOVE",
            StringArg1 = "RIGHT"}
        );
        eventBus.ProcessEvents();
        var temp = player.GetMoveRight();
        Assert.That(start - start, Is.EqualTo(temp));
    }

    [Test]
    public void TestMoveRightOOB(){
        for (var i = 0; i <= 44; i++){
            eventBus.RegisterEvent(
                new GameEvent {EventType = GameEventType.PlayerEvent, Message = "MOVE",
                StringArg1 = "RIGHT"}
            );
            eventBus.ProcessEvents();
            player.Move();
        }
        var temp = player.Shape.Position;
        eventBus.RegisterEvent(
            new GameEvent {EventType = GameEventType.PlayerEvent, Message = "MOVE",
            StringArg1 = "RIGHT"}
        );
        eventBus.ProcessEvents();
        player.Move();
        var OtherTemp = player.Shape.Position;
        Assert.That(temp.X, Is.EqualTo(OtherTemp.X));
    }

    [Test]
    public void TestMoveLeft(){
        var start = player.Shape.Position;
        eventBus.RegisterEvent(
            new GameEvent {EventType = GameEventType.PlayerEvent, Message = "MOVE",
            StringArg1 = "LEFT"}
        );
        eventBus.ProcessEvents();
        player.Move();
        var temp = player.Shape.Position;
        var OtherTemp = start + new Vec2F(-0.03f, 0.0f); 
        Assert.That(temp.X, Is.EqualTo(OtherTemp.X));
    }

    [Test]
    public void TestStopMoveLeft(){
        eventBus.RegisterEvent(
            new GameEvent {EventType = GameEventType.PlayerEvent, Message = "MOVE",
            StringArg1 = "LEFT"}
        );
        var start = player.GetMoveLeft();
        eventBus.RegisterEvent(
            new GameEvent {EventType = GameEventType.PlayerEvent, Message = "STOP_MOVE",
            StringArg1 = "LEFT"}
        );
        eventBus.ProcessEvents();
        var temp = player.GetMoveLeft();
        Assert.That(start - start, Is.EqualTo(temp));
    }

    [Test]
    public void TestMoveLeftOOB(){
        for (var i = 0; i <= 44; i++){
            eventBus.RegisterEvent(
                new GameEvent {EventType = GameEventType.PlayerEvent, Message = "MOVE",
                StringArg1 = "LEFT"}
            );
            eventBus.ProcessEvents();
            player.Move();
        }
        var temp = player.Shape.Position;
        eventBus.RegisterEvent(
            new GameEvent {EventType = GameEventType.PlayerEvent, Message = "MOVE",
            StringArg1 = "LEFT"}
        );
        eventBus.ProcessEvents();
        player.Move();
        var OtherTemp = player.Shape.Position;
        Assert.That(temp.X, Is.EqualTo(OtherTemp.X));
    }
}