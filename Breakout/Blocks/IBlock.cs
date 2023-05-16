using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;

namespace Breakout.Blocks;

public interface IBlock {
    int value {get; set;}
    int hp {get; set;}
    bool powerUp {get;}

    /// <summary>
    /// Block loses one health. Calls DeleteBlock() if hp = 0 to delete block
    /// </summary>
    void LoseHealth(){}

    /// <summary>
    /// Deletes block and creates a StatusEvent to comunicate its points to the Point class. 
    /// </summary>
    void DeleteBlock(){}

    /// <summary>
    /// For updating a block in relation to other blocks in a EntityContainer. 
    /// Used for subclasses with special abilities 
    /// </summary>
    /// <param name="blocks"> An EntityContainer of blocks </param> 
    void UpdateBlock(EntityContainer<Entity> blocks){}
}