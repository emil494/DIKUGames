using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks;

public class HardenedBlock : Entity, IBlock {
    public int hp {get; set;}
    public bool powerUp {get;}

    public HardenedBlock(StationaryShape shape, IBaseImage image, bool power) : base(shape, image){
        powerUp = power;
        hp = 4;
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
            //To do: Create powerUp
        }
        DeleteEntity();
    }
}