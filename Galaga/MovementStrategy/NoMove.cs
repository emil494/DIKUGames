using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace Galaga.MovementStrategy;

public class NoMove : IMovementStrategy{

    public NoMove(){}

    public void MoveEnemy (Enemy enemy){}

    public void MoveEnemies (EntityContainer<Enemy> enemies){}
} 