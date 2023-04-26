using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.States;

namespace Breakout
{
    public class Game : DIKUGame
    
    {
        private StateHandler stateHandler;
        public Game(WindowArgs windowArgs) : base(windowArgs) {
            stateHandler = new StateHandler();
            EventBus.GetBus().InitializeEventBus(new List<GameEventType> 
                { GameEventType.InputEvent, GameEventType.WindowEvent, 
                GameEventType.PlayerEvent, GameEventType.GameStateEvent});
            // TODO: Set key event handler (inherited window field of DIKUGame class)
        }

        //private void KeyHandler(KeyboardAction action, KeyboardKey key) {} // TODO: Outcomment

        public override void Render() {
            stateHandler.ActiveState.RenderState();
        }

        public override void Update() {
            stateHandler.ActiveState.RenderState();
            EventBus.GetBus().ProcessEventsSequentially();

        }

        private void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (stateHandler.ActiveState != null){
            stateHandler.ActiveState.HandleKeyEvent(action, key);
        }
    }
    }
}