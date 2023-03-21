using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using System.Collections.Generic;

namespace Galaga;

public class Explosion {
    public AnimationContainer container {get;}
    private List<Image> strides;
    private const int EXPLOSION_LENGTH_MS = 500;

    public Explosion (AnimationContainer Container, List<Image> Strides) {
        this.container = Container;
        this.strides = Strides;
    }

    public void AddExplosion(Vec2F position, Vec2F extent) {
        container.AddAnimation(new StationaryShape(position, extent), 
            EXPLOSION_LENGTH_MS, new ImageStride(EXPLOSION_LENGTH_MS/8, strides));
    }
}