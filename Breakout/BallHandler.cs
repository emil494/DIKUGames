using DIKUArcade.Entities;
using DIKUArcade.Math;
using Breakout.Blocks;
using System;
using System.IO;
using System.Collections.Generic;
namespace Breakout;

public class BallHandler{
    private EntityContainer<Ball> balls;
    public BallHandler(){
        balls = new EntityContainer<Ball>();
    }

    private void AddBall(Vec2F pos){
        balls.AddEntity(new Ball(pos));
    }

    public void InitializeGame() {
        balls.AddEntity(new Ball(new Vec2F(0.45f, 0.16f)));
    }
    
    public void UpdateBalls(EntityContainer<Entity> blocks, Player player) {
        foreach (Ball ball in balls) {
            ball.MoveBall();
            ball.BlockCollision(blocks);
            ball.PlayerCollision(player);
        }
    }

    public void RenderBalls() {
        foreach (Ball ball in balls) {
            ball.RenderEntity();
        }
    }
}