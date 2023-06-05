using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout;
using Breakout.States;
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
        bh.AddBall(new Vec2F(0.0f, 0.0f));
        bh.GetBalls().Iterate(ball =>{
            ball.UpdateDirectionY(-0.1f);
        });
        var player = new Player();
        var container = new EntityContainer<Entity>();
        var before = hp.GetHealth();
        bh.UpdateBalls(container, player);
        EventBus.GetBus().ProcessEvents();
        var after = hp.GetHealth();
        Assert.That(after, Is.LessThan(before));
    }

    [Test]
    public void TestToGameOver() {
        var sh = new StateHandler();
        EventBus.GetBus().Subscribe(GameEventType.GameStateEvent, sh);
        var player = new Player();
        var container = new EntityContainer<Entity>();
        for (int i = 0; i <= 3; i++){
            bh.AddBall(new Vec2F(0.0f, 0.0f));
            bh.GetBalls().Iterate(ball =>{
                ball.UpdateDirectionY(-0.1f);
            });
            bh.UpdateBalls(container, player);
            EventBus.GetBus().ProcessEvents();
        }
        Assert.That(sh.ActiveState, Is.InstanceOf<GameOver>());
    }
}
