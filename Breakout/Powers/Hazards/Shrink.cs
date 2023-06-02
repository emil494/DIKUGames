using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using System;
using System.IO;
using DIKUArcade.Timers;

namespace Breakout.Powers;

public class Shrink : Entity, IHazard {
    public Shrink (Vec2F pos) : base (
        new DynamicShape(pos, new Vec2F(0.04f, 0.04f), new Vec2F(0.0f, -0.005f)), 
        new Image(Path.Combine("Assets", "Images", "SlimJim.png"))) {
        }
    
    public void Move(){
        (Shape.AsDynamicShape()).Move();
        if (Shape.Position.Y + Shape.Extent.Y <= 0.0f) {
            DeleteEntity();
        }
    }
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
    public void PlayerCollision(Player player){
        if (!IsDeleted()) {
            if (CollisionDetection.Aabb(Shape.AsDynamicShape(), player.Shape).Collision) {
                Apply();
                DeleteEntity();
            }
        }
    }
}