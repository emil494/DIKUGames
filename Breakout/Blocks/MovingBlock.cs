using DIKUArcade.Physics;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using System.Collections.Generic;

namespace Breakout.Blocks;

/// <summary>
/// A moving block
/// </summary>
public class MovingBlock : Entity, IBlock {
    public int value {get; set;}
    public int hp {get; set;}
    public bool powerUp {get;}

    public MovingBlock(DynamicShape shape, IBaseImage image, bool power) : base(shape, image){
        value = 3;
        powerUp = power;
    }

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
    public void DeleteBlock() {
        DeleteEntity();
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
        EventBus.GetBus().RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.StatusEvent, 
                        Message = "POINT_GAIN",
                        StringArg1 = value.ToString()
                    }
                );
    }

    /// <summary>
    /// Updates the blocks movement in relation to blocks in the given EntityContainer
    /// </summary>
    public void UpdateBlock(EntityContainer<Entity> container){
        List<bool> list = new List<bool> {};

        //Adds true to above list if any collition between the moving block and any other block
        //false otherwise
        foreach (Entity block in container){
            if (block is IBlock IB && (CollisionDetection.Aabb(Shape.AsDynamicShape(), block.Shape)).Collision) {
                list.Add(true);
            } else {
                list.Add(false);
            }
        };

        //Checks for out of bounds and collition with other blocks
        if (list.Contains(true) || 
        Shape.Extent.X + Shape.Position.X + (Shape.AsDynamicShape()).Direction.X >= 1.0f ||
        Shape.Position.X + (Shape.AsDynamicShape()).Direction.X <= 0.0f) {
            (Shape.AsDynamicShape()).Direction *= -1.0f;
        } else {
            (Shape.AsDynamicShape()).Move();
        }
    }
}