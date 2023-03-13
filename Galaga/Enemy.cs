using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.Math;

namespace Galaga;
public class Enemy : Entity {
    private List<Image> enemyStridesRed;
    private int hitpoints;

    public int Hitpoints{
        get {return hitpoints;}
    }

    public Enemy(DynamicShape shape, IBaseImage image)
        : base(shape, image) {
            hitpoints = 20;
            enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine(
                "Assets", "Images", "RedMonster.png"));
            shape.Direction = new Vec2F(0.0f, -0.002f);
        }
    
    public void Hit(){
        hitpoints -= 5;
    }

    public void Enrage(){
        Image = new ImageStride(80, enemyStridesRed);
        Shape.AsDynamicShape().Direction += new Vec2F(0.0f, -0.004f);
    }   
}
