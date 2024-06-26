using System;

namespace Breakout.States;

public enum StateType {
    GameRunning,
    GamePaused,
    MainMenu,
    GameOver,
    GameWon
}

public class StateTransformer {
    /// <summary>
    /// Transfroms a string into a StateType
    /// </summary>
    /// <param name="state"> string to be turned into a StateType </param>
    /// <returns> The converted string as StateType </returns> 
    public static StateType TransformStringToState(string state) {
        switch (state) {
            case "GAME_RUNNING":
                return StateType.GameRunning;
            case "GAME_PAUSED":
                return StateType.GamePaused;
            case "MAIN_MENU":
                return StateType.MainMenu;
            case "GAME_OVER":
                return StateType.GameOver;
            case "GAME_WON":
                return StateType.GameWon;
            default:
                throw new ArgumentException("Invalid argument");
        }
    }
}