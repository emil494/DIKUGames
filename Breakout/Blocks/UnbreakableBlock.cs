using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks;

public class UnbreakableBlock : Block {
    public UnbreakableBlock(DynamicShape shape, IBaseImage image, bool power) : base(shape, image, power){}

    public override void LoseHealth(){}

    public override void DeleteBlock(){
        DeleteEntity();
    }
}