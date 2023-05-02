using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks;

public class UnbreakableBlock : Entity, IBlock {
    public int value {get;}
    public int hp {get; set;}
    public bool powerUp {get;}
    public UnbreakableBlock(StationaryShape shape, IBaseImage image, bool power) : base(shape, image){
        powerUp = power;
        hp = 1;
    }

    public void LoseHealth(){}

    public void DeleteBlock(){
        DeleteEntity();
    }

    public void UpdateBlock(){}
}