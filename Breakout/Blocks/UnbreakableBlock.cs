using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks;

/// <summary>
/// An unbreakable block
/// </summary>
public class UnbreakableBlock : Entity, IBlock {
    public int value {get; set;}
    public int hp {get; set;}
    public bool powerUp {get;}

    public UnbreakableBlock(DynamicShape shape, IBaseImage image) : base(shape, image){
        value = 0;
        hp = 0;
        powerUp = false;
    }

    /// <summary>
    /// Overrided to do nothing 
    /// </summary>
    public void LoseHealth(){}

    /// <summary>
    /// Overrided to just delete the block 
    /// </summary>
    public void DeleteBlock(){
        DeleteEntity();
    }

    public void UpdateBlock(EntityContainer<Entity> blocks){}
}