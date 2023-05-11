using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;

namespace Breakout.Blocks;

public class HardenedBlock : Block {

    /// <summary>
    /// Only HardenedBlock contains a damaged sprite as to look damaged when hit
    /// </summary>
    private IBaseImage damaged;

    /// <summary>
    /// Takes its sprites image name as to allow for naming it's damaged sprite
    /// </summary>
    public HardenedBlock(DynamicShape shape, IBaseImage image, bool power, string file) : base(shape, image, power){
        hp = 2;
        var split = file.Split(".");
        damaged = new Image(Path.Combine("Assets", "Images", $"{split[0]}-damaged.{split[1]}"));
    }

    /// <summary>
    /// Includes an additional else if for changing sprite when half health
    /// </summary>
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