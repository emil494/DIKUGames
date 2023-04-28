using DIKUArcade.Events;
using System.Collections.Generic;

namespace Breakout;

public static class EventBus {
    private static GameEventBus eventBus;
    public static GameEventBus GetBus() {
        if (EventBus.eventBus is null){
            EventBus.eventBus = new GameEventBus();
            EventBus.eventBus.InitializeEventBus(new List<GameEventType> 
                { GameEventType.InputEvent, GameEventType.WindowEvent, 
                GameEventType.PlayerEvent, GameEventType.GameStateEvent});
        }
        return EventBus.eventBus;
    }
}