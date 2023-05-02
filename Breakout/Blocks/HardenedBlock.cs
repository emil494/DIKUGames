using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;

namespace Breakout.Blocks;

public class HardenedBlock : Block {
    private IBaseImage damaged;

    public HardenedBlock(DynamicShape shape, IBaseImage image, bool power, string file) : base(shape, image, power){
        hp = 2;
        damaged = new Image(Path.Combine("Assets", "Images", $"{file}-damaged.png"));
    }

    public override void LoseHealth(){
        if (hp - 1 <= 0){
            DeleteBlock();
        } else if(hp - 1 == 1){
            this.Image = damaged;
        } else {
            hp -= 1;
        }
    }
}