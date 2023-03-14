using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga.MovementStrategy;
public class Down : IMovementStrategy{
    public Down(){}
    public void MoveEnemy (Enemy enemy) {
        if (enemy.IsEnraged()) {
            enemy.Shape.Move(new Vec2F(0.0f, -0.004f));
        } else {
        enemy.Shape.Move(new Vec2F(0.0f, -0.002f));
        }
    }
    public void MoveEnemies (EntityContainer<Enemy> enemies) {
        enemies.Iterate(enemy => MoveEnemy(enemy));
    }
}