using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.States;
using System.Collections.Generic;

namespace Breakout;

public class Game : DIKUGame, IGameEventProcessor {
    private StateHandler stateHandler;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        stateHandler = new StateHandler();
        EventBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateHandler);
        EventBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
        window.SetKeyEventHandler(HandleKeyEvent);
    }

    public override void Render() {
        stateHandler.ActiveState.RenderState();
    }

    public override void Update() {
        stateHandler.ActiveState.UpdateState();
        EventBus.GetBus().ProcessEventsSequentially();

    }

    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.WindowEvent){
            switch (gameEvent.Message){
                case "CLOSE_GAME":
                    window.CloseWindow();
                    break;
            }
        }
    }

    private void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (stateHandler.ActiveState != null){
            stateHandler.ActiveState.HandleKeyEvent(action, key);
        }
    }
}