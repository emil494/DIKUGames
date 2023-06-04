using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using System;
using System.IO;

namespace Breakout.Powers;

/// <summary>
/// Reduces the time left in the current level
/// </summary>
public class ReduceTime : Entity, IHazard {
    public ReduceTime (Vec2F pos) : base (
        new DynamicShape(pos, new Vec2F(0.04f, 0.04f), new Vec2F(0.0f, -0.005f)), 
        new Image(Path.Combine("Assets", "Images", "clock-down.png"))) {
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
    /// Sends a message to the level class, to reduce the current time
    /// </summary>
    private void Apply(){
        EventBus.GetBus().RegisterEvent(
            new GameEvent {
                EventType = GameEventType.StatusEvent,
                Message = "APPLY_HAZARD",
                StringArg1 = "REDUCE_TIME"
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