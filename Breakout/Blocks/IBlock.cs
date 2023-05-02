namespace Breakout.Blocks;

//Must inheirit from the Entity class
interface IBlock{
    public int value {get;}
    public int hp {get; set;}
    public bool powerUp {get;}
    public void LoseHealth();
    public void DeleteBlock();
    public void UpdateBlock();
}