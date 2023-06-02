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
public class GamePausedTests {
    private StateHandler stateHandler;
    private GamePaused gamePaused;

    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();
        GamePaused.GetInstance().ResetState();
        EventBus.ResetBus();
        stateHandler = new StateHandler();
        EventBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateHandler);
        EventBus.GetBus().RegisterEvent(
            new GameEvent {
                EventType = GameEventType.GameStateEvent, 
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_PAUSED"
            }
        );
        EventBus.GetBus().ProcessEventsSequentially();
        
        gamePaused = GamePaused.GetInstance();
    }

    [Test]
    public void TestContinue() {
        stateHandler.ActiveState.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Enter);
        EventBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateHandler.ActiveState, Is.InstanceOf<GameRunning>());
    }

    [Test]
    public void TestToMainMenu() {
        stateHandler.ActiveState.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Down);
        stateHandler.ActiveState.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Enter);
        EventBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateHandler.ActiveState, Is.InstanceOf<MainMenu>());
    }

    [Test]
    public void TestPressDown() {
        var before = gamePaused.GetActiveMenuButton();
        gamePaused.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Down);
        var after = gamePaused.GetActiveMenuButton();
        Assert.That(after, Is.EqualTo(before + 1));
    }

    [Test]
    public void TestPressDownEdge() {
        gamePaused.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Down);
        var before = gamePaused.GetActiveMenuButton();
        gamePaused.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Down);
        var after = gamePaused.GetActiveMenuButton();
        Assert.That(after, Is.EqualTo(before - 1));
    }

    [Test]
    public void TestPressUp() {
        gamePaused.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Down);
        var before = gamePaused.GetActiveMenuButton();
        gamePaused.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Up);
        var after = gamePaused.GetActiveMenuButton();
        Assert.That(after, Is.EqualTo(before - 1));
    }

    [Test]
    public void TestPressUpEdge() {
        var before = gamePaused.GetActiveMenuButton();
        gamePaused.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Up);
        var after = gamePaused.GetActiveMenuButton();
        Assert.That(after, Is.EqualTo(before + 1));
    }
}