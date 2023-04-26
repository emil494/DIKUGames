using DIKUArcade.Events;

namespace Breakout;

public static class EventBus {
    private static GameEventBus eventBus;
    
    public static GameEventBus GetBus() {
        return EventBus.eventBus ?? (EventBus.eventBus = new GameEventBus());
    }
}