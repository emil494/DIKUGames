using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using System.IO;
using DIKUArcade.Timers;
using System;
namespace Breakout;

public class Player : Entity, IGameEventProcessor {
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
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
                bigCounter = 0;
                smallCounter = 0;
            }

    private void SetMoveLeft(bool val){
        if (val){
            moveLeft = -MOVEMENT_SPEED;
            UpdateDirection();
        } else {
            moveLeft = 0.0f;
            UpdateDirection();
        }
    }

    private void SetMoveRight(bool val){
        if (val){
            moveRight = MOVEMENT_SPEED;
            UpdateDirection();
        } else {
            moveRight = 0.0f;
            UpdateDirection();
        }
    }

    private void UpdateDirection(){
        (Shape.AsDynamicShape()).Direction.X = moveLeft + moveRight;
    }

    public void Move() {
        if (Shape.Position.X + (Shape.AsDynamicShape()).Direction.X >= 0.0f && 
        Shape.Position.X + (Shape.AsDynamicShape()).Direction.X <= 1.0f-Shape.Extent.X){
            (Shape.AsDynamicShape()).Move();
        }
    }

    public void Reset(){
        Shape.Position.X = 0.45f;
        Shape.Position.Y = 0.1f;
    }

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
                                bigCounter += 1;
                                if (!IsBig){
                                    if (Shape.Position.X <= 0.1f) {
                                        Shape.Position.X += 0.1f; 
                                        } 
                                    if (Shape.Position.X + Shape.Extent.X >= 0.9f)  {
                                        Shape.Position.X -= 0.1f;
                                    }
                                    Shape.ScaleXFromCenter(2f);
                                    IsBig = true;
                                }      
                                break;
                            case "STOP":
                                if (IsBig){
                                    bigCounter -=1;
                                    if (bigCounter <= 0){
                                        Shape.ScaleXFromCenter(0.5f);
                                        IsBig = false;
                                    }
                                }    
                                break; 
                        }
                        break;
                    case "SLIM":
                        switch (gameEvent.StringArg2){
                            case "START":
                                if (!IsSmall){
                                    smallCounter += 1;
                                        Shape.ScaleXFromCenter(0.5f);
                                        IsSmall = true;
                                }      
                                break;
                            case "STOP":
                                if (IsSmall){
                                    smallCounter -=1;
                                    if (smallCounter <= 0){
                                        Shape.ScaleXFromCenter(2.0f);
                                        IsSmall = false;
                                    }
                                }
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
        }
    }
}