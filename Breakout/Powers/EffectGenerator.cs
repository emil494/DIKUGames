using DIKUArcade.Entities;
using DIKUArcade.Events;
using System;
using System.Collections.Generic;

namespace Breakout.Powers;

public class EffectGenerator : IGameEventProcessor {
    private List<IHazard> hazards;
    private List<IPowerUp> powerUps;
    private EntityContainer<Entity> effects;
    public Random number; 

    public EffectGenerator () {
        hazards = new List<IHazard>{};
        powerUps = new List<IPowerUp>{};
        effects = new EntityContainer<Entity>();
        number = new Random();
    }

    private void CreatePowerUp(int num){
        
    }

    private void CreateHazard(int num){

    }

    public void RenderEffects(){
        effects.RenderEntities();
    }

    public void UpdateEffects(){
        effects.Iterate(effect => {
            if (effect is IEffect IF){
                IF.Move();
            }
        });
    }

    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.InputEvent){
            switch (gameEvent.Message){
                case "ADD_POWERUP":
                    CreatePowerUp(number.Next(powerUps.Count));
                    break;
                case "ADD_HAZARD":
                    CreateHazard(number.Next(5));
                    break;
                default:
                    System.Console.WriteLine(
                        @$"Unknown message - EffectGenerator: 
                        {gameEvent.Message} is not a valid argument");
                    break;
            }
        }
    }
}