using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;
using DIKUArcade.Physics;

namespace Galaga;
public class Game : DIKUGame, IGameEventProcessor {
    private EntityContainer<Enemy> enemies;
    private EntityContainer<PlayerShot> playerShots;
    private IBaseImage playerShotImage;
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

        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));

        List<Image> images = ImageStride.CreateStrides
            (4, Path.Combine("Assets", "Images", "BlueMonster.png"));
        const int numEnemies = 8;
        enemies = new EntityContainer<Enemy>(numEnemies);
        for (int i = 0; i < numEnemies; i++) {
            enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, images)));
        }
    }

    public override void Render() {
        player.Render();
        enemies.RenderEntities();
        playerShots.RenderEntities();
    }

    public override void Update() {
        eventBus.ProcessEventsSequentially();
        player.Move();
        IterateShots();
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
    }

    private void KeyRelease(KeyboardKey key) {
        switch (key){
            case KeyboardKey.Left:
                player.SetMoveLeft(false);
                break;
            case KeyboardKey.Right:
                player.SetMoveRight(false);
                break;
            case KeyboardKey.Space:
                PlayerShot newShot = new PlayerShot(player.GetPosition(), playerShotImage);
                playerShots.AddEntity(newShot);
                break;
        }
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
    private void IterateShots() {
        System.Console.WriteLine(playerShots.CountEntities());
        playerShots.Iterate(shot => {
            shot.Move();
            // TODO: move the shot's shape
            if ( shot.Shape.Position.Y >= 1.0f) {
                shot.DeleteEntity();
                // TODO: delete shot
            } else {
                enemies.Iterate(enemy => {
                    //CollisionDetection detection = new CollisionDetection();
                    if ((CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape)).Collision){
                        shot.DeleteEntity();
                        enemy.DeleteEntity();
                    }

                    // TODO: if collision btw shot and enemy -> delete both entities
                });
            }
        });
    }
}