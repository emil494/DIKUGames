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
    /// <summary>
    /// Chooses (depending on num) which powerup to be created and where (depending on pos).
    /// </summary>
    /// <param name="num">Number between one and five which dictates the powerup.</param>
    /// <param name="pos">Position for where the powerup will be created.</param>
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

    /// <summary>
    /// Chooses (depending on num) which hazard to be created and where (depending on pos).
    /// </summary>
    /// <param name="num">Number between one and five which dictates the hazard.</param>
    /// <param name="pos">Position for where the hazard will be created.</param>
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

    /// <summary>
    /// Renders the effects
    /// </summary>
    public void RenderEffects(){
        effects.RenderEntities();
    }

    /// <summary>
    /// Updates effect on the player
    /// </summary>
    /// <param name="player">Entity which the effect is checked for collision with</param>
    public void UpdateEffects(Player player){
        effects.Iterate(effect => {
            if (effect is IEffect IF){
                IF.Move();
                IF.PlayerCollision(player);
            }
        });
    }

    /// <summary>
    /// Clears the container of all powers 
    /// </summary>
    public void Reset(){
        effects.ClearContainer();
    }
    /// <summary>
    /// Parameter of the message/command, e.g. sound: sound filename or identifier 
    /// </summary>
    /// <param name="gameEvent">The game event being processed.</param>
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

    /// <summary>
    /// Public getter for the effects container
    /// </summary>
    public EntityContainer<Entity> GetEffects(){
        return effects;
    }
}