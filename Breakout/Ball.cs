using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using System;
using Breakout.Blocks;

namespace Breakout;

public class Ball : Entity {
    private const float MOVEMENT_SPEED = 0.02f;
    private float yDir;
    private float xDir;
    private Random rand;

    public Ball (DynamicShape shape, IBaseImage image) : base(shape, image) {
        rand = new Random();
        yDir = (((float)rand.Next((int)(MOVEMENT_SPEED*1000.0f)))/1000.0f);
        xDir = System.MathF.Sqrt(System.MathF.Pow(MOVEMENT_SPEED, 2.0f) - System.MathF.Pow(yDir, 2.0f));
        UpdateDirection();
    }
    
    private void UpdateDirection() {
        (Shape.AsDynamicShape()).ChangeDirection(new Vec2F(xDir, yDir));
    }
    
    public void MoveBall() {
        //Vec2F nextPos = Shape.Position + (Shape.AsDynamicShape()).Direction;
        (Shape.AsDynamicShape()).Move();
        if (Shape.Position.Y > 1.0f) {
            yDir = -yDir;
            UpdateDirection();
        }
        if (Shape.Position.X < 0.0f || Shape.Position.X + Shape.Extent.X > 1.0f) {
            xDir = -xDir;
            UpdateDirection();
        }
        if (Shape.Position.Y <= 0.0f) {
            DeleteEntity();
        } else {
            (LevelHandler.GetLevelBlocks()).Iterate(block => {
                if ((CollisionDetection.Aabb(Shape.AsDynamicShape(), block.Shape)).Collision) {
                    block.LoseHealth();
                    //Change direction
                }
            });
        }
    }
}