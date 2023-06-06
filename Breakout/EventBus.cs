using DIKUArcade.Events;
using System.Collections.Generic;

namespace Breakout;

public static class EventBus {
    private static GameEventBus eventBus;

    /// <summary>
    /// Creates the eventbus when called the first time, else returns the eventsbus
    /// </summary>
    /// <returns> The eventbus </returns>
    public static GameEventBus GetBus() {
        if (EventBus.eventBus is null){
            EventBus.eventBus = new GameEventBus();
            EventBus.eventBus.InitializeEventBus(new List<GameEventType> 
                { GameEventType.InputEvent, GameEventType.WindowEvent, 
                GameEventType.PlayerEvent, GameEventType.GameStateEvent, 
                GameEventType.StatusEvent});
        }
        return EventBus.eventBus;
    }

    //For testing purposes
    /// <summary>
    /// Resets the eventbus
    /// </summary>
    public static void ResetBus(){
        EventBus.eventBus = new GameEventBus();
        EventBus.eventBus.InitializeEventBus(new List<GameEventType> 
            { GameEventType.InputEvent, GameEventType.WindowEvent, 
            GameEventType.PlayerEvent, GameEventType.GameStateEvent, 
            GameEventType.StatusEvent});
    }
}