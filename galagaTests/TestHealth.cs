namespace galagaTests;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga;
using DIKUArcade.GUI;

public class HealthTests{
    [SetUp] 
    public void Setup(){
        health = new Health(
            new Vec2F(0.0f, 0.0f),
            new Vec2F(0.0f, 0.0f)
        );
    }
    private Health health;

    [Test]
    public void TestLoseHealth(){
        var start = health.GetHealth();
        health.LoseHealth();
        var temp = health.GetHealth();
        var OtherTemp = start - 1;
        Assert.That(temp, Is.EqualTo(OtherTemp));
    }

    [Test]
    public void TestGameOver(){
        health.LoseHealth();
        health.LoseHealth();
        health.LoseHealth();
        Assert.True(health.GetGameOver());
    }
}