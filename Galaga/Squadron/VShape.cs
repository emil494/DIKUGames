using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga.Squadron;

public class VShape : ISquadron{
    public EntityContainer<Enemy> Enemies {get;}
    public int MaxEnemies {get;}

    public VShape(){
        MaxEnemies = 7;
        Enemies = new EntityContainer<Enemy>(MaxEnemies);
    }

    public void CreateEnemies (List<Image> enemyStride, List<Image> alternativeEnemyStride){
        for (int i = 0; i < 2; i++) {
            for (int j = 0; j < 5; j++){
                if (j == 2 && i == 0 || j == 0 && i == 1 || j == 4 && i == 1){}
                else{
                    Enemies.AddEntity(new Enemy(
                        new DynamicShape(new Vec2F(0.25f + (float)j * 0.1f, 0.9f - (float)i * 0.1f) 
                        , new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, enemyStride),
                        new ImageStride(80, alternativeEnemyStride)
                    ));
                }
            }
        }
    }
}