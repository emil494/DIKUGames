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
public class GameRunningTests {
    private StateHandler stateHandler;
    private GameRunning gameRunning;

    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();
        GameRunning.GetInstance().ResetState();
        EventBus.ResetBus();
        stateHandler = new StateHandler();
        EventBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateHandler);

        EventBus.GetBus().RegisterEvent(
            new GameEvent {
                EventType = GameEventType.GameStateEvent, 
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_RUNNING"
            }
        );
        EventBus.GetBus().ProcessEventsSequentially();
        
        gameRunning = GameRunning.GetInstance();
    }

    [Test]
    public void TestEscape() {
        stateHandler.ActiveState.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Escape);
        EventBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateHandler.ActiveState, Is.InstanceOf<GamePaused>());
    }
}