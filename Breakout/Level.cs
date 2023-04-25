using DIKUArcade.Entities;
using Breakout.Blocks;
using System;
using System.IO;

namespace Breakout;

public class Level{

    private string name;
    private int time;
    public EntityContainer<Block> blocks;
    public Dictionary map;
    public Dictionary metaData;
    public Dictionary legend;

    public Level(List<string> map_, Dictionary metaData_, Dictionary legend_){
        name = metaData_[Name];
        time = metaData_[Time];
        map = mao_;
        metaData = metaData_;
        legend = legend_;
    }
    
    private void CreateBlocks(List<string> map){
        foreach (string line in map){
            foreach (char c in line){
                if (c == "-"){}
                else{
                    
                }
            }
        }
    }

    public bool IsEmpty(){

    }
}