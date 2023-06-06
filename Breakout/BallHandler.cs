using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Timers;
using Breakout.Blocks;
using System;
using System.IO;
using System.Collections.Generic;
namespace Breakout;

/// <summary>
/// Keeps track of all balls currently on screen, 
/// and is responsiple for updating, rendering and spawning of balls
/// </summary>
public class BallHandler : IGameEventProcessor {
    private EntityContainer<Ball> balls;
    private bool noBalls;
    public BallHandler(){
        balls = new EntityContainer<Ball>();
        noBalls = false;
    }

    /// <summary>
    /// Adds a new ball at a specified location
    /// </summary>
    /// <param name="pos">A position vector of where the ball should be spawned</param>
    // Public for testing reasons, ideally private
    public void AddBall(Vec2F pos){
        balls.AddEntity(new Ball(pos));
        if (noBalls) {
            noBalls = false;
        }
    }

    /// <summary>
    /// Initializes the game, by spawning a ball in the middle of the screen
    /// </summary>
    public void InitializeGame() {
        AddBall(new Vec2F(0.45f, 0.16f));
    }
    
    /// <summary>
    /// Itterates through all balls on screen, moving them and checking for collision.
    /// Also sends a message to lose health if no balls are left
    /// </summary>
    /// <param name="blocks">All blocks on screen, for collision detection</param>
    /// <param name="player">The player, for collision detection</param>
    public void UpdateBalls(EntityContainer<Entity> blocks, Player player) {
        balls.Iterate(ball => {
            ball.MoveBall();
            ball.BlockCollision(blocks);
            ball.PlayerCollision(player);
        });
        if (balls.CountEntities() <= 0 && !noBalls) {
            EventBus.GetBus().RegisterEvent(
                new GameEvent {
                    EventType = GameEventType.StatusEvent,
                    Message = "LOSE_HEALTH"
                }
            );
            noBalls = true;
        }
    }

    /// <summary>
    /// Renders all balls
    /// </summary>
    public void RenderBalls() {
        foreach (Ball ball in balls) {
            if (!ball.IsDeleted()) {
                ball.RenderEntity();
            }
        }
    }

    /// <summary>
    /// Resets the entitycontainer, containing all the balls
    /// </summary>
    public void Reset(){
        balls.ClearContainer();
        InitializeGame();
    }

    /// <summary>
    /// Splits all balls into three new balls
    /// </summary>
    public void Split() {
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
    }

    public void ProcessEvent(GameEvent gameEvent) {
        switch (gameEvent.Message){
            case "APPLY_POWERUP":
                switch (gameEvent.StringArg1){
                    case "SPLIT":
                        Split();
                        break;
                    case "INFINITE":
                        if (gameEvent.ObjectArg1 is Player player) {
                            float x = player.Shape.Position.X + (player.Shape.Extent.X/2.0f);
                            float y = player.Shape.Position.Y;
                            AddBall(new Vec2F(x,y));
                        }
                        break;
                }
                break;
            case "RESET_BALLS":
                Reset();
                break;
            case "SPACE":
                if (noBalls) {
                    if (gameEvent.ObjectArg1 is Player player) {
                        float x = player.Shape.Position.X + (player.Shape.Extent.X/2.0f);
                        float y = player.Shape.Position.Y;
                        AddBall(new Vec2F(x,y));
                    }
                    noBalls = false;
                }
                break;
        }
    }

    // For testing purposes
    /// <summary>
    /// Getter for balls
    /// </summary>
    /// <returns> balls as an EntityContainer<Ball> </returns>
    public EntityContainer<Ball> GetBalls() {
        return balls;
    }

    // For testing purposes
    /// <summary>
    /// Turns balls into a list
    /// </summary>
    /// <returns> balls as a List<Ball> </returns>
    public List<Ball> GetBallsList() {
        List<Ball> list = new List<Ball>{};
        balls.Iterate(ball => {
            list.Add(ball);
        });
        return list;
    }
}