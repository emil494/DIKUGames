using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade;
using DIKUArcade.GUI;
using System;
using System.Collections.Generic;
using Breakout;

namespace Breakout.States;
public class StateHandler : IGameEventProcessor {
    public IGameState ActiveState { get; private set; }

    public StateHandler(){
        ActiveState = MainMenu.GetInstance();
    }

    private void SwitchState(StateType state) {
        switch (state){
            case StateType.GameRunning:
                if (typeof (MainMenu) == ActiveState.GetType()){
                    GameRunning.GetInstance().ResetState();
                }
                ActiveState = GameRunning.GetInstance();
                break;
            case StateType.GamePaused:
                ActiveState = GamePaused.GetInstance();
                break;
            case StateType.MainMenu:
                ActiveState = MainMenu.GetInstance();
                break;
            default:
                throw new ArgumentException("Invalid argument");
        }

    }

    public void ProcessEvent(GameEvent gameEvent) {
        switch (gameEvent.Message) {
            case "CHANGE_STATE":
                SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1));
                break;
        }
    }
}