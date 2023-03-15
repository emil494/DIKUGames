using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga;
public class Enemy : Entity {
    private int hitpoints;
    private IBaseImage enragedImage;
    private bool enraged;
    private float X_0, Y_0;
    public Enemy(DynamicShape shape, IBaseImage image, IBaseImage EnragedImage) : base(shape, image) {
        this.hitpoints = 4;
        this.enragedImage = EnragedImage;
        this.enraged = false;
        this.X_0 = shape.Position.X;
        this.Y_0 = shape.Position.Y;

    }

    public void LoseHealth() {
        hitpoints--;
        if (hitpoints <= 0) {
            DeleteEntity();
        } else if (hitpoints <= 2) {
            Enrage();
        }
    }

    public void Enrage() {
        Image = enragedImage;
        enraged = true;
        //Shape.AsDynamicShape().Direction *= 2.0f;
    }

    public bool IsEnraged() {
        return enraged;
    }

    public float GetStartX() {
        return X_0;
    }

    public float GetStartY() {
        return Y_0;
    }
}
