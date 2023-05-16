using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using System;
using Breakout.Blocks;

namespace Breakout;

public class CollisionHandler {

    public CollisionHandler() {}

    public void BlockCollision (EntityContainer<Entity> blocks, Ball ball) {
        blocks.Iterate(block => {
            if (block is IBlock IB){
                CollisionData colData = CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape);
                if (colData.Collision) {
                    IB.LoseHealth();
                    if (colData.CollisionDir == CollisionDirection.CollisionDirUp || colData.CollisionDir == CollisionDirection.CollisionDirDown) {
                        ball.UpdateDirection((ball.Shape.AsDynamicShape()).Direction.X, -(ball.Shape.AsDynamicShape()).Direction.Y);
                    } else if (colData.CollisionDir == CollisionDirection.CollisionDirLeft || colData.CollisionDir == CollisionDirection.CollisionDirRight) {
                        ball.UpdateDirection(-(ball.Shape.AsDynamicShape()).Direction.X, (ball.Shape.AsDynamicShape()).Direction.Y);
                    }
                }
            }
        });
    }

    public void PlayerCollision(Player player, Ball ball) {
        CollisionData colData = CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.Shape);
        if (colData.Collision) {
            float xDir = (ball.Shape.AsDynamicShape()).Direction.X;
            float yDir = (ball.Shape.AsDynamicShape()).Direction.Y;

            //Update X-direction, according to where on the player the collision occured:
            float colX = player.Shape.Position.X + player.Shape.Extent.X/2.0f - ball.Shape.Position.X + ball.Shape.Extent.X/2.0f;
            float newXDir = xDir - colX/10.0f;

            //Make sure new X-direction dosent surpass the movement speed:
            if (newXDir >= Ball.MOVEMENT_SPEED) {
                newXDir = Ball.MOVEMENT_SPEED * 0.95f;
            }
            if (newXDir <= -Ball.MOVEMENT_SPEED) {
                newXDir = -Ball.MOVEMENT_SPEED * 0.95f;
            }

            //Make a Y-direction, so the ball has the correct movementspeed, and update ball direction:
            float newYDir = System.MathF.Sqrt(System.MathF.Pow(Ball.MOVEMENT_SPEED, 2.0f) - System.MathF.Pow(System.Math.Abs(newXDir), 2.0f));
            ball.UpdateDirection(newXDir, newYDir);
        }
    }
}