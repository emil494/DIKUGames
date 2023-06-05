using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using System;
using System.IO;

namespace Breakout.Powers;

/// <summary>
/// Adds additional time to the given level
/// </summary>
public class MoreTime : Entity, IPowerUp {
    public MoreTime (Vec2F pos) : base (
        new DynamicShape(pos, new Vec2F(0.04f, 0.04f), new Vec2F(0.0f, -0.005f)), 
        new Image(Path.Combine("Assets", "Images", "ball2.png"))) {
        }
    
    /// <summary>
    /// Moves the power up, and deletes it when it leaves the screen
    /// </summary>
    public void Move(){
        (Shape.AsDynamicShape()).Move();
        if (Shape.Position.Y + Shape.Extent.Y <= 0.0f) {
            DeleteEntity();
        }
    }

    /// <summary>
    /// Sends a message to the level class, to add to the current level time
    /// </summary>
    public void Apply(){
        EventBus.GetBus().RegisterEvent(
            new GameEvent {
                EventType = GameEventType.StatusEvent,
                Message = "APPLY_POWERUP",
                StringArg1 = "ADD_TIME"
            }
        );
        DeleteEntity();
    }

    /// <summary>
    /// Checks for collision between the player and the powerup, 
    /// and calls the Apply method if one occurs
    /// </summary>
    /// <param name="player">The player object, to check for collision with</param>
    public void PlayerCollision(Player player){
        if (!IsDeleted()) {
            if (CollisionDetection.Aabb(Shape.AsDynamicShape(), player.Shape).Collision) {
                Apply();
            }
        }
    }
}