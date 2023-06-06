using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Blocks;
using Breakout.Powers;
using Breakout;
using DIKUArcade.Events;

namespace BreakoutTests;

public class ReduceTimeTests {
    private EffectGenerator generator;


    [SetUp]
    public void Setup(){
        EventBus.ResetBus();
        
        EventBus.GetBus().Subscribe(GameEventType.StatusEvent, points);
    }

    [Test]
    public void TestGainPoint() {
        int start = points.GetScore();
        block.hp = 1;
        block.DeleteBlock();
        EventBus.GetBus().ProcessEventsSequentially();
        int after = points.GetScore();
        Assert.True(start < after);
    }
}