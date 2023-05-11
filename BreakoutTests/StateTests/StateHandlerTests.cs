using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade;
using DIKUArcade.GUI;
using Breakout.States;
using System;
using System.Collections.Generic;

namespace BreakoutTests;

[TestFixture]
public class StateHandlerTests {
    private StateHandler stateHandler;
    private GameEventBus eventBus;

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
    public void TestInitialState() {
        Assert.That(stateHandler.ActiveState, Is.InstanceOf<MainMenu>());
    }

    [Test]
    public void TestEventGamePaused() {
        eventBus.RegisterEvent(
        new GameEvent{
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE_STATE",
            StringArg1 = "GAME_PAUSED"
        }
    );
    eventBus.ProcessEventsSequentially();
    Assert.That(stateHandler.ActiveState, Is.InstanceOf<GamePaused>());
    }

    [Test]
    public void TestEventGameRunning() {
        eventBus.RegisterEvent(
        new GameEvent{
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE_STATE",
            StringArg1 = "GAME_RUNNING"
        }
    );
    eventBus.ProcessEventsSequentially();
    Assert.That(stateHandler.ActiveState, Is.InstanceOf<GameRunning>());
    }

    [Test]
    public void TestEventMainMenu() {
        eventBus.RegisterEvent(
        new GameEvent{
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE_STATE",
            StringArg1 = "MAIN_MENU"
        }
    );
    eventBus.ProcessEventsSequentially();
    Assert.That(stateHandler.ActiveState, Is.InstanceOf<MainMenu>());
    }
}