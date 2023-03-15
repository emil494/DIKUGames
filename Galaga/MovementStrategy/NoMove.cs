using DIKUArcade.Entities;
namespace Galaga.MovementStrategy;
public class NoMove : IMovementStrategy{
    public NoMove(){}
    public void MoveEnemy (Enemy enemy) {}
    public void MoveEnemies (EntityContainer<Enemy> enemies) {}
    public void UpdateSpeed(float spd) {}
}
