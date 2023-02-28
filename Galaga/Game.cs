using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;

namespace Galaga;
    public class Game : DIKUGame, IGameEventProcessor {
        private Player player;
        private GameEventBus eventBus;

        public Game(WindowArgs windowArgs) : base(windowArgs) {
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.WindowEvent});
            window.SetKeyEventHandler(KeyHandler);
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.WindowEvent, this);
        }

    public override void Render() {
        player.Render();
    }

    public override void Update() {
        eventBus.ProcessEventsSequentially();
        player.Move();
    }

    private void KeyPress(KeyboardKey key) {
        switch (key){
            case KeyboardKey.Escape:
                GameEvent close = new GameEvent();
                close.EventType = GameEventType.WindowEvent;
                close.Message = "CLOSE_GAME";
                eventBus.RegisterEvent(close);
                break;
            case KeyboardKey.Left:
                player.SetMoveLeft(true);
                break;
            case KeyboardKey.Right:
                player.SetMoveRight(true);
                break;
        }
        // TODO: Close window if escape is pressed
        // TODO: switch on key string and set the player's move direction
    }

    private void KeyRelease(KeyboardKey key) {
        switch (key){
            case KeyboardKey.Left:
                player.SetMoveLeft(false);
                break;
            case KeyboardKey.Right:
                player.SetMoveRight(false);
                break;
        }
        // TODO: switch on key string and disable the player's move direction
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        switch (action){
            case KeyboardAction.KeyPress:
                KeyPress(key);
                break;
            case KeyboardAction.KeyRelease:
                KeyRelease(key);
                break;
        }
        // TODO: Switch on KeyBoardAction and call proper method
    }

    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.WindowEvent){
            switch (gameEvent.Message){
                case "CLOSE_GAME":
                    window.CloseWindow();
                    break;
            }
        }
    }
}