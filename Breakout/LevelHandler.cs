using DIKUArcade.Entities;
using Breakout.Blocks;
using System;
using System.Collections.Generic;
namespace Breakout;

public class LevelHandler {
    private List<string> loadOrder;
    private FileReader reader;
    private static Level currentLevel;
    private int lvlCount;

    public LevelHandler() {
        loadOrder = new List<string> {"level1.txt", "level2.txt"};
        lvlCount = 0;
        reader = new FileReader();
    }

    public static EntityContainer<Entity> GetLevelBlocks(){
        return currentLevel.GetBlocks();
    }

    public void NextLevel() {
        if (currentLevel is null || currentLevel.IsEmpty()) {
            NewLevel(loadOrder[lvlCount]);
            if ((lvlCount + 1 > loadOrder.Count - 1)!) {
                lvlCount++;
            }
        }
    }

    private void NewLevel(string lvl) {
        if (currentLevel is not null){
            currentLevel.DeleteBlocks();
        }
        if (reader.Read(lvl)) {
            lvlCount += 1;
            currentLevel = new Level(reader.map, reader.meta, reader.legend);
        }
    }

    public void RenderLevel() {
        currentLevel.Render();
    }

    public void UpdateLevel() {
        NextLevel();
        currentLevel.Update();
    }
}