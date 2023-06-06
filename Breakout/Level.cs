using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Breakout.Blocks;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace Breakout;

public class Level : IGameEventProcessor{
    private Timer timer;
    private Text timeDisplay;
    private bool containTimer;
    private EntityContainer<Entity> blocks;
    private Dictionary<string, string> metaData;
    private Dictionary<char, string> legend;
    public Level(List<string> map_, Dictionary<string, string> metaData_,
        Dictionary<char, string> legend_) {

        metaData = metaData_;
        legend = legend_;

        if (metaData.ContainsKey("Time")){
            timer = new Timer(Int32.Parse(metaData["Time"]));
            timer.ResumeTimer();
            timeDisplay = new Text($"Time: ", 
                new Vec2F(0.36f, -0.25f), new Vec2F(0.3f, 0.3f));
            timeDisplay.SetColor(System.Drawing.Color.Coral);
            containTimer = true;
        } else {
            containTimer = false;
        }

        blocks = new EntityContainer<Entity>();
        CreateBlocks(map_);
    }

    /// <summary>
    /// A getter for the blocks in the level
    /// </summary>
    /// <returns> The blocks in the level </returns>
    public EntityContainer<Entity> GetBlocks() {
        return blocks;
    }

    /// <summary>
    /// Creates the blocks for the level based on the data read through the FileReader
    /// </summary>
    /// <param name="map"> List of strings representing the layout of blocks </param>
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
                            new DynamicShape(
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
                            new DynamicShape(
                                new Vec2F(i, j), new Vec2F(1/12.0f, 1/25.0f)), 
                            new Image(
                                Path.Combine("Assets", "Images", legend[c]))
                        )
                    );
                    
                //Adds Moving blocks if marked as
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
                        new Block (new DynamicShape(
                            new Vec2F(i, j), new Vec2F(1/12.0f, 1/25.0f)
                        ), new Image(Path.Combine("Assets", "Images", legend[c])), PowerUp(c))
                    );
                }
                i+=1.0f/12.0f;
            }
            j-=1.0f/25.0f;
        }
    }

    // Public for testing purposes, ideally private
    /// <summary>
    /// Checks if the given character is marked as having a powerUp
    /// </summary>
    /// <param name="c"> A character representing a block </param>
    /// <returns> A bool, true if it contains a powerUp, else false </returns>
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

    /// <summary>
    /// Checks if the board is without breakable blocks
    /// </summary>
    /// <returns> A bool, true if the board is without breakable blocks, else false </returns>
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

    /// <summary>
    /// Deletes all blocks on the board
    /// </summary>
    public void DeleteBlocks(){
        blocks.Iterate( block =>{
            block.DeleteEntity();
        });
    }

    /// <summary>
    /// Renders all blocks
    /// </summary>
    public void Render(){
        blocks.RenderEntities();
        timeDisplay.RenderText();
    }

    /// <summary>
    /// Updates all blocks on the board
    /// </summary>
    public void Update(){
        if (containTimer && timer.OutOfTime()){
            timer.PauseTimer();
            EventBus.GetBus().RegisterEvent(
                new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_OVER"
                }
            );
        }
        blocks.Iterate( block =>{
            if (block is IBlock IB){
                IB.UpdateBlock(blocks);
            }
        });
        if (containTimer) {
            timeDisplay.SetText($"Time: {timer.RemaningTime()}");
        }
    }
    
    public void Reset(List<string> map_, Dictionary<string, string> metaData_,
        Dictionary<char, string> legend_){
        timer = new Timer(Int32.Parse(metaData["Time"]));
        timer.ResumeTimer();
        blocks.ClearContainer();
        CreateBlocks(map_);
        metaData = metaData_;
        legend = legend_;
    }

    public void ProcessEvent(GameEvent gameEvent) {
        switch (gameEvent.Message){
            case "APPLY_POWERUP":
                switch (gameEvent.StringArg1){
                    case "ADD_TIME":
                        timer.AddTime(30);
                        break;
                }
                break;
            case "APPLY_HAZARD":
                switch (gameEvent.StringArg1) {
                    case "REDUCE_TIME":
                        timer.AddTime(-30);
                        break;
                }
                break;
        }
    }

    //For testing reasons
    public int GetRemaningTime(){
        return timer.RemaningTime();
    }
}