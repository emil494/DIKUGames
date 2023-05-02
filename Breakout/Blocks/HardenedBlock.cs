using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;

namespace Breakout.Blocks;

public class HardenedBlock : Entity, IBlock {
    private IBaseImage damaged;
    public int value {get;}
    public int hp {get; set;}
    public bool powerUp {get;}

    public HardenedBlock(StationaryShape shape, IBaseImage image, bool power, string file) : base(shape, image){
        powerUp = power;
        hp = 2;
        var split = file.Split(".");
        damaged = new Image(Path.Combine("Assets", "Images", $"{split[0]}-damaged.png"));
    }

    public void LoseHealth(){
        if (hp - 1 <= 0){
            hp -= 1;
            DeleteBlock();
        } else if(hp - 1 == 1){
            hp -= 1;
            this.Image = damaged;
        } else {
            hp -= 1;
        }
    }

    public void DeleteBlock(){
        if (powerUp){
            //To do: Create powerUp through EventBus
        }
        DeleteEntity();
    }

    public void UpdateBlock(){}
}