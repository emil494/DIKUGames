using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace Galaga;

public class Health {
        private int health;
        private Text display;
        private bool gameOver;
        public Health (Vec2F position, Vec2F extent) {
            health = 3;
            display = new Text (health.ToString(), position, extent);
            display.SetColor(System.Drawing.Color.Coral);
            gameOver = false;
        }

    // The health goes one down, and the display is updated
    public void LoseHealth () {
        health--;
        if (health <= 0) {
            display.SetText("GAME OVER");
            gameOver = true;
        } else {
            display.SetText(health.ToString());
        }
    }
    public void RenderHealth () {
        display.RenderText();
    } 

    public bool GetGameOver() {
        return gameOver;
    }
}