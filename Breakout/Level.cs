using DIKUArcade.Entities;
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
    public Dictionary<string, string> legend;

    public Level(List<string> map_, Dictionary<string, string> metaData_,
    Dictionary<string, string> legend_){
        name = metaData_["Name"];
        time = metaData_["Time"];
        map = mao_;
        metaData = metaData_;
        legend = legend_;
    }
    
    private void CreateBlocks(List<string> map){
        foreach (string line in map){
            var i = 0;
            foreach (char c in line){
                var j = 0;
                if (c == "-"){}
                else{
                    new Block (new StationaryShape(), Path.Combine("Assets"legend[c]));
                }
                j++;
            }
            i++;
        }
    }

    public bool IsEmpty(){

    }
}