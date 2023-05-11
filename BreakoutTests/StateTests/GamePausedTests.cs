using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using Breakout.States;
using System;
using System.Collections.Generic;

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
        eventBus.RegisterEvent(
            new GameEvent {
                EventType = GameEventType.GameStateEvent, 
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_PAUSED"
            }
        );
        eventBus.ProcessEventsSequentially();
    }

    [Test]
    public void TestContinue() {
        stateHandler.ActiveState.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Enter);
        eventBus.ProcessEventsSequentially();
        Assert.That(stateHandler.ActiveState, Is.InstanceOf<GameRunning>());
    }
}