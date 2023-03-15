using DIKUArcade.Entities;
using DIKUArcade.Math;
using System;

namespace Galaga.MovementStrategy;

public class ZigZagDown : IMovementStrategy{

    private static float s = 0.0003f;

    public ZigZagDown(){}

    public void MoveEnemy (Enemy enemy){
        enemy.Shape.AsDynamicShape().Direction = 
            new Vec2F(ZigZagX(enemy), ZigZagY(enemy));
            enemy.Shape.Move();
    }
    public void MoveEnemies (EntityContainer<Enemy> enemies){
        enemies.Iterate(enemy => 
            {enemy.Shape.AsDynamicShape().Position = 
                new Vec2F(ZigZagX(enemy), ZigZagY(enemy));}
        );
    }

    private static float ZigZagX(Enemy enemy){
        var PiCal = (2*Math.PI*(enemy.StartPos.Y-ZigZagY(enemy))) / 0.045f;
        return (float)(enemy.StartPos.X + 0.05f * Math.Sin(PiCal));
    }

    private static float ZigZagY(Enemy enemy){
        if (enemy.IsEnraged){
            return enemy.Shape.Position.Y - s*2;
        } else{
            return enemy.Shape.Position.Y - s;
        }
    }
} 