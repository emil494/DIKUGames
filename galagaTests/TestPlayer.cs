namespace galagaTests;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using Galaga;
using DIKUArcade.GUI;
public class Tests
{
    [SetUp] 
    public void Setup()
    {
        Window.CreateOpenGLContext();
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(@"Assets\Images\Player.png"));
        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> {GameEventType.PlayerEvent});
        eventBus.Subscribe(GameEventType.PlayerEvent, player);
    }
    private Player player;
    private GameEventBus eventBus;

    [Test]
    public void TestMoveUp()
    {
        var start = player.GetPosition();
        eventBus.RegisterEvent(
            new GameEvent {EventType = GameEventType.PlayerEvent, Message = "MOVE",
            StringArg1 = "UP"}
        );
        player.Move();
        player.Render();
        var temp = player.GetPosition();
        var OtherTemp = start + new Vec2F(0.0f, 0.01f); 
        Assert.AreEqual(temp,OtherTemp);
    }
}