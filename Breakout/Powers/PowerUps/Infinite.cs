using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using DIKUArcade.Timers;
using System;
using System.IO;

namespace Breakout.Powers;

/// <summary>
/// Spawns a new ball every second for 5 seconds, at the player's position
/// </summary>
public class Infinite : Entity, IPowerUp {
    public Infinite (Vec2F pos) : base (
        new DynamicShape(pos, new Vec2F(0.04f, 0.04f), new Vec2F(0.0f, -0.005f)), 
        new Image(Path.Combine("Assets", "Images", "InfinitePowerUp.png"))) {
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
    /// Sends a message to the player class to find the player position, 
    /// which comunicates to the BallHandler to spawn new balls at the given location
    /// </summary>
    private void Apply(){
        for (int i = 1; i <= 5; i++) {
            EventBus.GetBus().RegisterTimedEvent(
                (new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "FIND_POS_PLAYER",
                }), TimePeriod.NewSeconds(i));
        }
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