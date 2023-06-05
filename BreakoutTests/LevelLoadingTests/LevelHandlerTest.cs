using Breakout;
using Breakout.Blocks;
using Breakout.States;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using System.Collections.Generic;

namespace BreakoutTests;

public class HandlerTest
{
    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();
        handler = new LevelHandler();
    }
    LevelHandler handler;

    [Test]
    public void TestNewGame() {
        handler.NewGame();
        Assert.True(handler.GetLevelCount() == 0 && handler.GetLevel() is not null);
    }

    [Test]
    public void TestNextLevel() {
        var before = new List<Entity>();
        var after = new List<Entity>();
        handler.NewGame();
        handler.GetLevelBlocks().Iterate(block => {
            before.Add(block);
        });
        handler.GetLevel().DeleteBlocks();
        handler.UpdateLevel();
        handler.GetLevelBlocks().Iterate(block => {
            after.Add(block);
        });
        
        var result = false;
        if (before.Count != after.Count){
            result = true;
        } else {
            for (int i = 0; i < before.Count; i++){
                if (before[i].Shape.Position.X != after[i].Shape.Position.X ||
                before[i].Shape.Position.Y != after[i].Shape.Position.Y){
                    result = true;
                }
            }
        }

        Assert.True(handler.GetLevelCount() == 1 && result);
    }

    [Test]
    public void TestWinCondition() {
        handler.NewGame();
        EventBus.ResetBus();
        var stateHandler = new StateHandler();
        EventBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateHandler);
        for (int i = 0; i < handler.GetLoadOrderSize(); i++){
            handler.GetLevel().DeleteBlocks();
            handler.UpdateLevel();
        }
        EventBus.GetBus().ProcessEvents();
        Assert.That(stateHandler.ActiveState, Is.InstanceOf<GameWon>());
    }    
}