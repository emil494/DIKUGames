using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout;

public class Player : Entity, IGameEventProcessor {

    private float moveLeft = 0.0f;

    private float moveRight = 0.0f;

    private float MOVEMENT_SPEED = 0.01f;

    private DynamicShape Shape;

    
    //private DynamicShape shape;
    public Player(DynamicShape shape, IBaseImage image) : base(shape, image) {
        //this.image = image;
        shape = shape.DynamicShape();
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
        if (shape.Position.X + shape.Direction.X >= 0.0f && 
        shape.Position.X + shape.Direction.X <= 1.0f-shape.Extent.X){
            shape.Move();
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
        }
    }
}