using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using System.IO;
using System;

namespace Breakout;

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

    public void LoseHealth(){
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

    public void RenderHearts(){
        for (int i = 0; i < hp; i++){
            hearts[i].RenderEntity();
        }
    }

    public void Reset(){
        hp = 3;
    }

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
}