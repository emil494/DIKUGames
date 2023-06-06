using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using System.IO;
using System;

namespace Breakout;

/// <summary>
/// Keeps track of the players health and condition tied to it
/// </summary>
public class Health : IGameEventProcessor{
    private int hp;
    private List<Entity> hearts;
    private List<Entity> emptyHearts;

    public Health(){
        hp = 3;
        hearts = new List<Entity>{};
        emptyHearts = new List<Entity>{};
        for (int i = 1; i <= 5; i++) {
            hearts.Add(
            new Entity(
                new StationaryShape(
                    new Vec2F(1.0f-(((float)(i*5))*0.01f), 0.0f), new Vec2F(0.05f, 0.05f)),
                new Image(
                    Path.Combine("Assets", "Images", "heart_filled.png"))     
            )
        );
        }
    }

/// <summary>
/// Creates a gameevent if the player has run out of points
/// or changes the number of health points by -1
/// </summary>
    private void LoseHealth(){
        if (hp - 1 <= 0){
            EventBus.GetBus().RegisterEvent(
                new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_OVER"
                }
            );
        }
        else {
            hp -= 1;
        }
    }

/// <summary>
/// Renders the heart entities (the ones being displayed)
/// </summary>
    public void RenderHearts(){
        for (int i = 0; i < hp; i++){
            hearts[i].RenderEntity();
        }
    }
/// <summary>
/// Resets the amount of health to the standard of three
/// </summary>
    public void Reset(){
        hp = 3;
    }

    /// <summary>
    /// Parameter of the message/command, e.g. sound: sound filename or identifier 
    /// </summary>
    /// <param name="gameEvent">The game event being processed.</param>
    public void ProcessEvent(GameEvent gameEvent) {
        switch (gameEvent.Message){
            case "LOSE_HEALTH":
                LoseHealth();
                break;
            case "APPLY_POWERUP":
                switch (gameEvent.StringArg1){
                    case "EXTRALIFE":
                        if (hp <= 4) {
                            hp +=1;
                        }
                        break;
                }
                break;
            case "APPLY_HAZARD":
                switch (gameEvent.StringArg1) {
                    case "LOSELIFE":
                        LoseHealth();
                        break;
                }
                break;
        }
    }

/// <summary>
/// A public getter for accessing the amount of current health points
/// </summary>
/// <returns></returns>
//For testing purposes    
    public int GetHealth(){
        return hp;
    }
}