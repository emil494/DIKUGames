using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;

namespace Breakout
{
    public class Game : DIKUGame
    
    {
        private Player player;
        public Game(WindowArgs windowArgs) : base(windowArgs) {
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.15f, 0.03f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));
            // TODO: Set key event handler (inherited window field of DIKUGame class)
        }

        //private void KeyHandler(KeyboardAction action, KeyboardKey key) {} // TODO: Outcomment

        public override void Render()
        {
            player.RenderEntity();
        }

        public override void Update()
        {
            
        }
    }
}