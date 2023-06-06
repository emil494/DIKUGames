using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.State;
using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Timers;
using System.IO;
using Breakout;
using System;
using Breakout.Powers;

namespace Breakout.States;

public class GameRunning : IGameState {
    private static GameRunning instance = null;
    private Player player;
    private EffectGenerator effectGenerator;
    private LevelHandler lvlHandler;
    private Points points;
    private BallHandler ballHandler;
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
        player = new Player();
        EventBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);

        points = new Points();
        EventBus.GetBus().Subscribe(GameEventType.StatusEvent, points);

        lvlHandler = new LevelHandler();
        lvlHandler.NewGame();
        
        effectGenerator = new EffectGenerator();
        EventBus.GetBus().Subscribe(GameEventType.InputEvent, effectGenerator);
        
        ballHandler = new BallHandler();
        EventBus.GetBus().Subscribe(GameEventType.StatusEvent, ballHandler);
        ballHandler.InitializeGame();
        
        health = new Health();
        EventBus.GetBus().Subscribe(GameEventType.StatusEvent, health);
    }

    /// <summary>
    /// Resets the state
    /// </summary>
    public void ResetState(){
        player.Reset();
        effectGenerator.Reset();
        points.Reset();
        health.Reset();
        ballHandler.Reset();
        lvlHandler.NewGame();
    }

    /// <summary>
    /// Updates selected objects in the state
    /// </summary>
    public void UpdateState(){
        player.Move();
        effectGenerator.UpdateEffects(player);
        ballHandler.UpdateBalls(lvlHandler.GetLevelBlocks(), player);
        lvlHandler.UpdateLevel();
    }
    
    /// <summary>
    /// Renders all objects in the state
    /// </summary>
    public void RenderState(){
        player.RenderEntity();
        lvlHandler.RenderLevel();
        effectGenerator.RenderEffects();
        points.RenderScore();
        ballHandler.RenderBalls();
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

    /// <summary>
    /// Registers a game event depending on the keyinput.
    /// </summary>
    /// <param name="key">key input</param>
    private void KeyPress(KeyboardKey key) {
        switch (key){
            case KeyboardKey.Escape:
                EventBus.GetBus().RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.StatusEvent, 
                        Message = "PAUSE_GAME"
                    }
                );
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
            case KeyboardKey.Space:
                EventBus.GetBus().RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.PlayerEvent, 
                        Message = "SPACE"
                    }
                );
                break;
        }
    }

    /// <summary>
    /// Registers a game event bases on the key input to stop the previous game event.
    /// </summary>
    /// <param name="key">key input</param>
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
    
    /// <summary>
    /// A public getter to gain acces to the amount of final points.
    /// </summary>
    /// <returns>Public amount of points for displaying and testing.</returns>
    public int GetFinalScore(){ //This functions helps GameWon display the final points
        return points.GetScore();
    }
}