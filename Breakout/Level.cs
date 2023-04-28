using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.Blocks;
using System;
using System.IO;
using System.Collections.Generic;

namespace Breakout;

public class Level{
    private EntityContainer<Block> blocks;
    private Dictionary<string, string> metaData;
    private Dictionary<char, string> legend;

    public Level(List<string> map_, Dictionary<string, string> metaData_,
        Dictionary<char, string> legend_){
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
                // To do: Add different kinds of blocks according to the file
                else{
                    blocks.AddEntity(
                        new Block (new StationaryShape(
                            new Vec2F(i, j), new Vec2F(1/12.0f, 1/25.0f)
                        ), new Image(Path.Combine("Assets", "Images", legend[c])), PowerUp(c))
                    );
                }
                i+=1.0f/12.0f;
            }
            j-=1.0f/25.0f;
        }
    }

    private bool PowerUp(char c){
        if (metaData.ContainsKey("PowerUp")) {
            if (metaData["PowerUp"].Contains(c.ToString())){
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

    public bool IsEmpty(){
        if (blocks.CountEntities() == 0){
            return true;
        } else {
            return false;
        }
    }

    public void Render(){
        blocks.RenderEntities();
    }
}