using System;

namespace Breakout.States;

public enum StateType {
    GameRunning,
    GamePaused,
    MainMenu
}

public class StateTransformer {
    public static StateType TransformStringToState(string state) {
        switch (state) {
            case "GAME_RUNNING":
                return StateType.GameRunning;
            case "GAME_PAUSED":
                return StateType.GamePaused;
            case "MAIN_MENU":
                return StateType.MainMenu;
            default:
                throw new ArgumentException("Invalid argument");
        }
    }
}