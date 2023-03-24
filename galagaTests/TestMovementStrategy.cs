namespace galagaTests;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Galaga;
using Galaga.MovementStrategy;
using DIKUArcade.GUI;

public class MovementStrategyTests{
    [SetUp] 
    public void Setup(){
        enemy =
            new Enemy(
                new DynamicShape(
                    new Vec2F(0.5f, 0.5f), 
                    new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, 
                        ImageStride.CreateStrides (4, 
                            Path.Combine("Assets", "Images", "BlueMonster.png")
                        )
                    ),
                    new ImageStride(80, 
                        ImageStride.CreateStrides (2, 
                            Path.Combine("Assets", "Images", "RedMonster.png")
                        )
                    )
            );
    }
    private IMovementStrategy? movement;
    private Enemy enemy;

    [Test]
    public void TestDown(){
        movement = new Down();
        var start = enemy.Shape.Position;
        movement.MoveEnemy(enemy);
        var temp = enemy.Shape.Position;
        var OtherTemp = start + new Vec2F(0.0f, -0.0003f);
        Assert.That(temp.Y, Is.EqualTo(OtherTemp.Y));
    }

    [Test]
    public void TestNoMove(){
        movement = new NoMove();
        var start = enemy.Shape.Position;
        movement.MoveEnemy(enemy);
        var temp = enemy.Shape.Position;
        Assert.That(start.Y, Is.EqualTo(temp.Y));
    }
    
    [Test]
    public void TestZigZagDown(){
        movement = new ZigZagDown();
        var start = enemy.Shape.Position;
        movement.MoveEnemy(enemy);
        var temp = enemy.Shape.Position;
        var otherTemp = new Vec2F(
            enemy.GetStartX() + 0.05f * (float) Math.Sin(
                (2.0d * Math.PI * ((double) enemy.GetStartY() - start.Y - 0.0003f))
                / 0.045f), 
            start.Y - 0.0003f);
        Assert.That(temp.Y, Is.EqualTo(otherTemp.Y));
    }
}