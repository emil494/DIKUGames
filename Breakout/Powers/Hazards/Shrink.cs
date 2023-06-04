using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using System;
using System.IO;
using DIKUArcade.Timers;

namespace Breakout.Powers;

/// <summary>
/// Reduces the player class size, for 5 seconds
/// </summary>
public class Shrink : Entity, IHazard {
    public Shrink (Vec2F pos) : base (
        new DynamicShape(pos, new Vec2F(0.04f, 0.04f), new Vec2F(0.0f, -0.005f)), 
        new Image(Path.Combine("Assets", "Images", "SlimJim.png"))) {
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
    /// Sends a "start" message, and a delayed "end" message to the player class 
    /// to reduce the size of the player
    /// </summary>
    public void Apply(){
        EventBus.GetBus().RegisterEvent(
            new GameEvent {
                EventType = GameEventType.PlayerEvent,
                Message = "APPLY_POWERUP",
                StringArg1 = "SLIM",
                StringArg2 = "START"
            }
        );
        EventBus.GetBus().RegisterTimedEvent(
            (new GameEvent {
                EventType = GameEventType.PlayerEvent,
                Message = "APPLY_POWERUP",
                StringArg1 = "SLIM",
                StringArg2 = "STOP"
            }) ,
            (TimePeriod.NewSeconds(5))
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
                DeleteEntity();
            }
        }
    }
}