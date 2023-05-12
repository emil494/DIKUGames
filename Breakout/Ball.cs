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
}
