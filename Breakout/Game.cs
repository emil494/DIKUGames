using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Timers;
using Breakout.States;
using System.Collections.Generic;

namespace Breakout;

/// <summary>
/// Creates the game, handles events related to the game window and keybindings
/// </summary>
public class Game : DIKUGame, IGameEventProcessor {
    private StateHandler stateHandler;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        stateHandler = new StateHandler();
        EventBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateHandler);
        EventBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
        window.SetKeyEventHandler(HandleKeyEvent);
    }

    /// <summary>
    /// Renders the game
    /// </summary>
    public override void Render() {
        stateHandler.ActiveState.RenderState();
    }

    /// <summary>
    /// Updates the game
    /// </summary>
    public override void Update() {
        stateHandler.ActiveState.UpdateState();
        EventBus.GetBus().ProcessEventsSequentially();

    }

    /// <summary>
    /// Processes WindowEvents
    /// </summary>
    /// <param name="gameEvent"> WindowEvent to be processed </param>
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.WindowEvent){
            switch (gameEvent.Message){
                case "CLOSE_GAME":
                    window.CloseWindow();
                    break;
            }
        }
    }

    /// <summary>
    /// Delegates key inputs to the active state
    /// </summary>
    private void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (stateHandler.ActiveState != null){
            stateHandler.ActiveState.HandleKeyEvent(action, key);
        }
    }
}