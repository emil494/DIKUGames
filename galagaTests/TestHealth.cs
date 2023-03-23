namespace galagaTests;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga;
using DIKUArcade.GUI;

public class HealthTests{
    [SetUp] 
    public void Setup(){
        Window.CreateOpenGLContext();
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(@"Assets\Images\Player.png"));
        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> 
            {GameEventType.PlayerEvent});
        eventBus.Subscribe(GameEventType.PlayerEvent, player);
    }
    private Health health;
    private GameEventBus eventBus;
}