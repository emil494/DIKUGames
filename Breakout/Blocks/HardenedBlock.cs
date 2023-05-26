using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using System.IO;

namespace Breakout.Blocks;

public class HardenedBlock : Entity, IBlock {
    public int value {get; set;}
    public int hp {get; set;}
    public bool powerUp {get;}

    /// <summary>
    /// Only HardenedBlock contains a damaged sprite as to look damaged when hit
    /// </summary>
    private IBaseImage damaged;

    /// <summary>
    /// Takes its sprites image name as to allow for naming it's damaged sprite
    /// </summary>
    public HardenedBlock(DynamicShape shape, IBaseImage image, bool power, string file) : base(shape, image){
        hp = 2;
        value = 2;
        var split = file.Split(".");
        damaged = new Image(Path.Combine("Assets", "Images", $"{split[0]}-damaged.{split[1]}"));
    }

    /// <summary>
    /// Includes an additional else if for changing sprite when half health
    /// </summary>
    public void LoseHealth(){
        if (hp - 1 <= 0){
            hp -= 1;
            DeleteBlock();
        } else if(hp - 1 == 1){
            hp -= 1;
            this.Image = damaged;
        } else {
            hp -= 1;
        }
    }

    public void DeleteBlock(){
        if (powerUp){
            EventBus.GetBus().RegisterEvent(
                new GameEvent {
                    EventType = GameEventType.InputEvent, 
                    Message = "ADD_POWERUP",
                    StringArg1 = Shape.Position.X.ToString(),
                    StringArg2 = Shape.Position.Y.ToString()
                }
            );
        } else {
            EventBus.GetBus().RegisterEvent(
                new GameEvent {
                    EventType = GameEventType.InputEvent, 
                    Message = "ADD_HAZARD",
                    StringArg1 = Shape.Position.X.ToString(),
                    StringArg2 = Shape.Position.Y.ToString()
                }
            );
        }
        DeleteEntity();
        EventBus.GetBus().RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.StatusEvent, 
                        Message = "POINT_GAIN",
                        StringArg1 = value.ToString()
                    }
                );
    }

    public void UpdateBlock(EntityContainer<Entity> blocks){}
}