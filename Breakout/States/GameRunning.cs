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
    private Ball ball;
    private Points points;
    private Health health;

    /// <summary>
    /// Returns instance of itself and creates itself if null
    /// </summary>
    /// <returns> Itself </returns>
    public static GameRunning GetInstance() {
        if (GameRunning.instance == null) {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.InitializeGameState();
        }
        return GameRunning.instance;
    }

    /// <summary>
    /// Initializes the state
    /// </summary>
    private void InitializeGameState(){
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.2f, 0.03f)),
            new Image(Path.Combine("Assets", "Images", "player.png")));

        EventBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);

        points = new Points();
        EventBus.GetBus().Subscribe(GameEventType.StatusEvent, points);

        lvlHandler = new LevelHandler();
        lvlHandler.NewGame();

        ball = new Ball(
            new DynamicShape(new Vec2F(0.45f, 0.16f), new Vec2F(0.04f, 0.04f)),
            new Image(Path.Combine("Assets", "Images", "ball.png")));

        health = new Health();
    }

    /// <summary>
    /// Resets the state
    /// </summary>
    public void ResetState(){
        InitializeGameState();
    }

    /// <summary>
    /// Updates selected objects in the state
    /// </summary>
    public void UpdateState(){
        lvlHandler.UpdateLevel();
        player.Move();
        ball.MoveBall();
        ball.BlockCollision(lvlHandler.GetLevelBlocks());
        ball.PlayerCollision(player);

    }
    
    /// <summary>
    /// Renders all objects in the state
    /// </summary>
    public void RenderState(){
        player.RenderEntity();
        lvlHandler.RenderLevel();
        ball.RenderEntity();
        points.RenderScore();
        health.RenderHearts();
    }
    
    /// <summary>
    /// Handles key inputs
    /// </summary>
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