using DIKUArcade.Entities;
using System;
using Galaga.Squadron;
using Galaga.MovementStrategy;
using System.Collections.Generic;
using DIKUArcade.Graphics;
using System.IO;
using DIKUArcade.Math;

namespace Galaga;

public class Wave {
    public int num;
    private Random rnd;
    private EntityContainer<Enemy> enemies;
    private IMovementStrategy move;
    public ISquadron squadron;
    private List<Image> enemyStridesBlue;
    private List<Image> enemyStridesRed;
    private float speed;
    private Text score;
    public Wave() {
        num = 1;
        rnd = new Random();
        
        enemyStridesBlue = ImageStride.CreateStrides (4, Path.Combine("Assets", "Images", "BlueMonster.png"));
        enemyStridesRed = ImageStride.CreateStrides (2, Path.Combine("Assets", "Images", "RedMonster.png"));
        
        squadron = new LineSquadron();
        squadron.CreateEnemies(enemyStridesBlue, enemyStridesRed);
        
        enemies = squadron.Enemies;
        move = new Down();
        speed = 0.0003f;

        score = new Text (num.ToString(), new Vec2F(0.0f,0.6f), new Vec2F(0.3f,0.4f));
        score.SetColor(System.Drawing.Color.Coral);
    }

    public void NextWave() {
        if (enemies.CountEntities() <= 0) {
            num ++;
            score.SetText(num.ToString());
            switch (rnd.Next(2)) {
                case 0:
                    move = new Down();
                    break;
                case 1:
                    move = new ZigZagDown();
                    break;
            }
            switch (rnd.Next(5)) {
                case 0:
                    squadron = new LineSquadron();
                    squadron.CreateEnemies(enemyStridesBlue, enemyStridesRed);
                    enemies = squadron.Enemies;
                    break;
                case 1:
                    squadron = new CheckeredSquadron();
                    squadron.CreateEnemies(enemyStridesBlue, enemyStridesRed);
                    enemies = squadron.Enemies;
                    break;
                case 2:
                    squadron = new PyramidSquadron();
                    squadron.CreateEnemies(enemyStridesBlue, enemyStridesRed);
                    enemies = squadron.Enemies;
                    break;
                case 3:
                    squadron = new SquareSquadron();
                    squadron.CreateEnemies(enemyStridesBlue, enemyStridesRed);
                    enemies = squadron.Enemies;
                    break;
                case 4:
                    squadron = new VSquadron();
                    squadron.CreateEnemies(enemyStridesBlue, enemyStridesRed);
                    enemies = squadron.Enemies;
                    break;
            }
            move.UpdateSpeed(num * speed);
        }
    }

    public ISquadron GetSquadron() {
        return squadron;
    }

    public EntityContainer<Enemy> GetEnemies() {
        return enemies;
    }

    public IMovementStrategy GetMove() {
        return move;
    }

    public void RenderScore() {
        score.RenderText();
    }
}