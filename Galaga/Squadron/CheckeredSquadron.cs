using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga.Squadron;

public class CheckeredSquadron : ISquadron{
    public EntityContainer<Enemy> Enemies {get;}
    public int MaxEnemies {get;}

    public CheckeredSquadron() {
        MaxEnemies = 8;
        Enemies = new EntityContainer<Enemy>(MaxEnemies);
    }

    public void CreateEnemies (List<Image> enemyStride, List<Image> alternativeEnemyStride) {
        for (int j = 0; j < 2; j++) {
            for (int i = 0; i < MaxEnemies; i++) {
                if ((i+j) % 2 == 0) {
                    Enemies.AddEntity(new Enemy(
                        new DynamicShape(
                            new Vec2F(0.1f + (float)i * 0.1f, 0.9f - (float)j * 0.1f), 
                                new Vec2F(0.1f, 0.1f)),
                            new ImageStride(80, enemyStride),
                            new ImageStride(80, alternativeEnemyStride)));
                }
            }
        }
    }
}