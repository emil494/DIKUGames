using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks;

public class Block : Entity {
    public int value {get;}
    public int hp {get; set;}
    public bool powerUp {get;}
    public Block(DynamicShape shape, IBaseImage image, bool power) : base(shape, image){
        powerUp = power;
        hp = 1;
    }

    public virtual void LoseHealth(){
        if (hp - 1 <= 0){
            hp -= 1;
            DeleteBlock();
        } else{
            hp -= 1;
        }
    }

    public virtual void DeleteBlock(){
        if (powerUp){
            //To do: Create powerUp through EventBus
        }
        DeleteEntity();
    }

    public virtual void UpdateBlock(EntityContainer<Block> blocks){}
}