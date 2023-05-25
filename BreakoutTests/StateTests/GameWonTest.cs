using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using Breakout.States;
using System;
using System.Collections.Generic;
using Breakout;


namespace BreakoutTests;

[TestFixture]
public class GameWonTest {
    private StateHandler stateHandler;

    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();
        stateHandler = new StateHandler();
        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> 
            {GameEventType.GameStateEvent});
        eventBus.Subscribe(GameEventType.GameStateEvent, stateHandler);
    }

    [Test]
    public void TestHasWon() {
        eventBus.RegisterEvent(
        new GameEvent{
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE_STATE",
            StringArg1 = "GAME_RUNNING"
        });
        eventBus.RegisterEvent(
            new GameEvent{
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_WON"
            }
        );
        EventBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateHandler.ActiveState, Is.InstanceOf<GameWon>());
    }
}