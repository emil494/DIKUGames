using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout;
using DIKUArcade.Events;

namespace BreakoutTests;

public class HealthTests {
    private Health hp;
    private BallHandler bh;

    [SetUp]
    public void Setup(){
        EventBus.ResetBus();
        hp = new Health();
        bh = new BallHandler();
        EventBus.GetBus().Subscribe(GameEventType.StatusEvent, hp);
    }

    [Test]
    public void TestLoseHealth() {
        bh.InitializeGame();
        var player = new Player();
        var container = new EntityContainer<Entity>();
        var before = hp.GetHealth();
        for (int i = 0; i <= 99; i++){
            bh.UpdateBalls(container, player);
        }
        EventBus.GetBus().ProcessEvents();
        var after = hp.GetHealth();
        Assert.That(after, Is.LessThan(before));
    }
}
