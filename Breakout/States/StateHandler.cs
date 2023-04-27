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
        ActiveState = IsRunning.GetInstance();
    }

    private void SwitchState(/*GameStateType stateType*/) {}

    public void ProcessEvent(GameEvent gameEvent) {}
}