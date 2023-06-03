using Breakout;

namespace Breakout.Powers;

public interface IEffect{
    /// <summary>
    /// Moves the power up, and deletes it when it leaves the screen
    /// </summary>
    void Move(){}
    /// <summary>
    /// Sends a message through the game bus, to the appropriate class, to apply the effect
    /// </summary>
    void Apply(){}
    /// <summary>
    /// Checks for collision between the player and the powerup, 
    /// and calls the Apply method if one occurs
    /// </summary>
    /// <param name="player">The player object, to check for collision with</param>
    void PlayerCollision(Player player){}
}