using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;

namespace Breakout.Blocks;

public class HardenedBlock : Block {
    private IBaseImage damaged;

    public HardenedBlock(DynamicShape shape, IBaseImage image, bool power, string file) : base(shape, image, power){
        hp = 2;
        var split = file.Split(".");
        damaged = new Image(Path.Combine("Assets", "Images", $"{split[0]}-damaged.png"));
    }

    public override void LoseHealth(){
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
}