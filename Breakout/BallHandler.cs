using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Timers;
using Breakout.Blocks;
using System;
using System.IO;
using System.Collections.Generic;
namespace Breakout;

public class BallHandler : IGameEventProcessor {
    private EntityContainer<Ball> balls;
    private bool outOfUse;
    public BallHandler(){
        balls = new EntityContainer<Ball>();
        outOfUse = false;
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
            if (balls.CountEntities() <= 0 && !outOfUse) {
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

    public void Infinite() {
    double startTime = StaticTimer.GetElapsedMilliseconds();
    double lastExecutionTime = startTime;
    double interval = 200.0; // 0.2 seconds in milliseconds
    double currentTime = startTime;

    while (startTime + 5000.0 > currentTime) {
        currentTime = StaticTimer.GetElapsedMilliseconds(); // Update current time at each iteration

        // Check if the time difference since the last execution is greater than or equal to the desired interval
        if (currentTime - lastExecutionTime >= interval) {
            InitializeGame();
            lastExecutionTime = currentTime; // Update the last execution time
        }

        System.Threading.Thread.Sleep(200); // Introduce a 200ms delay between iterations
    }
        
    }

    public void ProcessEvent(GameEvent gameEvent) {
        if (!outOfUse) {
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
                        case "INFINITE":
                            //Infinite();
                            break;
                    }
                    break;
                case "NEWGAME":
                    outOfUse = true;
                    balls.ClearContainer();
                    break;
            }
        }
    }
}