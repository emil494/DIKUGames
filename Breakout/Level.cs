using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.Blocks;
using System;
using System.IO;
using System.Collections.Generic;

namespace Breakout;

public class Level {
    private EntityContainer<Entity> blocks;
    private Dictionary<string, string> metaData;
    private Dictionary<char, string> legend;

    public Level(List<string> map_, Dictionary<string, string> metaData_,
        Dictionary<char, string> legend_) {
        metaData = metaData_;
        legend = legend_;
        blocks = new EntityContainer<Entity>();
        CreateBlocks(map_);
    }

    public EntityContainer<Entity> GetBlocks() {
        return blocks;
    }

    private void CreateBlocks(List<string> map) {
        var j = 1.0f;
        foreach (string line in map) {
            var i = 0.0f;
            foreach (char c in line) {

                //If empty space, do nothing
                if (c.ToString() == "-") {}

                //Adds Hardened blocks if marked as
                else if (metaData.ContainsKey("Hardened") && 
                metaData["Hardened"].Contains(c.ToString())) {
                    blocks.AddEntity(
                        new HardenedBlock (
                            new StationaryShape(
                                new Vec2F(i, j), new Vec2F(1/12.0f, 1/25.0f)), 
                            new Image(
                                Path.Combine("Assets", "Images", legend[c])), 
                            PowerUp(c),
                            //Needs filename for damaged picture
                            legend[c]
                        )
                    );

                //Adds Unbreakable blocks if marked as
                } else if (metaData.ContainsKey("Unbreakable") && 
                metaData["Unbreakable"].Contains(c.ToString())) {
                    blocks.AddEntity(
                        new UnbreakableBlock(
                            new StationaryShape(
                                new Vec2F(i, j), new Vec2F(1/12.0f, 1/25.0f)), 
                            new Image(
                                Path.Combine("Assets", "Images", legend[c])), 
                            PowerUp(c)
                        )
                    );
                    
                //Adds Unbreakable blocks if marked as
                } else if (metaData.ContainsKey("Moving") && 
                metaData["Moving"].Contains(c.ToString())) {
                    blocks.AddEntity(
                        new MovingBlock(
                            new DynamicShape(
                                new Vec2F(i, j), new Vec2F(1/12.0f, 1/25.0f), 
                                new Vec2F(0.01f, 0.0f)), 
                            new Image(
                                Path.Combine("Assets", "Images", legend[c])), 
                            PowerUp(c)
                        )
                    );
                }

                //If character is not marked, make a basic Block
                else {
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

    //Public for testing purposes, ideally private
    public bool PowerUp(char c){
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

    //Checks if board is without breakable blocks
    public bool IsEmpty(){
        List<bool> list = new List<bool> {};
        blocks.Iterate( block =>{
            if (typeof (UnbreakableBlock) != block.GetType()) {
                list.Add(false);
            } else {
                list.Add(true);
            }
        });
        if (blocks.CountEntities() == 0 || list.TrueForAll(Bool => Bool == true)){
            return true;
        } else {
            return false;
        }
    }

    //Deletes all blocks
    public void DeleteBlocks(){
        blocks.Iterate( block =>{
            block.DeleteEntity();
        });
    }

    public void Render(){
        blocks.RenderEntities();
    }

    public void Update(){
        foreach (IBlock block in blocks){
            block.UpdateBlock();
        }
    }
}