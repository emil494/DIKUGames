using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using System;

namespace Breakout;

public class Points : IGameEventProcessor {
    private int score;

    private Text dscore;

    public Points(){
        score = 0;
        dscore = new Text (score.ToString(), new Vec2F(0.0f,0.6f), new Vec2F(0.3f,0.4f));
        dscore.SetColor(System.Drawing.Color.Coral);
    }
    
    public void ProcessEvent(GameEvent gameEvent) {
        switch (gameEvent.Message){
            case "POINT_GAIN":
                score += Int32.Parse(gameEvent.StringArg1);
                dscore.SetText(score.ToString());
                break;
        }
    }

    public void RenderScore() {
        dscore.RenderText();
        
    }
}

