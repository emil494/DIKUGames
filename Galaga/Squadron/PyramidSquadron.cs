using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga.Squadron;

public class PyramidSquadron : ISquadron{
    public EntityContainer<Enemy> Enemies {get;}
    public int MaxEnemies {get;}

    public PyramidSquadron() {
        MaxEnemies = 18;
        Enemies = new EntityContainer<Enemy>(MaxEnemies);
    }

    public void CreateEnemies (List<Image> enemyStride, List<Image> alternativeEnemyStride) {
            for (int i = 1; i < 7; i++) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(
                        new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, enemyStride),
                        new ImageStride(80, alternativeEnemyStride)));
            }
            for (int i = 2; i < 6; i++) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(
                        new Vec2F(0.1f + (float)i * 0.1f, 0.8f), new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, enemyStride),
                        new ImageStride(80, alternativeEnemyStride)));
            }
            for (int i = 3; i < 5; i++) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(
                        new Vec2F(0.1f + (float)i * 0.1f, 0.7f), new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, enemyStride),
                        new ImageStride(80, alternativeEnemyStride)));
            }
    }
}