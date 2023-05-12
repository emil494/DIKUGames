using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks;

public class UnbreakableBlock : Block {
    public UnbreakableBlock(DynamicShape shape, IBaseImage image, bool power) : base(shape, image, power){}

    /// <summary>
    /// Overrided to do nothing 
    /// </summary>
    public override void LoseHealth(){}

    /// <summary>
    /// Overrided to just delete the block 
    /// </summary>
    public override void DeleteBlock(){
        DeleteEntity();
    }
}