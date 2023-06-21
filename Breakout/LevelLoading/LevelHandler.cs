using DIKUArcade.Entities;
using DIKUArcade.Events;
using Breakout.Blocks;
using Breakout;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace Breakout;

/// <summary>
/// LevelHandler is responsible reading the correct file and create the correct level 
/// in the correct order according to its set loadOrder.
/// </summary>
public class LevelHandler {
    private List<string> loadOrder;
    private FileReader reader;
    private Level currentLevel;
    private int lvlCount;

    public LevelHandler() {
        loadOrder = new List<string> {"levelf1.txt","levelf2.txt","levelf3.txt","level1.txt", "level2.txt", "level3.txt"};
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
            if (lvlCount + 1 >= loadOrder.Count){
                EventBus.GetBus().RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_WON"
                    }
                );
            } else {
                lvlCount++;
                NewLevel(loadOrder[lvlCount]);
            }
        }
    }

    /// <summary>
    /// Creates the next level in the levelOrder
    /// </summary>
    /// <param name="lvl"> Name of the level file </param>
    private void NewLevel(string lvl) {
        EventBus.GetBus().RegisterEvent(
            new GameEvent{
                EventType = GameEventType.StatusEvent,
                Message = "RESET_BALLS"
            }
        );
        EventBus.GetBus().RegisterEvent(
            new GameEvent{
                EventType = GameEventType.InputEvent,
                Message = "RESET_EFFECTS"
            }
        );
        if (currentLevel is not null){
            currentLevel.DeleteBlocks();
        } 
        
        if (currentLevel is null){
            //Initialize first level
            if (reader.Read(path(lvl))){
                currentLevel = new Level(reader.map, reader.meta, reader.legend);
                EventBus.GetBus().Subscribe(GameEventType.StatusEvent, currentLevel);
            } else {
                System.Console.WriteLine($"Unable to load ASCII file: {lvl}");
                reader.Read(path("level1.txt"));
                currentLevel = new Level(reader.map, reader.meta, reader.legend);
            }
        } 

        //Initialize next level
        else if (reader.Read(path(lvl))) {
            currentLevel.Reset(reader.map, reader.meta, reader.legend);
        } else {
            System.Console.WriteLine($"Unable to load ASCII file: {lvl}");
            reader.Read(path("level1.txt"));
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
    
    
    /// <summary>
    /// A getter for the current level
    /// </summary>
    /// <returns> The current level </returns>
    public Level GetLevel(){
        return currentLevel;
    }

    //Exists for testing reasons, ideally a generalized path without this method
    /// <summary>
    /// Makes the correct pathing for testing and normal play
    /// </summary>
    /// <param name="lvl"> The name of the levels textfile </param>
    /// <returns> The correct path as a string </returns>
    private string path(string lvl) {
        
        //Checks if it's a testing path
        if (File.Exists(Path.Combine("..", "..", "..", "Assets", "Levels", lvl))){
            return Path.Combine("..", "..", "..", "Assets", "Levels", lvl);

        //Else normal path
        } else {
            return Path.Combine("Assets", "Levels", lvl);
        }
    }

    //For testing purposes
    /// <summary>
    /// Getter for levelcount
    /// </summary>
    /// <returns> lvlcount as an int </returns>
    public int GetLevelCount(){
        return lvlCount;
    }

    //For testing purposes
    /// <summary>
    /// Getter for LoadOrder size
    /// </summary>
    /// <returns> LoadOrder.Count as an int </returns>
    public int GetLoadOrderSize(){
        return loadOrder.Count;
    }
}