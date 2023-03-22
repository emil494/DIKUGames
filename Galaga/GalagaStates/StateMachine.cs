using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade;
using DIKUArcade.GUI;
using System;
using System.Collections.Generic;

namespace Galaga.GalagaStates;
public class StateMachine : IGameEventProcessor {
    public IGameState ActiveState { get; private set; }

    public StateMachine(){
        ActiveState = MainMenu.GetInstance();
    }

    private void SwitchState(GameStateType stateType) {
        switch (stateType) {
            case GameStateType.GameRunning:
                if (typeof (MainMenu) == ActiveState.GetType()){
                    GameRunning.GetInstance().ResetState();
                }
                ActiveState = GameRunning.GetInstance();
                break;
            case GameStateType.GamePaused:
                GamePaused.GetInstance().ResetState();
                ActiveState = GamePaused.GetInstance();
                break;
            case GameStateType.MainMenu:
                ActiveState = MainMenu.GetInstance();
                break;
            default:
                throw new ArgumentException("Invalid argument");
        }
    }

    public void Render() {
        ActiveState.RenderState();
    }

    public void Update() {
        ActiveState.UpdateState();
    }

    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.GameStateEvent){
            switch (gameEvent.Message){
                case "CHANGE_STATE":
                    switch (gameEvent.StringArg1){
                        case "GAME_RUNNING":
                            SwitchState(GameStateType.GameRunning);
                            break;
                        case "GAME_PAUSED":
                            SwitchState(GameStateType.GamePaused);
                            break;
                        case "MAIN_MENU":
                            SwitchState(GameStateType.MainMenu);
                            break;
                    }
                    break;
            }
        }
    }
}