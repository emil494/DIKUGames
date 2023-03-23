namespace galagaTests;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Galaga;
using DIKUArcade.GUI;

public class EnemyTests{
    [SetUp] 
    public void Setup(){
        Window.CreateOpenGLContext();
        enemy =
            new Enemy(
                new DynamicShape(
                    new Vec2F(0.1f, 0.1f), 
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
    private Enemy enemy;

    [Test]
    public void TestLoseHealth(){
        var start = enemy.GetHealth();
        enemy.LoseHealth();
        var temp = enemy.GetHealth();
        var OtherTemp = start - 1;
        Assert.That(temp, Is.EqualTo(OtherTemp));
    }

    [Test]
    public void TestEnrage(){
        enemy.LoseHealth();
        enemy.LoseHealth();
        Assert.True(enemy.IsEnraged());
    }

    [Test]
    public void TestDelete(){
        enemy.LoseHealth();
        enemy.LoseHealth();
        enemy.LoseHealth();
        enemy.LoseHealth();
        Assert.True(enemy.IsDeleted());
    }
}