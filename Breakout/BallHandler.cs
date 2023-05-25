using DIKUArcade.Entities;
using Breakout.Blocks;
using System;
using System.IO;
using System.Collections.Generic;
namespace Breakout;

public class BallHandler{
    private EntityContainer Balls;
    public BallHandler(){
        Balls = new EntityContainer();
    }

    private void AddBall(Ball ball){
        Balls.AddEntity(ball);
    }

    
}