using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using System;
using Breakout.Blocks;

namespace Breakout;

public class Ball : Entity {
    private const float MOVEMENT_SPEED = 0.3f;
    private float yDir;
    private float xDir;
    private Random rand;
    private EntityContainer<Block> blocks;

    public Ball (DynamicShape shape, IBaseImage image) : base(shape, image) {
        rand = new Random();
        yDir = (((float)rand.Next((int)MOVEMENT_SPEED*100))/100f);
        xDir = System.MathF.Sqrt(System.MathF.Pow(MOVEMENT_SPEED, 2f) - System.MathF.Pow(yDir, 2f));
        (Shape.AsDynamicShape()).ChangeDirection(new Vec2F(xDir, yDir));
        //blocks = LevelHandler.GetLevelBlocks();
    }
    /*
    public void MoveBall() {
        Vec2F newPos = Position + base.Direction;

    }*/
}