using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;

namespace Breakout.Blocks;

public interface IBlock {
    int value {get; set;}
    int hp {get; set;}
    bool powerUp {get;}

    /// <summary>
    /// Block should have the ability to lose health.
    /// </summary>
    void LoseHealth(){}

    /// <summary>
    /// Deletes block with other possible conditions. 
    /// </summary>
    void DeleteBlock(){}

    /// <summary>
    /// For updating a block in relation to other blocks in a EntityContainer. 
    /// Used for subclasses with special abilities 
    /// </summary>
    /// <param name="blocks"> An EntityContainer of blocks </param> 
    void UpdateBlock(EntityContainer<Entity> blocks){}
}