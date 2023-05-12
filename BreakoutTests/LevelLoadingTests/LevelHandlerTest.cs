using Breakout;
using DIKUArcade.GUI;

namespace BreakoutTests;

public class HandlerTest
{
    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();
        handler = new LevelHandler();
        lvl1Path = Path.Combine("..", "..", "..", "Assets", "Levels", "level1.txt");
        lvl2Path = Path.Combine("..", "..", "..", "Assets", "Levels", "level2.txt");
    }
    LevelHandler handler;
    string lvl1Path;
    string lvl2Path;

    [Test]
    public void TestInitialize() {
        handler.Initialize(lvl1Path);
        bool res = handler.GetLevel() != null;
        Assert.True(res);
    }
    
    [Test]
    public void TestInitialize2() {
        handler.Initialize(lvl1Path);
        Level lvl1 = handler.GetLevel();
        handler.Initialize(lvl2Path);
        bool res = handler.GetLevel() == lvl1;
        Assert.True(res);
    }
}