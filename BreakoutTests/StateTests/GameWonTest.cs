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
    private GameWon gameWon;

    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();
        GameWon.GetInstance().ResetState();
        EventBus.ResetBus();
        stateHandler = new StateHandler();
        EventBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateHandler);

        EventBus.GetBus().RegisterEvent(
            new GameEvent{
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_WON"
        });
        EventBus.GetBus().ProcessEventsSequentially();
        gameWon = GameWon.GetInstance();
    }

    [Test]
    public void TestToMainMenu() {
        stateHandler.ActiveState.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Enter);
        EventBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateHandler.ActiveState, Is.InstanceOf<MainMenu>());
    }

    [Test]
    public void TestPressDown() {
        var before = gameWon.GetActiveMenuButton();
        gameWon.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Down);
        var after = gameWon.GetActiveMenuButton();
        Assert.That(after, Is.EqualTo(before + 1));
    }

    [Test]
    public void TestPressDownEdge() {
        gameWon.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Down);
        var before = gameWon.GetActiveMenuButton();
        gameWon.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Down);
        var after = gameWon.GetActiveMenuButton();
        Assert.That(after, Is.EqualTo(before - 1));
    }

    [Test]
    public void TestPressUp() {
        gameWon.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Down);
        var before = gameWon.GetActiveMenuButton();
        gameWon.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Up);
        var after = gameWon.GetActiveMenuButton();
        Assert.That(after, Is.EqualTo(before - 1));
    }

    [Test]
    public void TestPressUpEdge() {
        var before = gameWon.GetActiveMenuButton();
        gameWon.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Up);
        var after = gameWon.GetActiveMenuButton();
        Assert.That(after, Is.EqualTo(before + 1));
    }
}