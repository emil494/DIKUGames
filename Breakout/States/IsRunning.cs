using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.State;
using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.Graphics;

namespace Breakout.States;

public class IsRunning : IGameState {
    private static IsRunning instance = null;
    private Player player;
    private LevelHandler handler;

    public static IsRunning GetInstance() {
        if (IsRunning.instance == null) {
            IsRunning.instance = new IsRunning();
            IsRunning.instance.InitializeGameState();
        }
        return IsRunning.instance;
    }

    private void InitializeGameState(){
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.15f, 0.03f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
        handler = new LevelHandler();
        handler.Initialize();
        EventBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
    }

    public void ResetState(){}

    public void UpdateState(){
        player.Move();
    }
    
    public void RenderState(){
        player.RenderEntity();
        handler.RenderLevel();
    }
    
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key){
             switch (action){
            case KeyboardAction.KeyPress:
                KeyPress(key);
                break;
            case KeyboardAction.KeyRelease:
                KeyRelease(key);
                break;
        }
    }
    private void KeyPress(KeyboardKey key) {
        switch (key){
            case KeyboardKey.Left:
                EventBus.GetBus().RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.PlayerEvent, 
                        Message = "MOVE",
                        StringArg1 = "LEFT"
                    }
                );
                break;
            case KeyboardKey.Right:
                EventBus.GetBus().RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.PlayerEvent, 
                        Message = "MOVE",
                        StringArg1 = "RIGHT"
                    }
                );
                break;
        }
    }

    private void KeyRelease(KeyboardKey key) {
        switch (key){
            case KeyboardKey.Left:
                EventBus.GetBus().RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.PlayerEvent, 
                        Message = "STOP_MOVE",
                        StringArg1 = "LEFT"
                    }
                );
                break;
            case KeyboardKey.Right:
                EventBus.GetBus().RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.PlayerEvent, 
                        Message = "STOP_MOVE",
                        StringArg1 = "RIGHT"
                    }
                );
                break;
        }
    }
}