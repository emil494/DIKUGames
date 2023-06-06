using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using System.IO;
using DIKUArcade.Timers;
using System;
namespace Breakout;

/// <summary>
/// Responsible for all logic related to the players
/// movement and powerups/hazards related to the player.
/// </summary>
public class Player : Entity, IGameEventProcessor {
    private float moveLeft;
    private float moveRight;
    private const float MOVEMENT_SPEED = 0.03f;
    private bool IsBig;
    private bool IsSmall;
    private int bigCounter;
    private int smallCounter;
    public Player() : base(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.2f, 0.03f)),
            new Image(Path.Combine("Assets", "Images", "player.png"))) {
                IsBig = false;
                IsSmall = false;
                moveLeft = 0.0f;
                moveRight = 0.0f;
                bigCounter = 0;
                smallCounter = 0;
            }

    /// <summary>
    /// Sets the players left movement
    /// </summary>
    /// <param name="val"> Bool indicating if moving left </param>
    private void SetMoveLeft(bool val){
        if (val){
            moveLeft = -MOVEMENT_SPEED;
            UpdateDirection();
        } else {
            moveLeft = 0.0f;
            UpdateDirection();
        }
    }

    /// <summary>
    /// Sets the players right movement
    /// </summary>
    /// <param name="val"> Bool indicating if moving right </param>
    private void SetMoveRight(bool val){
        if (val){
            moveRight = MOVEMENT_SPEED;
            UpdateDirection();
        } else {
            moveRight = 0.0f;
            UpdateDirection();
        }
    }

    /// <summary>
    /// Updates the players horizontal direction according to SetMoveRight and SetMoveLeft
    /// </summary>
    private void UpdateDirection(){
        (Shape.AsDynamicShape()).Direction.X = moveLeft + moveRight;
    }

    /// <summary>
    /// Moves the player horizontally according to the set direction, 
    /// also prevents moving OOB.
    /// </summary>
    public void Move() {
        if (Shape.Position.X + (Shape.AsDynamicShape()).Direction.X >= 0.0f && 
        Shape.Position.X + (Shape.AsDynamicShape()).Direction.X <= 1.0f-Shape.Extent.X){
            (Shape.AsDynamicShape()).Move();
        }
    }

    /// <summary>
    /// Resets the players position
    /// </summary>
    public void Reset(){
        Shape.Position.X = 0.45f;
        Shape.Position.Y = 0.1f;
    }

    /// <summary>
    /// Doubles the player size and makes sure it always stay in bounds
    /// </summary>
    public void StartWide() {
        bigCounter += 1;
        if (!IsBig){
            if (Shape.Position.X <= 0.1f) {
                Shape.Position.X += 0.1f; 
                } 
            if (Shape.Position.X + Shape.Extent.X >= 0.9f)  {
                Shape.Position.X -= 0.1f;
            }
            Shape.ScaleXFromCenter(2.0f);
            IsBig = true;
        }
    }
    /// <summary>
    /// Reduces the player down to half size
    /// </summary>
    public void EndWide() {
        if (IsBig){
            bigCounter -=1;
            if (bigCounter <= 0){
                Shape.ScaleXFromCenter(0.5f);
                IsBig = false;
            }
        }    
    }

    /// <summary>
    /// Reduces the player down to half size
    /// </summary>
    public void StartSlim() {
        if (!IsSmall){
            smallCounter += 1;
            Shape.ScaleXFromCenter(0.5f);
            IsSmall = true;
        }      
    }
    /// <summary>
    /// Increases the player size back to the original size
    /// </summary>
    public void EndSlim() {
        if (IsSmall){
            smallCounter -=1;
            if (smallCounter <= 0){
                if (Shape.Position.X <= 0.1f) {
                    Shape.Position.X += 0.1f; 
                } 
                if (Shape.Position.X + Shape.Extent.X >= 0.9f)  {
                    Shape.Position.X -= 0.1f;
                }
                Shape.ScaleXFromCenter(2.0f);
                IsSmall = false;
            }
        }
    }

    /// <summary>
    /// Parameter of the message/command, e.g. sound: sound filename or identifier 
    /// </summary>
    /// <param name="gameEvent">The game event being processed.</param>
    public void ProcessEvent(GameEvent gameEvent) {
        switch (gameEvent.Message){
            case "MOVE":
                switch (gameEvent.StringArg1){
                    case "RIGHT":
                        SetMoveRight(true);
                        break;
                    case "LEFT":
                        SetMoveLeft(true);
                        break;
                }
                break;
            case "STOP_MOVE":
                switch (gameEvent.StringArg1){
                    case "RIGHT":
                        SetMoveRight(false);
                        break;
                    case "LEFT":
                        SetMoveLeft(false);
                        break;
                }
                break;
            case "APPLY_POWERUP":
                switch (gameEvent.StringArg1){
                    case "WIDE":
                        switch (gameEvent.StringArg2){
                            case "START":
                                StartWide();
                                break;
                            case "STOP":
                                EndWide();
                                break; 
                        }
                        break;
                    case "SLIM":
                        switch (gameEvent.StringArg2){
                            case "START":
                                StartSlim();
                                break;
                            case "STOP":
                                EndSlim();
                                break;     
                        }
                        break;
                } 
                break;  
            case "FIND_POS_PLAYER":
                EventBus.GetBus().RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.StatusEvent,
                        Message = "APPLY_POWERUP",
                        StringArg1 = "INFINITE",
                        ObjectArg1 = this
                    });
                break;
            case "SPACE":
                EventBus.GetBus().RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.StatusEvent,
                        Message = "SPACE",
                        ObjectArg1 = this
                    });
                break;
        }
    }

    //For testing purposes
    /// <summary>
    /// Getter for moveRight
    /// </summary>
    /// <returns> moveRight as a float </returns>
    public float GetMoveRight(){
        return moveRight;
    }
    
    //For testing purposes
    /// <summary>
    /// Getter for moveLeft
    /// </summary>
    /// <returns> moveLeft as a float </returns>
    public float GetMoveLeft(){
        return moveLeft;
    }
}