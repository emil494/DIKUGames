using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Math;
using System;
using System.Collections.Generic;

namespace Breakout.Powers;

public class EffectGenerator : IGameEventProcessor {
    private List<String> hazards;
    private List<String> powerUps;
    private EntityContainer<Entity> effects;
    public Random number;

    public EffectGenerator (){
        hazards = new List<String>();
        powerUps = new List<String>(){"Split", "ExtraLife", "More_time", "Split", "Split"};
        effects = new EntityContainer<Entity>();
        number = new Random();
    }

    private void CreatePowerUp(int num, Vec2F pos){
        switch(powerUps[num]) {
            case "Split":
                effects.AddEntity(new Split(pos));
                break;
            case "More_time":
                effects.AddEntity(new MoreTime(pos));
                break;
            case "Infinite":
                effects.AddEntity(new Infinite(pos));
                break;
            case "ExtraLife":
                effects.AddEntity(new ExtraLife(pos));
                break;
        }
    }

    private void CreateHazard(int num){

    }

    public void RenderEffects(){
        effects.RenderEntities();
    }

    public void UpdateEffects(Player player){
        effects.Iterate(effect => {
            if (effect is IEffect IF){
                IF.Move();
                IF.PlayerCollision(player);
            }
        });
    }

    public void Reset(){
        effects.ClearContainer();
    }

    public void ProcessEvent(GameEvent gameEvent) {
        switch (gameEvent.Message){
            case "ADD_POWERUP":
                float x = Convert.ToSingle(gameEvent.StringArg1);
                float y = Convert.ToSingle(gameEvent.StringArg2);
                CreatePowerUp(number.Next(5), new Vec2F(x, y));
                break;
            case "ADD_HAZARD":
                CreateHazard(number.Next(5));
                break;
            case "RESET_CONTAINER":
                Reset();
                break;
        }
    }
}