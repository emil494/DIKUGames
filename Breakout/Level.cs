using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.Blocks;
using System;
using System.IO;

namespace Breakout;

public class Level{

    private string name;
    private int time;
    public EntityContainer<Block> blocks;
    public List<string> map;
    public Dictionary<string, string> metaData;
    public Dictionary<char, string> legend;

    public Level(List<string> map_, Dictionary<string, string> metaData_,
        Dictionary<char, string> legend_){
        name = metaData_["Name"];
        time = int.Parse(metaData_["Time"]);
        map = map_;
        metaData = metaData_;
        legend = legend_;
        blocks = new EntityContainer<Block>();
        CreateBlocks(map_);
    }
    
    private void CreateBlocks(List<string> map){
        var j = 1.0f;
        foreach (string line in map){
            var i = 0.0f;
            foreach (char c in line){
                if (c.ToString() == "-"){}
                else{
                    blocks.AddEntity(
                        new Block (new StationaryShape(
                            new Vec2F(i, j), new Vec2F(1/12.0f, 1/25.0f)
                        ), new Image(Path.Combine("Assets", "Images", legend[c])), false)
                    );
                }
                i+=1.0f/12.0f;
            }
            j-=1.0f/25.0f;
        }
    }

    public bool IsEmpty(){
        return true;
    }
}