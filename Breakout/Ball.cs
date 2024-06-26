using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using System.IO;
using System;
using Breakout.Blocks;

namespace Breakout;

/// <summary>
/// An entity with a dynamic shape, 
/// the class contains method for moving along the screen and coliding with blocks and player
/// </summary>
public class Ball : Entity {
    public const float MOVEMENT_SPEED = 0.02f;
    private Random rand;

    public Ball (Vec2F pos) : base(new DynamicShape(pos, new Vec2F(0.04f, 0.04f)), new Image(Path.Combine("Assets", "Images", "ball.png"))) {
        rand = new Random();
        float xDir0 = (System.MathF.Pow((-1.0f), rand.Next(2)+1))*(((float)rand.Next((int)((MOVEMENT_SPEED*0.49f)*1000.0f)))/1000.0f);
        float yDir0 = System.MathF.Sqrt(System.MathF.Pow(MOVEMENT_SPEED, 2.0f) - System.MathF.Pow(xDir0, 2.0f));
        UpdateDirectionX(xDir0);
        UpdateDirectionY(yDir0);
    }
    
    /// <summary>
    /// Updates the X within the direction vector of the Ball's shape
    /// </summary>
    /// <param name="xDir">The new float to replace the direction vector's X value</param>
    public void UpdateDirectionX(float xDir) {
        (Shape.AsDynamicShape()).ChangeDirection(new Vec2F(xDir, (Shape.AsDynamicShape()).Direction.Y));
    }

    /// <summary>
    /// Updates the Y within the direction vector of the Ball's shape
    /// </summary>
    /// <param name="yDir">The new float to replace the direction vector's Y value</param>
    public void UpdateDirectionY(float yDir) {
        (Shape.AsDynamicShape()).ChangeDirection(new Vec2F((Shape.AsDynamicShape()).Direction.X, yDir));
    }
    
    /// <summary>
    /// Moves the ball using the DynsamicShape.Move method, 
    /// and collides with the boarders of the screen
    /// </summary>
    public void MoveBall() {
        (Shape.AsDynamicShape()).Move();
        if (Shape.Position.Y + Shape.Extent.Y > 1.0f) {
            UpdateDirectionY(-System.Math.Abs((Shape.AsDynamicShape()).Direction.Y));
        }
        if (Shape.Position.X < 0.0f) {
            UpdateDirectionX(System.Math.Abs((Shape.AsDynamicShape()).Direction.X));
        }
        if (Shape.Position.X + Shape.Extent.X > 1.0f) {
            UpdateDirectionX(-System.Math.Abs((Shape.AsDynamicShape()).Direction.X));
        }
        if (Shape.Position.Y + Shape.Extent.Y <= 0.0f) {
            DeleteEntity();
        }
    }

    /// <summary>
    /// Checks for collision with the player, and updates the ball's direction if a collision occurs
    /// </summary>
    /// <param name="player">The player object to check for collision with</param>
    public void PlayerCollision(Player player) {
        CollisionData colData = CollisionDetection.Aabb(Shape.AsDynamicShape(), player.Shape);
        if (colData.Collision) {
            float xDir = (Shape.AsDynamicShape()).Direction.X;
            float yDir = (Shape.AsDynamicShape()).Direction.Y;

            //Update X-direction, according to where on the player the collision occured:
            float colX = (player.Shape.Position.X + (player.Shape.Extent.X/2.0f)) - (Shape.Position.X + (Shape.Extent.X/2.0f));
            float newXDir = xDir - colX/10.0f;

            //Make sure new X-direction dosent surpass the movement speed:
            if (newXDir >= MOVEMENT_SPEED/2) {
                newXDir = MOVEMENT_SPEED/2f;
            }
            if (newXDir <= -MOVEMENT_SPEED/2) {
                newXDir = -MOVEMENT_SPEED/2;
            }

            //Make a Y-direction, so the ball has the correct movementspeed, and update ball direction:
            float newYDir = System.MathF.Sqrt(System.MathF.Pow(MOVEMENT_SPEED, 2.0f) - System.MathF.Pow(System.Math.Abs(newXDir), 2.0f));
            UpdateDirectionX(newXDir);
            UpdateDirectionY(newYDir);
        }
    }

    /// <summary>
    /// Checks for collision with the blocks, and updates the ball's direction if a collision occurs
    /// </summary>
    /// <param name="blocks">An entity container with the blocks</param>
    public void BlockCollision (EntityContainer<Entity> blocks) {
        blocks.Iterate(block => {
            if (block is IBlock IB) {
                CollisionData colData = CollisionDetection.Aabb(Shape.AsDynamicShape(), block.Shape);
                if (colData.Collision) {
                    IB.LoseHealth();
                    if (colData.CollisionDir == CollisionDirection.CollisionDirUp || colData.CollisionDir == CollisionDirection.CollisionDirDown) {
                        UpdateDirectionY(-(Shape.AsDynamicShape()).Direction.Y);
                    } else if (colData.CollisionDir == CollisionDirection.CollisionDirLeft || colData.CollisionDir == CollisionDirection.CollisionDirRight) {
                        UpdateDirectionX(-(Shape.AsDynamicShape()).Direction.X);
                    }
                }
            }
        });
    }
}
