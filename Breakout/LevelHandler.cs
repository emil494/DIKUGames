using Breakout.Blocks;
using System;

namespace Breakout;

public class LevelHandler{
    private FileReader reader;
    public Level currentLevel;
    private int lvlCount;

    public LevelHandler(){
        lvlCount = 0;
        reader = new FileReader();
    }

    public void Initialize(){
        if (currentLevel is null || currentLevel.IsEmpty()){
            NewLevel();
        }
    }

    private void NewLevel(){
        lvlCount += 1;
        reader.Read($"level{lvlCount.ToString()}.txt");
        currentLevel = new Level(reader.map, reader.meta, reader.legend);
    }

    public void RenderLevel(){
        currentLevel.Render();
    }
}