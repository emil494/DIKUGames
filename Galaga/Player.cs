using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Galaga;
public class Player : IGameEventProcessor {
    private Entity entity;
    private DynamicShape shape;
    private float moveUp;
    private float moveDown;
    private float moveLeft;
    private float moveRight;
    private const float MOVEMENT_SPEED = 0.01f;
    public Player(DynamicShape shape, IBaseImage image) {
        entity = new Entity(shape, image);
        this.shape = shape;
        moveUp = 0.0f;
        moveDown = 0.0f;
        moveLeft = 0.0f;
        moveRight = 0.0f;
    }

    public Vec2F GetExtend(){
        return shape.Extent;
    } 

    public Vec2F GetPosition(){
        return shape.Position;
    }

    public void Render() {
        entity.RenderEntity();
    }

    public void Move() {
        if (shape.Position.X + shape.Direction.X >= 0.0f && 
        shape.Position.X + shape.Direction.X <= 1.0f-shape.Extent.X &&
        shape.Position.Y + shape.Direction.Y >= 0.0f && 
        shape.Position.Y + shape.Direction.Y <= 1.0f-shape.Extent.Y){
            shape.Move();
        }
    }

    private void SetMoveUp(bool val){
        if (val){
            moveUp = MOVEMENT_SPEED;
            UpdateDirection();
        } else {
            moveUp = 0.0f;
            UpdateDirection();
        }
    }

    private void SetMoveDown(bool val){
        if (val){
            moveDown = -MOVEMENT_SPEED;
            UpdateDirection();
        } else {
            moveDown = 0.0f;
            UpdateDirection();
        }
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
        shape.Direction.X = moveLeft + moveRight;
        shape.Direction.Y = moveDown + moveUp;
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
                    case "UP":
                        SetMoveUp(true);
                        break;
                    case "DOWN":
                        SetMoveDown(true);
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
                    case "UP":
                        SetMoveUp(false);
                        break;
                    case "DOWN":
                        SetMoveDown(false);
                        break;
                }
                break;
        }
    }

    public DynamicShape GetShape() {
        return shape;
    }

    public Entity GetEntity() {
        return entity;
    }
}
