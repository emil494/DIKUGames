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
public class GameOverTest {
    private StateHandler stateHandler;
    private GameOver gameOver;

    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();
        GameOver.GetInstance().ResetState();
        EventBus.ResetBus();
        stateHandler = new StateHandler();
        EventBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateHandler);

        EventBus.GetBus().RegisterEvent(
            new GameEvent{
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_OVER"
        });
        EventBus.GetBus().ProcessEventsSequentially();
        gameOver = GameOver.GetInstance();
    }

    [Test]
    public void TestToMainMenu() {
        stateHandler.ActiveState.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Enter);
        EventBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateHandler.ActiveState, Is.InstanceOf<MainMenu>());
    }

    [Test]
    public void TestPressDown() {
        var before = gameOver.GetActiveMenuButton();
        gameOver.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Down);
        var after = gameOver.GetActiveMenuButton();
        Assert.That(after, Is.EqualTo(before + 1));
    }

    [Test]
    public void TestPressDownEdge() {
        gameOver.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Down);
        var before = gameOver.GetActiveMenuButton();
        gameOver.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Down);
        var after = gameOver.GetActiveMenuButton();
        Assert.That(after, Is.EqualTo(before - 1));
    }

    [Test]
    public void TestPressUp() {
        gameOver.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Down);
        var before = gameOver.GetActiveMenuButton();
        gameOver.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Up);
        var after = gameOver.GetActiveMenuButton();
        Assert.That(after, Is.EqualTo(before - 1));
    }

    [Test]
    public void TestPressUpEdge() {
        var before = gameOver.GetActiveMenuButton();
        gameOver.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Up);
        var after = gameOver.GetActiveMenuButton();
        Assert.That(after, Is.EqualTo(before + 1));
    }
}