using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using Breakout;
using Breakout.Blocks;
using Breakout.Powers;

namespace BreakoutTests;

public class TestMovingBlock {
    private EntityContainer<Entity> container;
    private MovingBlock block;
    private EffectGenerator generator;

    [SetUp]
    public void Setup(){
        Window.CreateOpenGLContext();
        EventBus.ResetBus();
        block = new MovingBlock (
            new DynamicShape(
                new Vec2F(0.0f, 0.0f), new Vec2F(0.10f, 0.01f), new Vec2F(0.01f, 0.0f)), 
            new Image(Path.Combine("Assets", "Images", "blue-block.png")), false);
        container = new EntityContainer<Entity>();
        generator = new EffectGenerator();
        EventBus.GetBus().Subscribe(GameEventType.InputEvent, generator);
    }

    [Test]
    public void TestMove() {
        var before = block.Shape.Position.X;
        block.UpdateBlock(container);
        var after = block.Shape.Position.X;
        Assert.That(before + 0.01f, Is.EqualTo(after));
    }

    [Test]
    public void TestMoveCollision() {
        List<bool> list = new List<bool>{};
        container.AddEntity(
            new Block (
                new DynamicShape(
                    new Vec2F(0.10f, 0.0f), new Vec2F(0.01f, 0.01f)), 
                new Image(Path.Combine("Assets", "Images", "blue-block.png")), false));
        var dir = block.Shape.AsDynamicShape().Direction.X;
        var before = block.Shape.Position.X;
        block.UpdateBlock(container);
        var after = block.Shape.Position.X;
        if (before == after) {
            list.Add(true);
        } else {
            list.Add(false);
        }
        if (-dir == block.Shape.AsDynamicShape().Direction.X){
            list.Add(true);
        } else {
            list.Add(false);
        }
        Assert.True(list.TrueForAll(ele => {return ele == true;}));
    }

    [Test]
    public void TestMoveNoCollision() {
        List<bool> list = new List<bool>{};
        container.AddEntity(
            new Block (
                new DynamicShape(
                    new Vec2F(0.40f, 0.0f), new Vec2F(0.01f, 0.01f)), 
                new Image(Path.Combine("Assets", "Images", "blue-block.png")), false));
        var dir = block.Shape.AsDynamicShape().Direction.X;
        var before = block.Shape.Position.X;
        block.UpdateBlock(container);
        var after = block.Shape.Position.X;
        if (before != after) {
            list.Add(true);
        } else {
            list.Add(false);
        }
        if (dir == block.Shape.AsDynamicShape().Direction.X){
            list.Add(true);
        } else {
            list.Add(false);
        }
        Assert.True(list.TrueForAll(ele => {return ele == true;}));
    }

    [Test]
    public void TestMoveOOB() {
        List<bool> list = new List<bool>{};
        var dir = block.Shape.AsDynamicShape().Direction.X;
        for (int i = 0; i <= 89; i++){
            block.UpdateBlock(container);
        }
        var before = block.Shape.Position.X;
        block.UpdateBlock(container);
        var after = block.Shape.Position.X;
        if (before == after) {
            list.Add(true);
        } else {
            list.Add(false);
        }
        if (-dir == block.Shape.AsDynamicShape().Direction.X){
            list.Add(true);
        } else {
            list.Add(false);
        }
        Assert.True(list.TrueForAll(ele => {return ele == true;}));
    }

    [Test]
    public void TestLoseHealth() {
        block.hp = 2;
        var start = block.hp;
        block.LoseHealth();
        var after = block.hp;
        Assert.Less(after, start);
    }

    [Test]
    public void TestDeleteBlock() {
        block.hp = 0;
        block.LoseHealth();
        Assert.True(block.IsDeleted());
    }

    [Test]
    public void TestPowerUp() {
        block = new MovingBlock (
            new DynamicShape(
                new Vec2F(0.0f, 0.0f), new Vec2F(1/12.0f, 1/25.0f)), 
            new Image(Path.Combine("Assets", "Images", "blue-block.png")), true);
        block.DeleteBlock();
        EventBus.GetBus().ProcessEvents();
        var count = generator.GetEffects().CountEntities();
        Assert.That(count, Is.EqualTo(1));
    }
}