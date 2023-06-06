using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Timers;
using Breakout;
using Breakout.Blocks;
using System.Collections.Generic;

namespace BreakoutTests;

public class LevelTest {
    FileReader reader;
    Level lvl2;
    EntityContainer<Entity> blocks;

    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();
        StaticTimer.PauseTimer();
        StaticTimer.RestartTimer();
        reader = new FileReader();
        reader.Read(Path.Combine("..", "..", "..", "Assets", "Levels", "level2.txt"));
        lvl2 = new Level(reader.map, reader.meta, reader.legend);
        blocks = lvl2.GetBlocks();
    }

    [Test]
    public void TestBlockNum() {
        int num = blocks.CountEntities();
        Assert.That(num, Is.EqualTo(72));
    }

    [Test]
    public void TestPowerUpTrue() {
        bool res = lvl2.PowerUp('i');
        Assert.True(res);
    }

    [Test]
    public void TestPowerUpFalse() {
        bool res = lvl2.PowerUp('j');
        Assert.False(res);
    }

    [Test]
    public void TestPowerUp_NoIndex() {
        reader.Read(Path.Combine("..", "..", "..", "Assets", "Levels", "levelNoPowerUp.txt"));
        Level noPower = new Level(reader.map, reader.meta, reader.legend);
        bool res = noPower.PowerUp('j');
        Assert.False(res);
    }

    [Test]
    public void TestIsEmptyFalse() {
        bool res = lvl2.IsEmpty();
        Assert.False(res);
    }

    [Test]
    public void TestIsEmptyTrue() {
        blocks.Iterate(block => {
            block.DeleteEntity();
        });
        bool res = lvl2.IsEmpty();
        Assert.True(res);
    }

    [Test]
    public void TestIsEmptyTrue_WithUnbreakable() {
        reader.Read(Path.Combine("..", "..", "..", "Assets", "Levels", "unbreakableTest.txt"));
        Level unbreakable = new Level(reader.map, reader.meta, reader.legend);
        bool res = unbreakable.IsEmpty();
        Assert.True(res);
    }

    [Test]
    public void TestDeleteBlocks() {
        lvl2.DeleteBlocks();
        int num = blocks.CountEntities();
        Assert.That(num, Is.EqualTo(0));
    }    

    [Test]
    public void TestAddMoving() {
        reader.Read(Path.Combine("..", "..", "..", "Assets", "Levels", "MovingTest.txt"));
        Level moving = new Level(reader.map, reader.meta, reader.legend);
        blocks = moving.GetBlocks();
        List<bool> list = new List<bool>();
        blocks.Iterate(block =>{
            if (block is MovingBlock mb){
                list.Add(true);
            }
        });
        Assert.True(list.Contains(true));
    }    

    [Test]
    public void TestAddHardened() {
        reader.Read(Path.Combine("..", "..", "..", "Assets", "Levels", "HardenedTest.txt"));
        Level moving = new Level(reader.map, reader.meta, reader.legend);
        blocks = moving.GetBlocks();
        List<bool> list = new List<bool>();
        blocks.Iterate(block =>{
            if (block is HardenedBlock mb){
                list.Add(true);
            }
        });
        Assert.True(list.Contains(true));
    } 

    [Test]
    public void TestTime() {
        lvl2.Update();
        int before = lvl2.GetRemaningTime();
        System.Threading.Thread.Sleep(2000);
        lvl2.Update();
        int after = lvl2.GetRemaningTime();
        Assert.That(after, Is.EqualTo(before - 2));
    } 
}
