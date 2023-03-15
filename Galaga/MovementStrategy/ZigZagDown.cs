using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System;

namespace Galaga.MovementStrategy;

public class ZigZagDown : IMovementStrategy{
    private float speed;
    private float period;
    private float amplitude;

    public ZigZagDown() {
        speed = 0.0003f;
        period = 0.045f;
        amplitude = 0.05f;
    }
    public void MoveEnemy (Enemy enemy) {
        float y = enemy.Shape.Position.Y - speed;
        float x = enemy.GetStartX() + amplitude * (float) Math.Sin((2.0d * Math.PI * ((double) enemy.GetStartY() - y)) / period);
        
        float Y = y - enemy.Shape.Position.Y;
        float X = x - enemy.Shape.Position.X;

        if (enemy.IsEnraged()) {
            enemy.Shape.Move(new Vec2F(X, Y*2));
        } else {
            enemy.Shape.Move(new Vec2F(X, Y));
        }
    }
    public void MoveEnemies (EntityContainer<Enemy> enemies) {
        enemies.Iterate(enemy => MoveEnemy(enemy));
    }
    public void UpdateSpeed(float spd) {
        speed = spd;
    }
}