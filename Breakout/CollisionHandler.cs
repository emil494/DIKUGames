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

    public void BlockCollision (EntityContainer<Block> blocks, Ball ball) {
        blocks.Iterate(block => {
                CollisionData colData = CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape);
                if (colData.Collision) {
                    block.LoseHealth();
                    if (colData.CollisionDir == CollisionDirection.CollisionDirUp || colData.CollisionDir == CollisionDirection.CollisionDirDown) {
                        ball.UpdateDirection((ball.Shape.AsDynamicShape()).Direction.X, -(ball.Shape.AsDynamicShape()).Direction.Y);
                    } else if (colData.CollisionDir == CollisionDirection.CollisionDirLeft || colData.CollisionDir == CollisionDirection.CollisionDirRight) {
                        ball.UpdateDirection(-(ball.Shape.AsDynamicShape()).Direction.X, (ball.Shape.AsDynamicShape()).Direction.Y);
                    }
                }
            });
    }
}