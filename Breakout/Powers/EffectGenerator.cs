using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Math;
using System;
using System.Collections.Generic;

namespace Breakout.Powers;

public class EffectGenerator : IGameEventProcessor {
    private EntityContainer<Entity> effects;
    public Random number;

    public EffectGenerator (){
        effects = new EntityContainer<Entity>();
        number = new Random();
    }

    private void CreatePowerUp(int num, Vec2F pos){
        switch(num) {
            case 0:
                effects.AddEntity(new Split(pos));
                break;
            case 1:
                effects.AddEntity(new MoreTime(pos));
                break;
            case 2:
                effects.AddEntity(new Infinite(pos));
                break;
            case 3:
                effects.AddEntity(new ExtraLife(pos));
                break;
            case 4:
                effects.AddEntity(new Wide(pos));
                break;
        }
    }

    private void CreateHazard(int num, Vec2F pos){
        if (num == 1){
            switch(number.Next(3)) {
                case 0:
                    effects.AddEntity(new LoseLife(pos));
                    break;
                case 1:
                    effects.AddEntity(new ReduceTime(pos));
                    break;
                case 2:
                    effects.AddEntity(new Shrink(pos));
                    break;
            }
        }
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
                float Px = Convert.ToSingle(gameEvent.StringArg1);
                float Py = Convert.ToSingle(gameEvent.StringArg2);
                CreatePowerUp(number.Next(5), new Vec2F(Px, Py));
                break;
            case "ADD_HAZARD":
                float Hx = Convert.ToSingle(gameEvent.StringArg1);
                float Hy = Convert.ToSingle(gameEvent.StringArg2);
                CreateHazard(number.Next(12), new Vec2F(Hx, Hy));
                break;
            case "RESET_EFFECTS":
                Reset();
                break;
        }
    }

    public EntityContainer<Entity> GetEffects(){
        return effects;
    }
}