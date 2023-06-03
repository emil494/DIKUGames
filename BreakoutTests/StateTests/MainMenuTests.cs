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
    private MainMenu mainMenu;

    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();
        MainMenu.GetInstance().ResetState();
        EventBus.ResetBus();
        stateHandler = new StateHandler();
        EventBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateHandler);
        EventBus.GetBus().RegisterEvent(
            new GameEvent {
                EventType = GameEventType.GameStateEvent, 
                Message = "CHANGE_STATE",
                StringArg1 = "MAIN_MENU"
            }
        );
        EventBus.GetBus().ProcessEventsSequentially();

        mainMenu = MainMenu.GetInstance();
    }

    [Test]
    public void TestNewGame() {
        stateHandler.ActiveState.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Enter);
        EventBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateHandler.ActiveState, Is.InstanceOf<GameRunning>());
    }

    [Test]
    public void TestPressDown() {
        var before = mainMenu.GetActiveMenuButton();
        mainMenu.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Down);
        var after = mainMenu.GetActiveMenuButton();
        Assert.That(after, Is.EqualTo(before + 1));
    }

    [Test]
    public void TestPressDownEdge() {
        mainMenu.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Down);
        var before = mainMenu.GetActiveMenuButton();
        mainMenu.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Down);
        var after = mainMenu.GetActiveMenuButton();
        Assert.That(after, Is.EqualTo(before - 1));
    }

    [Test]
    public void TestPressUp() {
        mainMenu.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Down);
        var before = mainMenu.GetActiveMenuButton();
        mainMenu.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Up);
        var after = mainMenu.GetActiveMenuButton();
        Assert.That(after, Is.EqualTo(before - 1));
    }

    [Test]
    public void TestPressUpEdge() {
        var before = mainMenu.GetActiveMenuButton();
        mainMenu.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.Up);
        var after = mainMenu.GetActiveMenuButton();
        Assert.That(after, Is.EqualTo(before + 1));
    }
}