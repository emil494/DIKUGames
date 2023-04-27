namespace galagaTests;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Math;
using Galaga;
using DIKUArcade.GUI;

public class PlayerTests{
    [SetUp] 
    public void Setup(){
        Window.CreateOpenGLContext();
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> 
            {GameEventType.PlayerEvent});
        eventBus.Subscribe(GameEventType.PlayerEvent, player);
    }
    private Player player;
    private GameEventBus eventBus;

    [Test]
    public void TestMoveUp(){
        var start = player.GetPosition();
        eventBus.RegisterEvent(
            new GameEvent {EventType = GameEventType.PlayerEvent, Message = "MOVE",
            StringArg1 = "UP"}
        );
        eventBus.ProcessEvents();
        player.Move();
        var temp = player.GetPosition();
        var OtherTemp = start + new Vec2F(0.0f, 0.01f); 
        Assert.That(temp.Y, Is.EqualTo(OtherTemp.Y));
    }

    [Test]
    public void TestMoveUpOOB(){
        for (var i = 0; i <= 79; i++){
            eventBus.RegisterEvent(
                new GameEvent {EventType = GameEventType.PlayerEvent, Message = "MOVE",
                StringArg1 = "UP"}
            );
            eventBus.ProcessEvents();
            player.Move();
        }
        var temp = player.GetPosition();
        eventBus.RegisterEvent(
            new GameEvent {EventType = GameEventType.PlayerEvent, Message = "MOVE",
            StringArg1 = "UP"}
        );
        eventBus.ProcessEvents();
        player.Move();
        var OtherTemp = player.GetPosition();
        Assert.That(temp.Y, Is.EqualTo(OtherTemp.Y));
    }

    [Test]

    public void TestMoveDownOOB(){
        for (var i = 0; i <= 9; i++){
            eventBus.RegisterEvent(
                new GameEvent {EventType = GameEventType.PlayerEvent, Message = "MOVE",
                StringArg1 = "DOWN"}
            );
            eventBus.ProcessEvents();
            player.Move();
        }
        var temp = player.GetPosition();
        eventBus.RegisterEvent(
            new GameEvent {EventType = GameEventType.PlayerEvent, Message = "MOVE",
            StringArg1 = "DOWN"}
        );
        eventBus.ProcessEvents();
        player.Move();
        var OtherTemp = player.GetPosition();
        Assert.That(temp.Y, Is.EqualTo(OtherTemp.Y));
    }

    [Test]
    public void TestMoveRight(){
        var start = player.GetPosition();
        eventBus.RegisterEvent(
            new GameEvent {EventType = GameEventType.PlayerEvent, Message = "MOVE",
            StringArg1 = "RIGHT"}
        );
        eventBus.ProcessEvents();
        player.Move();
        var temp = player.GetPosition();
        var OtherTemp = start + new Vec2F(0.01f, 0.0f); 
        Assert.That(temp.X, Is.EqualTo(OtherTemp.X));
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
        var temp = player.GetPosition();
        eventBus.RegisterEvent(
            new GameEvent {EventType = GameEventType.PlayerEvent, Message = "MOVE",
            StringArg1 = "RIGHT"}
        );
        eventBus.ProcessEvents();
        player.Move();
        var OtherTemp = player.GetPosition();
        Assert.That(temp.X, Is.EqualTo(OtherTemp.X));
    }

    [Test]
    public void TestMoveLeft(){
        var start = player.GetPosition();
        eventBus.RegisterEvent(
            new GameEvent {EventType = GameEventType.PlayerEvent, Message = "MOVE",
            StringArg1 = "LEFT"}
        );
        eventBus.ProcessEvents();
        player.Move();
        var temp = player.GetPosition();
        var OtherTemp = start + new Vec2F(-0.01f, 0.0f); 
        Assert.That(temp.X, Is.EqualTo(OtherTemp.X));
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
        var temp = player.GetPosition();
        eventBus.RegisterEvent(
            new GameEvent {EventType = GameEventType.PlayerEvent, Message = "MOVE",
            StringArg1 = "LEFT"}
        );
        eventBus.ProcessEvents();
        player.Move();
        var OtherTemp = player.GetPosition();
        Assert.That(temp.X, Is.EqualTo(OtherTemp.X));
    }
}