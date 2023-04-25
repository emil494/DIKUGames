namespace Breakout.Blocks;

interface IBlock{
    public int hp {get; set;}
    public bool powerUp {get;}
    public void LoseHealth();
    public void DeleteBlock();
}