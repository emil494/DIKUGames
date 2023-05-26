using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using System;
using System.IO;

namespace Breakout.Powers;

public class MoreTime : Entity, IPowerUp {
    public MoreTime (Vec2F pos) : base (
        new DynamicShape(pos, new Vec2F(0.04f, 0.04f), new Vec2F(0.0f, -0.005f)), 
        new Image(Path.Combine("Assets", "Images", "ball2.png"))) {
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
                EventType = GameEventType.StatusEvent,
                Message = "APPLY_POWERUP",
                StringArg1 = "ADD_TIME"
            }
        );
        DeleteEntity();
    }
    public void PlayerCollision(Player player){
        if (!IsDeleted()) {
            if (CollisionDetection.Aabb(Shape.AsDynamicShape(), player.Shape).Collision) {
                Apply();
            }
        }
    }
}