using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga;
public class Enemy : Entity {
    private int hitpoints;
    private IBaseImage enragedImage;
    public Enemy(DynamicShape shape, IBaseImage image, IBaseImage EnragedImage) : base(shape, image) {
        this.hitpoints = 4;
        this.enragedImage = EnragedImage;
        Shape.AsDynamicShape().Direction = new Vec2F(0.0f, -0.002f);

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
        Shape.AsDynamicShape().Direction *= 2.0f;
    }
}
