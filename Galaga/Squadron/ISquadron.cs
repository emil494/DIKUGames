using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Galaga;

namespace Galaga.Squadron;

public interface ISquadron {
    EntityContainer<Enemy> Enemies {get;}
    int MaxEnemies {get;}
    
    void CreateEnemies (List<Image> enemyStride,
        List<Image> alternativeEnemyStride);
}