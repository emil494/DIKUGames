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
using Breakout.Powers;
using System;

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
        ballHandler.InitializeGame();

        health = new Health();
        EventBus.GetBus().Subscribe(GameEventType.StatusEvent, health);
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
        effectGenerator.UpdateEffects();
        ballHandler.UpdateBalls(lvlHandler.GetLevelBlocks(), player);
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
    public int GetFinalScore(){ //This functions helps GameWon display the final points
        return points.GetScore();
    }
}