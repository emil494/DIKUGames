using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga.MovementStrategy;
public class Down : IMovementStrategy{
    private float speed;
    public Down(){
        speed = 0.0003f;
    }
    public void MoveEnemy (Enemy enemy) {
        if (enemy.IsEnraged()) {
            enemy.Shape.Move(new Vec2F(0.0f, -speed*1.5f));
        } else {
        enemy.Shape.Move(new Vec2F(0.0f, -speed));
        }
    }
    public void MoveEnemies (EntityContainer<Enemy> enemies) {
        enemies.Iterate(enemy => MoveEnemy(enemy));
    }

    public void UpdateSpeed(float spd) {
        speed = spd;
    }
}