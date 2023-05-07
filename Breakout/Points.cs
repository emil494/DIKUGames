using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout;

public class Points {

    public void ProcessEvent(GameEvent gameEvent) {
            switch (gameEvent.Message){
                case "MOVE":
                    switch (gameEvent.StringArg1){
                        case "RIGHT":
                            SetMoveRight(true);
                            break;
                    break;
            }

        }
    }
}
