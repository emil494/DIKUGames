using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using System;
using Breakout.Blocks;

namespace Breakout;

public class Ball : Entity {
    public const float MOVEMENT_SPEED = 0.02f;
    private Random rand;

    public Ball (DynamicShape shape, IBaseImage image) : base(shape, image) {
        rand = new Random();
        float xDir0 = (((float)rand.Next((int)((MOVEMENT_SPEED*0.49f)*1000.0f)))/1000.0f);
        float yDir0 = System.MathF.Sqrt(System.MathF.Pow(MOVEMENT_SPEED, 2.0f) - System.MathF.Pow(xDir0, 2.0f));
        UpdateDirection(xDir0, yDir0);
    }
    
    public void UpdateDirection(float xDir, float yDir) {
        (Shape.AsDynamicShape()).ChangeDirection(new Vec2F(xDir, yDir));
    }
    
    public void MoveBall() {
        //Vec2F nextPos = Shape.Position + (Shape.AsDynamicShape()).Direction;
        (Shape.AsDynamicShape()).Move();
        if (Shape.Position.Y + Shape.Extent.Y > 1.0f) {
            UpdateDirection((Shape.AsDynamicShape()).Direction.X, -(Shape.AsDynamicShape()).Direction.Y);
        }
        if (Shape.Position.X < 0.0f) {
            UpdateDirection(System.Math.Abs((Shape.AsDynamicShape()).Direction.X), (Shape.AsDynamicShape()).Direction.Y);
        }
        if (Shape.Position.X + Shape.Extent.X > 1.0f) {
            UpdateDirection(-System.Math.Abs((Shape.AsDynamicShape()).Direction.X), (Shape.AsDynamicShape()).Direction.Y);
        }
        if (Shape.Position.Y <= 0.0f) {
            DeleteEntity();
        }
    }

    public void PlayerCollision(Player player) {
        CollisionData colData = CollisionDetection.Aabb(Shape.AsDynamicShape(), player.Shape);
        if (colData.Collision) {
            float xDir = (Shape.AsDynamicShape()).Direction.X;
            float yDir = (Shape.AsDynamicShape()).Direction.Y;

            //Update X-direction, according to where on the player the collision occured:
            float colX = player.Shape.Position.X + player.Shape.Extent.X/2.0f - Shape.Position.X + Shape.Extent.X/2.0f;
            float newXDir = xDir - colX/10.0f;

            //Make sure new X-direction dosent surpass the movement speed:
            if (newXDir >= MOVEMENT_SPEED) {
                newXDir = MOVEMENT_SPEED * 0.95f;
            }
            if (newXDir <= -MOVEMENT_SPEED) {
                newXDir = -MOVEMENT_SPEED * 0.95f;
            }

            //Make a Y-direction, so the ball has the correct movementspeed, and update ball direction:
            float newYDir = System.MathF.Sqrt(System.MathF.Pow(MOVEMENT_SPEED, 2.0f) - System.MathF.Pow(System.Math.Abs(newXDir), 2.0f));
            UpdateDirection(newXDir, newYDir);
        }
    }

    public void BlockCollision (EntityContainer<Entity> blocks) {
        blocks.Iterate(block => {
            if (block is IBlock IB) {
                CollisionData colData = CollisionDetection.Aabb(Shape.AsDynamicShape(), block.Shape);
                if (colData.Collision) {
                    IB.LoseHealth();
                    if (colData.CollisionDir == CollisionDirection.CollisionDirUp || colData.CollisionDir == CollisionDirection.CollisionDirDown) {
                        UpdateDirection((Shape.AsDynamicShape()).Direction.X, -(Shape.AsDynamicShape()).Direction.Y);
                    } else if (colData.CollisionDir == CollisionDirection.CollisionDirLeft || colData.CollisionDir == CollisionDirection.CollisionDirRight) {
                        UpdateDirection(-(Shape.AsDynamicShape()).Direction.X, (Shape.AsDynamicShape()).Direction.Y);
                    }
                }
            }
        });
    }
}
