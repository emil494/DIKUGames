using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks;

public class Block : Entity, IBlock {
    public int value {get;}
    public int hp {get; set;}
    public bool powerUp {get;}
    public Block(StationaryShape shape, IBaseImage image, bool power) : base(shape, image){
        powerUp = power;
        hp = 2;
    }

    public void LoseHealth(){
        if (hp - 1 <= 0){
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
}