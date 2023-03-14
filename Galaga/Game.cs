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
using System;
using Galaga.Squadron;

namespace Galaga;
public class Game : DIKUGame, IGameEventProcessor {
    private EntityContainer<Enemy> enemies;
    private List<Image> enemyStridesBlue;
    private List<Image> enemyStridesRed;
    private EntityContainer<PlayerShot> playerShots;
    private IBaseImage playerShotImage;
    private Player player;
    private GameEventBus eventBus;
    private AnimationContainer enemyExplosions;
    private List<Image> explosionStrides;
    private ISquadron squadron;
    private const int EXPLOSION_LENGTH_MS = 500;

    public Game(WindowArgs windowArgs) : base(windowArgs) {
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
        
        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> 
            { GameEventType.InputEvent, GameEventType.WindowEvent, GameEventType.PlayerEvent});

        window.SetKeyEventHandler(KeyHandler);
        eventBus.Subscribe(GameEventType.InputEvent, this);
        eventBus.Subscribe(GameEventType.WindowEvent, this);
        eventBus.Subscribe(GameEventType.PlayerEvent, player);

        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));

        enemyStridesBlue = ImageStride.CreateStrides (4, Path.Combine("Assets", "Images", "BlueMonster.png"));
        enemyStridesRed = ImageStride.CreateStrides (2, Path.Combine("Assets", "Images", "RedMonster.png"));

        squadron = new CheckeredSquadron();
        squadron.CreateEnemies(enemyStridesBlue, enemyStridesRed);
        enemies = squadron.Enemies;

        enemyExplosions = new AnimationContainer(squadron.MaxEnemies);
        explosionStrides = ImageStride.CreateStrides(8,
            Path.Combine("Assets", "Images", "Explosion.png"));
    }

    public override void Render() {
        player.Render();
        enemies.RenderEntities();
        playerShots.RenderEntities();
        enemyExplosions.RenderAnimations();
    }

    public override void Update() {
        eventBus.ProcessEventsSequentially();
        player.Move();
        IterateShots();
        IterateEnemies();
    }

    private void KeyPress(KeyboardKey key) {
        switch (key){
            case KeyboardKey.Escape:
                eventBus.RegisterEvent(
                    new GameEvent {EventType = GameEventType.WindowEvent, Message = "CLOSE_GAME"});
                break;
            case KeyboardKey.Left:
                eventBus.RegisterEvent(
                    new GameEvent {EventType = GameEventType.PlayerEvent, Message = "MOVE", StringArg1 = "LEFT"});
                break;
            case KeyboardKey.Right:
                eventBus.RegisterEvent(
                    new GameEvent {EventType = GameEventType.PlayerEvent, Message = "MOVE", StringArg1 = "RIGHT"});
                break;

        }
    }

    private void KeyRelease(KeyboardKey key) {
        switch (key){
            case KeyboardKey.Left:
                eventBus.RegisterEvent(
                    new GameEvent {EventType = GameEventType.PlayerEvent, Message = "STOP_MOVE", StringArg1 = "LEFT"});
                break;
            case KeyboardKey.Right:
                eventBus.RegisterEvent(
                    new GameEvent {EventType = GameEventType.PlayerEvent, Message = "STOP_MOVE", StringArg1 = "RIGHT"});
                break;
            case KeyboardKey.Space:
                PlayerShot newShot = new PlayerShot(player.GetPosition() + 
                    new Vec2F (player.GetExtend().X/2, 0), playerShotImage);
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
        playerShots.Iterate(shot => {
            shot.Shape.Move();
            if ( shot.Shape.Position.Y >= 1.0f) {
                shot.DeleteEntity();
            } else {
                enemies.Iterate(enemy => {
                    if ((CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), 
                    enemy.Shape)).Collision){
                        shot.DeleteEntity();
                        enemy.LoseHealth();
                        if (enemy.IsDeleted()) {
                        AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                        }
                    }
                });
            }
        });
    }

    private void IterateEnemies() {
        enemies.Iterate (enemy => {enemy.Shape.Move();});
    }

    public void AddExplosion(Vec2F position, Vec2F extent) {
        enemyExplosions.AddAnimation(new StationaryShape(position, extent), 
            EXPLOSION_LENGTH_MS/8, new ImageStride(EXPLOSION_LENGTH_MS/8, explosionStrides));
    }
}