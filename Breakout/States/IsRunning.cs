using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.State;
using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.IO;
using Breakout;

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
        EventBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
        handler = new LevelHandler();
        handler.Initialize();
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
            case KeyboardKey.Escape:
                EventBus.GetBus().RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.WindowEvent, 
                        Message = "CLOSE_GAME"
                    }
                );
                break;
            case KeyboardKey.Left:
                EventBus.GetBus().RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.PlayerEvent, 
                        Message = "MOVE",
                        StringArg1 = "LEFT"
                    }
                );
                System.Console.WriteLine(1);
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
                System.Console.WriteLine("release");
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