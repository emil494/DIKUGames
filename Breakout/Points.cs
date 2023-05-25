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
        dscore = new Text ($"Points: {score.ToString()}", new Vec2F(0.0f,-0.25f), new Vec2F(0.3f,0.3f)); 
        dscore.SetColor(System.Drawing.Color.Coral); //Coloring the visuals
    }
    
    /// <summary>
    /// Parameter of the message/command, e.g. sound: sound filename or identifier 
    /// </summary>
    /// <param name="gameEvent">The game event being processed.</param>
    public void ProcessEvent(GameEvent gameEvent) {
        switch (gameEvent.Message){
            case "POINT_GAIN":
                score += Int32.Parse(gameEvent.StringArg1); //Changing the score 
                dscore.SetText($"Points: {score.ToString()}"); //Updating the newly changed score
                break;
        }
    }

    /// <summary>
    /// Renders the score
    /// </summary>
    public void RenderScore() {
        dscore.RenderText(); //Rendering the text 
        
    }
    /// <summary>
    /// Making it possible to get the score for testing purposes
    /// </summary>
    public int GetScore(){ //This functions is only made for testing purposes
        return score;
    }
}

