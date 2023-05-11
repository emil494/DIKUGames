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
public class MainMenuTests {
    private StateHandler stateHandler;

    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();
        stateHandler = new StateHandler();
        EventBus.GetBus();
        EventBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateHandler);
        EventBus.GetBus().RegisterEvent(
            new GameEvent {
                EventType = GameEventType.GameStateEvent, 
                Message = "CHANGE_STATE",
                StringArg1 = "MAIN_MENU"
            }
        );
        EventBus.GetBus().ProcessEventsSequentially();
    }

    [Test]
    public void TestNewGame() {
        stateHandler.ActiveState.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Enter);
        EventBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateHandler.ActiveState, Is.InstanceOf<GameRunning>());
    }
}