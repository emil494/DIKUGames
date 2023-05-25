using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using System;

namespace Breakout.Blocks;

public class Block : Entity, IBlock {
    public int value {get; set;}
    public int hp {get; set;}
    public bool powerUp {get;}

    public Block(DynamicShape shape, IBaseImage image, bool power) : base(shape, image){
        powerUp = power; 
        value = 1;
        hp = 1;
    }

    // Methods are virtual to allow costumizability in subclasses

    /// <summary>
    /// Block loses one health. Calls DeleteBlock() if hp = 0 to delete block
    /// </summary>
    public void LoseHealth(){
        if (hp - 1 <= 0){
            hp -= 1;
            DeleteBlock();
        } else{
            hp -= 1;
        }
    }

    /// <summary>
    /// Deletes block and creates a StatusEvent to comunicate its points to the Point class. 
    /// </summary>
    public void DeleteBlock(){
        if (powerUp){
            //To do: Create powerUp through EventBus
        }
        DeleteEntity();
        EventBus.GetBus().RegisterEvent(
            new GameEvent {
                EventType = GameEventType.StatusEvent, 
                Message = "POINT_GAIN",
                StringArg1 = value.ToString()
            }
        );
        if (powerUp) {
            EventBus.GetBus().RegisterEvent(
            new GameEvent {
                EventType = GameEventType.InputEvent, 
                Message = "ADD_POWERUP",
                StringArg1 = Shape.Position.X.ToString(),
                StringArg2 = Shape.Position.Y.ToString()
            }
        );
        }
    }

    /// <summary>
    /// For updating a block in relation to other blocks in a EntityContainer. 
    /// Used for subclasses with special abilities 
    /// </summary>
    /// <param name="blocks"> An EntityContainer of blocks </param> 
    public void UpdateBlock(EntityContainer<Entity> blocks){}
}