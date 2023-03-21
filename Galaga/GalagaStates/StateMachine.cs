using DIKUArcade.Events;
using DIKUArcade.State;
using System;

namespace Galaga.GalagaStates;
public class StateMachine : IGameEventProcessor {
    public IGameState ActiveState { get; private set; }

    public StateMachine() {
        GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        ActiveState = MainMenu.GetInstance();
    }

    private void SwitchState(GameStateType stateType) {
        switch (stateType) {
            case GameStateType.GameRunning:
                ActiveState = GameRunning.GetInstance();
                break;
            case GameStateType.GamePaused:
                ActiveState = GamePaused.GetInstance();
                break;
            case GameStateType.MainMenu:
                ActiveState = MainMenu.GetInstance();
                break;
            default:
                throw new ArgumentException("Invalid argument");
        }
    }

    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.WindowEvent){
            switch (gameEvent.Message){
                case "ESCAPE":
                    if (ActiveState.GetType() == GameStateType.GameRunning) {
                        SwitchState(GameStateType.GamePaused);
                    }
                    if (ActiveState.GetType() == GameStateType.GamePaused) {
                        SwitchState(GameStateType.GameRunning);
                    }
                    break;
            }
    }
}