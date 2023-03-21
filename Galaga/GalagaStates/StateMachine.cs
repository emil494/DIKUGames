using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade;
using DIKUArcade.GUI;
using System;
using System.Collections.Generic;

namespace Galaga.GalagaStates;
public class StateMachine : DIKUGame, IGameEventProcessor {
    public IGameState ActiveState { get; private set; }

    public StateMachine(WindowArgs windowArgs) : base(windowArgs) {
        GalagaBus.GetBus().InitializeEventBus(new List<GameEventType> 
            { GameEventType.InputEvent, GameEventType.WindowEvent, GameEventType.PlayerEvent, GameEventType.GameStateEvent});
        GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        GalagaBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
        ActiveState = MainMenu.GetInstance();
        window.SetKeyEventHandler(ActiveState.HandleKeyEvent);
    }

    private void SwitchState(GameStateType stateType) {
        switch (stateType) {
            case GameStateType.GameRunning:
                ActiveState = GameRunning.GetInstance();
                window.SetKeyEventHandler(ActiveState.HandleKeyEvent);
                break;
            case GameStateType.GamePaused:
                //ActiveState = GamePaused.GetInstance();
                break;
            case GameStateType.MainMenu:
                ActiveState = MainMenu.GetInstance();
                window.SetKeyEventHandler(ActiveState.HandleKeyEvent);
                break;
            default:
                throw new ArgumentException("Invalid argument");
        }
    }

    public override void Render() {
        ActiveState.RenderState();
    }

    public override void Update() {
        ActiveState.UpdateState();
        GalagaBus.GetBus().ProcessEventsSequentially();
    }

    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.WindowEvent){
            switch (gameEvent.Message){
                case "ESCAPE":
                    if (ActiveState is GameRunning) {
                        SwitchState(GameStateType.GamePaused);
                    }
                    /*if (ActiveState.GetType() == GameStateType.GamePaused) {
                        SwitchState(GameStateType.GameRunning);
                    }*/
                    break;
                case "CLOSE_GAME":
                    window.CloseWindow();
                    break;
            }
        } else if (gameEvent.EventType == GameEventType.GameStateEvent){
            switch (gameEvent.Message){
                case "CHANGE_STATE":
                    switch (gameEvent.StringArg1){
                        case "GAME_RUNNING":
                            SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1));
                            break;
                    }
                    break;
            }
        }
    }
}