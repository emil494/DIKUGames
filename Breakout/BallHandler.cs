using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Breakout.Blocks;
using System;
using System.IO;
using System.Collections.Generic;
namespace Breakout;

public class BallHandler : IGameEventProcessor {
    private EntityContainer<Ball> balls;
    public BallHandler(){
        balls = new EntityContainer<Ball>();
    }

    private void AddBall(Vec2F pos){
        balls.AddEntity(new Ball(pos));
    }

    public void InitializeGame() {
        AddBall(new Vec2F(0.45f, 0.16f));
    }
    
    public void UpdateBalls(EntityContainer<Entity> blocks, Player player) {
            balls.Iterate(ball => {
                ball.MoveBall();
                ball.BlockCollision(blocks);
                ball.PlayerCollision(player);
            });
            if (balls.CountEntities() <= 0) {
                EventBus.GetBus().RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.StatusEvent,
                        Message = "LOSE_HEALTH"
                    }
                );
                InitializeGame();
            }
    }

    public void RenderBalls() {
        foreach (Ball ball in balls) {
            if (!ball.IsDeleted()) {
                ball.RenderEntity();
            }
        }
    }

    public void ProcessEvent(GameEvent gameEvent) {
        switch (gameEvent.Message){
            case "APPLY_POWERUP":
                switch (gameEvent.StringArg1){
                    case "SPLIT":
                        EntityContainer<Ball> newBalls = new EntityContainer<Ball>();
                        foreach (Ball ball in balls) {
                            if (!ball.IsDeleted()) {
                                float x = ball.Shape.Position.X;
                                float y = ball.Shape.Position.Y;
                                ball.DeleteEntity();
                                newBalls.AddEntity(new Ball(new Vec2F(x,y)));
                                newBalls.AddEntity(new Ball(new Vec2F(x,y)));
                                newBalls.AddEntity(new Ball(new Vec2F(x,y)));
                            }
                        }
                        balls = newBalls;
                        break;
                }
                break;
        }
    }
}