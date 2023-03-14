using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.Math;

namespace Galaga;
public class Enemy : Entity {
    private IBaseImage enrageImage;
    private int hitpoints;

    public int Hitpoints{
        get {return hitpoints;}
    }

    public Enemy(DynamicShape shape, IBaseImage image, IBaseImage enrage)
        : base(shape, image) {
            hitpoints = 4;
            enrageImage = enrage;
            shape.Direction = new Vec2F(0.0f, -0.002f);
        }
    
    public void Hit(){
        hitpoints -= 1;
    }

    public void Enrage(){
        if (enrageImage is not null){
            Image = enrageImage;
            Shape.AsDynamicShape().Direction += new Vec2F(0.0f, -0.004f);
        }
    }   
}
