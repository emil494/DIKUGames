using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Physics;
using DIKUArcade.State;
using Galaga.Squadron;
using Galaga.MovementStrategy;

namespace Galaga.GalagaStates;
public class GameRunning : IGameState {
    private static GameRunning instance = null;
    private EntityContainer<Enemy> enemies;
    private EntityContainer<PlayerShot> playerShots;
    private IBaseImage playerShotImage;
    private Player player;
    private Explosion explosion;
    private IMovementStrategy move;
    private Wave wave;
    private int waveNum;
    private Health health;

    public static GameRunning GetInstance() {
        if (GameRunning.instance == null) {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.InitializeGameState();
        }
        return GameRunning.instance;
    }
    private void InitializeGameState(){
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
        
        GalagaBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);

        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));

        wave = new Wave();
        waveNum = wave.num;

        enemies = wave.GetEnemies();

        move = new Down();

        explosion = new Explosion(new AnimationContainer(wave.GetSquadron().MaxEnemies), 
            ImageStride.CreateStrides(8, Path.Combine("Assets", "Images", "Explosion.png")));
        health = new Health(new Vec2F(0.0f,-0.3f), new Vec2F(0.3f,0.4f));
    }

    public void ResetState(){
        instance.InitializeGameState();
    }

    public void RenderState() {
        if (!health.GetGameOver()) {
            player.Render();
            enemies.RenderEntities();
            playerShots.RenderEntities();
            explosion.container.RenderAnimations();
        }
        health.RenderHealth();
        wave.RenderScore();
    }

    public void UpdateState() {
        UpdateEnemies();
        player.Move();
        IterateShots();
        if (!health.GetGameOver()) {
            move.MoveEnemies(enemies);
            wave.NextWave();
        }
        UpdateEnemies();
        IterateHealth();
    }
    
    public void UpdateEnemies() {
        if (waveNum != wave.num) {
            enemies = wave.GetEnemies();
            waveNum = wave.num;
            move = wave.GetMove();
        }
        IterateHealth();
    }

    private void IterateHealth() {
        enemies.Iterate(enemy =>{
            if (enemy.Shape.Position.Y + enemy.Shape.Extent.Y <= 0.0f) {
                health.LoseHealth();
                enemy.DeleteEntity();
                explosion.AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
            }
        });
    }
    private void KeyPress(KeyboardKey key) {
        switch (key){
            case KeyboardKey.Escape:
                GalagaBus.GetBus().RegisterEvent(
                    new GameEvent {EventType = GameEventType.WindowEvent, Message = "CLOSE_GAME"});
                break;
            case KeyboardKey.Left:
                GalagaBus.GetBus().RegisterEvent(
                    new GameEvent {EventType = GameEventType.PlayerEvent, Message = "MOVE",
                    StringArg1 = "LEFT"}
                );
                break;
            case KeyboardKey.Right:
                GalagaBus.GetBus().RegisterEvent(
                    new GameEvent {EventType = GameEventType.PlayerEvent, Message = "MOVE",
                    StringArg1 = "RIGHT"}
                );
                break;
            case KeyboardKey.Up:
                GalagaBus.GetBus().RegisterEvent(
                    new GameEvent {EventType = GameEventType.PlayerEvent, Message = "MOVE",
                    StringArg1 = "UP"}
                );
                break;
            case KeyboardKey.Down:
                GalagaBus.GetBus().RegisterEvent(
                    new GameEvent {EventType = GameEventType.PlayerEvent, Message = "MOVE",
                    StringArg1 = "DOWN"}
                );
                break;

        }
    }

    private void KeyRelease(KeyboardKey key) {
        switch (key){
            case KeyboardKey.Left:
                GalagaBus.GetBus().RegisterEvent(
                    new GameEvent {EventType = GameEventType.PlayerEvent, Message = "STOP_MOVE",
                    StringArg1 = "LEFT"}
                );
                break;
            case KeyboardKey.Right:
                GalagaBus.GetBus().RegisterEvent(
                    new GameEvent {EventType = GameEventType.PlayerEvent, Message = "STOP_MOVE",
                    StringArg1 = "RIGHT"}
                );
                break;
            case KeyboardKey.Up:
                GalagaBus.GetBus().RegisterEvent(
                    new GameEvent {EventType = GameEventType.PlayerEvent, Message = "STOP_MOVE",
                    StringArg1 = "UP"}
                );
                break;
            case KeyboardKey.Down:
                GalagaBus.GetBus().RegisterEvent(
                    new GameEvent {EventType = GameEventType.PlayerEvent, Message = "STOP_MOVE",
                    StringArg1 = "DOWN"}
                );
                break;  
            case KeyboardKey.Space:
                PlayerShot newShot = new PlayerShot(player.GetPosition() + 
                    new Vec2F (player.GetExtend().X/2, 0), playerShotImage);
                playerShots.AddEntity(newShot);
                break;
        }
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        switch (action){
            case KeyboardAction.KeyPress:
                KeyPress(key);
                break;
            case KeyboardAction.KeyRelease:
                KeyRelease(key);
                break;
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
                        explosion.AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                        }
                    }
                });
            }
        });
    }
}