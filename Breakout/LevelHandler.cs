using DIKUArcade.Entities;
using DIKUArcade.Events;
using Breakout.Blocks;
using Breakout;
using System;
using System.IO;
using System.Collections.Generic;

namespace Breakout;

public class LevelHandler {
    private List<string> loadOrder;
    private FileReader reader;
    private Level currentLevel;
    private int lvlCount;

    public LevelHandler() {
        loadOrder = new List<string> {"powerUpTest.txt", "level2.txt","level3.txt"};
        lvlCount = 0;
        reader = new FileReader();
    }

    /// <summary>
    /// A getter for the blocks in the current level
    /// </summary>
    /// <returns> The blocks in the current level </returns>
    public EntityContainer<Entity> GetLevelBlocks(){
        return currentLevel.GetBlocks();
    }

    /// <summary>
    /// Resets the levelhandler for a newgame
    /// </summary>
    public void NewGame(){
        lvlCount = 0;
        NewLevel(loadOrder[lvlCount]);
    }

    /// <summary>
    /// Checks if the current level is empty of all breakable blocks and creates a new level if so
    /// </summary>
    private void NextLevel() {
        if (currentLevel.IsEmpty()) {
            if (lvlCount >= loadOrder.Count){
                EventBus.GetBus().RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_WON"
                    }
                );
            } else {
                NewLevel(loadOrder[lvlCount]);
                if ((lvlCount + 1 > loadOrder.Count - 1)!) {
                    lvlCount++;
                }
            }
        }
    }

    /// <summary>
    /// Creates the next level in the levelOrder
    /// </summary>
    /// <param name="lvl"> Name of the level file </param>
    private void NewLevel(string lvl) {
        if (currentLevel is not null){
            currentLevel.DeleteBlocks();
        } else {
            reader.Read(Path.Combine("Assets", "Levels", lvl));
            currentLevel = new Level(reader.map, reader.meta, reader.legend);
            EventBus.GetBus().Subscribe(GameEventType.StatusEvent, currentLevel);
        }
        if (reader.Read(Path.Combine("Assets", "Levels", lvl))) {
            lvlCount += 1;
            currentLevel.Reset(reader.map, reader.meta, reader.legend);
        }
    }

    /// <summary>
    /// Renders the current level
    /// </summary>
    public void RenderLevel() {
        currentLevel.Render();
    }

    /// <summary>
    /// Updates the current level and checks if ready for the next level
    /// </summary>
    public void UpdateLevel() {
        NextLevel();
        currentLevel.Update();
    }

    //For testing purposes
    /// <summary>
    /// By-passes other functions to immediatly create a level
    /// </summary>
    /// <param name="path"> Entire path for a txt file containing the level </param>
    public void Initialize(string path){
        if (currentLevel is null || currentLevel.IsEmpty()) {
            reader.Read(path);
            currentLevel = new Level(reader.map, reader.meta, reader.legend);
        }
    }

    //For testing purposes
    /// <summary>
    /// A getter for the current level
    /// </summary>
    /// <returns> The current level </returns>
    public Level GetLevel(){
        return currentLevel;
    }
}