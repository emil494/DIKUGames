using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;

namespace Breakout.Blocks;

public class Block : Entity {
    public int value {get; set;}
    public int hp {get; set;}
    public bool powerUp {get;}
    public Block(DynamicShape shape, IBaseImage image, bool power) : base(shape, image){
        powerUp = power; 
        value = 1;
        hp = 1;
    }

    public virtual void LoseHealth(){
        if (hp - 1 <= 0){
            hp -= 1;
            DeleteBlock();
        } else{
            hp -= 1;
        }
    }

    public virtual void DeleteBlock(){
        if (powerUp){
            //To do: Create powerUp through EventBus
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

    public virtual void UpdateBlock(EntityContainer<Block> blocks){}
}