using Breakout.Blocks;
using System;

namespace Breakout;

public class LevelHandler{
    private FileReader reader;
    private Level currentLevel;
    private int lvlCount;

    public LevelHandler(){
        lvlCount = 1;
    }

    public void Initialize(){
        if (cuttentLevel.IsEmpty()){
            NewLevel();
        }
    }

    private void NewLevel(){
        lvlCount += 1;
        reader.Read($"level{lvlCount.ToString()}.txt");
        currentLevel = new Level(reader.map, reader.meta, reader.legend);
    }
}