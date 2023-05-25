using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout;

public class Player : Entity, IGameEventProcessor {
    private float moveLeft;
    private float moveRight;
    private const float MOVEMENT_SPEED = 0.03f;
    private int lives;
    public Player(DynamicShape shape, IBaseImage image) : base(shape, image) {
        lives = 3;
        moveRight =  0.0f;
        moveLeft  = 0.0f;
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
            case "LOSELIFE": // Should tell the player to lose a life
                lives -= 1;
                break;
        }
    }

    public int GetLives(){ //This functions is only made for testing purposes
        return lives;
    }
}