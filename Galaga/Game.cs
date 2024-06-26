using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using Galaga.GalagaStates;
using System;
using System.Collections.Generic;

namespace Galaga;

public class Game : DIKUGame, IGameEventProcessor{
    private StateMachine stateMachine;

    public Game (WindowArgs windowArgs) : base(windowArgs) {
        stateMachine = new StateMachine();
        GalagaBus.GetBus().InitializeEventBus(new List<GameEventType> 
            { GameEventType.InputEvent, GameEventType.WindowEvent, 
            GameEventType.PlayerEvent, GameEventType.GameStateEvent});
        GalagaBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
        GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
        window.SetKeyEventHandler(HandleKeyEvent);
    }

    public override void Render() {
        stateMachine.ActiveState.RenderState();
    }

    public override void Update() {
        stateMachine.ActiveState.UpdateState();
        GalagaBus.GetBus().ProcessEventsSequentially();
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
        if (stateMachine.ActiveState != null){
            stateMachine.ActiveState.HandleKeyEvent(action, key);
        }
    }
}