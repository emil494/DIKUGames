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

public class GameRunning : IGameState {
    private static GameRunning instance = null;
    private Player player;
    private LevelHandler lvlHandler;
    private CollisionHandler colHandler;
    private Ball ball;

    public static GameRunning GetInstance() {
        if (GameRunning.instance == null) {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.InitializeGameState();
        }
        return GameRunning.instance;
    }

    private void InitializeGameState(){
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.2f, 0.03f)),
            new Image(Path.Combine("Assets", "Images", "player.png")));

        EventBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);

        lvlHandler = new LevelHandler();
        lvlHandler.NextLevel();

        colHandler = new CollisionHandler();

        ball = new Ball(
            new DynamicShape(new Vec2F(0.44f, 0.17f), new Vec2F(0.06f, 0.06f)),
            new Image(Path.Combine("Assets", "Images", "ball.png")));
    }

    public void ResetState(){
        InitializeGameState();
    }

    public void UpdateState(){
        lvlHandler.UpdateLevel();
        player.Move();
        ball.MoveBall();
        colHandler.BlockCollision(LevelHandler.GetLevelBlocks(), ball);
        colHandler.PlayerCollision(player, ball);
    }
    
    public void RenderState(){
        player.RenderEntity();
        lvlHandler.RenderLevel();
        ball.RenderEntity();
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
                        EventType = GameEventType.GameStateEvent, 
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_PAUSED"
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