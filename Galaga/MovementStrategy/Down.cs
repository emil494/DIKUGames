using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace Galaga.MovementStrategy;

public class Down : IMovementStrategy{

    public Down(){}

    public void MoveEnemy (Enemy enemy){
        enemy.Shape.Move();
    }
    
    public void MoveEnemies (EntityContainer<Enemy> enemies){
        enemies.Iterate(enemy => 
            {enemy.Shape.Move();}
        );
    }
} 