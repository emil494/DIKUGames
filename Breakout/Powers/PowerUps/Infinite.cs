using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using DIKUArcade.Timers;
using System;
using System.IO;

namespace Breakout.Powers;

public class Infinite : Entity, IPowerUp {
    public Infinite (Vec2F pos) : base (
        new DynamicShape(pos, new Vec2F(0.04f, 0.04f), new Vec2F(0.0f, -0.005f)), 
        new Image(Path.Combine("Assets", "Images", "InfinitePowerUp.png"))) {
        }
    
    public void Move(){
        (Shape.AsDynamicShape()).Move();
        if (Shape.Position.Y + Shape.Extent.Y <= 0.0f) {
            DeleteEntity();
        }
    }
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
    public void PlayerCollision(Player player){
        if (!IsDeleted()) {
            if (CollisionDetection.Aabb(Shape.AsDynamicShape(), player.Shape).Collision) {
                Apply();
                DeleteEntity();
            }
        }
    }
}