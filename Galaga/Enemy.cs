using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.Math;

namespace Galaga;
public class Enemy : Entity {
    private bool enraged = false;
    private IBaseImage enrageImage;
    private int hitpoints;
    private Vec2F startPos;

    public bool IsEnraged{
        get {return enraged;}
    }

    public Vec2F StartPos{
        get {return startPos;}
    }

    public Enemy(DynamicShape shape, IBaseImage image, IBaseImage enrage)
        : base(shape, image) {
            hitpoints = 4;
            enrageImage = enrage;
            shape.Direction = new Vec2F(0.0f, -0.002f);
            startPos = Shape.Position;
        }
    
    public void Hit(){
        hitpoints -= 1;
        if (hitpoints <= 0){
            DeleteEntity();
        } else if (hitpoints <= 2 && !enraged){
            Enrage();
        }
    }

    private void Enrage(){
        enraged = true;
        Image = enrageImage;
        Shape.AsDynamicShape().Direction *= 2;
    }   
}
