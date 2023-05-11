using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Blocks;
using System.Collections.Generic;

namespace BreakoutTests;

public class TestMovingBlock {
    [SetUp]
    public void Setup(){
        block = new MovingBlock (
            new DynamicShape(
                new Vec2F(0.0f, 0.0f), new Vec2F(0.10f, 0.01f), new Vec2F(0.01f, 0.0f)), 
            new Image(Path.Combine("Assets", "Images", "blue-block.png")), false);
        container = new EntityContainer<Block>();
    }

    private EntityContainer<Block> container;
    private MovingBlock block;

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
}