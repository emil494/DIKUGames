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

    public void Initialize(string lvl){
        if (currentLevel is null || currentLevel.IsEmpty()){
            NewLevel(lvl);
        }
    }

    private void NewLevel(string lvl){
        if (reader.Read(lvl)) {
            lvlCount += 1;
            currentLevel = new Level(reader.map, reader.meta, reader.legend);
        }
    }

    public void RenderLevel(){
        currentLevel.Render();
    }
}