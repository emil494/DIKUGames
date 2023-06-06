namespace BreakoutTests;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using Breakout;
using Breakout.Blocks;
using Breakout.Powers;
using System;

public class EffectGeneratorTest {
    private EffectGenerator generator;

    [SetUp] 
    public void Setup(){
        Window.CreateOpenGLContext();
        EventBus.ResetBus();
        generator = new EffectGenerator();
        EventBus.GetBus().Subscribe(GameEventType.StatusEvent, generator);

    }

    [Test]
    public void TestReset_EffectsGenerator() {
        EventBus.GetBus().RegisterEvent(
                new GameEvent {
                    EventType = GameEventType.InputEvent, 
                    Message = "ADD_POWERUP",
                    StringArg1 = (0.5f).ToString(),
                    StringArg2 = (0.5f).ToString()
                }
            );
        EventBus.GetBus().ProcessEvents();

        var lvlhandler = new LevelHandler();
        lvlhandler.NewGame();
        EventBus.GetBus().ProcessEvents();
        var after = generator.GetEffects().CountEntities();

        Assert.That(after, Is.EqualTo(0));
    }
}