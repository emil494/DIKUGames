using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Drawing;

namespace Galaga;

public class Health {
    private int health;
    private Text display;
    public Health (Vec2F position, Vec2F extent) {
        health = 3;
        display = new Text (health.ToString(), position, extent);
        display.SetColor(System.Drawing.Color.Coral );
    }
    // Remember to explaination your choice as to what happens
    //when losing health.
    public void LoseHealth () {
        health--;
        if (health <= 0){
            display.SetText("GAME OVER");
        } else {
            display.SetText(health.ToString());
        }
    }
    public void RenderHealth () {
        display.RenderText();
    }
}