using DIKUArcade.Physics;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;

namespace Breakout.Blocks;

public class MovingBlock : Entity, IBlock {
    public int value {get;}
    public int hp {get; set;}
    public bool powerUp {get;}
    public MovingBlock(DynamicShape shape, IBaseImage image, bool power) : base(shape, image){
        powerUp = power;
        hp = 1;
    }

    public void LoseHealth(){
        if (hp - 1 <= 0){
            hp -= 1;
            DeleteBlock();
        } else{
            hp -= 1;
        }
    }

    public void DeleteBlock(){
        if (powerUp){
            //To do: Create powerUp through EventBus
        }
        DeleteEntity();
    }

    public void UpdateBlock(){
        List<bool> list = new List<bool> {};

        //Adds false to above list if any collition between the moving and any other block
        //True otherwise
        foreach (Entity block in LevelHandler.GetLevelBlocks()){
            if ((CollisionDetection.Aabb(Shape.AsDynamicShape(), block.Shape)).Collision) {
                list.Add(true);
            } else {
                list.Add(false);
            }
        };

        //Checks for out of bounds and collition with other blocks
        if (list.Contains(true) || 
        Shape.Extent.X + Shape.Position.X + (Shape.AsDynamicShape()).Direction.X >= 1.0f ||
        Shape.Position.X + (Shape.AsDynamicShape()).Direction.X <= 0.0f) {
            (Shape.AsDynamicShape()).Direction *= -1.0f;
        } else {
            (Shape.AsDynamicShape()).Move();
        }
    }
}