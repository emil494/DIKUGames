using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using System;

namespace Breakout;

public class Points : IGameEventProcessor {
    private int score; // The numerical score
    private Text dscore; //The display score

    public Points(){
        score = 0;

        // Creating the visuals 
        dscore = new Text (score.ToString(), new Vec2F(0.0f,-0.3f), new Vec2F(0.3f,0.4f)); 
        dscore.SetColor(System.Drawing.Color.Coral); //Coloring the visuals
    }
    
    public void ProcessEvent(GameEvent gameEvent) {
        switch (gameEvent.Message){
            case "POINT_GAIN":
                score += Int32.Parse(gameEvent.StringArg1); //Changing the score 
                dscore.SetText(score.ToString()); //Updating the newly changed score
                break;
        }
    }

    public void RenderScore() {
        dscore.RenderText(); //Rendering the text 
        
    }

    public int GetScore(){ //This functions is only made for testing purposes
        return score;
    }
}

