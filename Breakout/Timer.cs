using System.Diagnostics;
using System;

namespace Breakout;

public class Timer {
    private int addedTime;
    private Stopwatch timer;

    public Timer(int startTime){
        timer = new Stopwatch();
        timer.Start();
        addedTime = startTime;
    }

    public double GetElapsedSeconds() {
        return timer.ElapsedMilliseconds / 1000.0;
    }

    public int RemaningTime(){
        return addedTime - Convert.ToInt32(GetElapsedSeconds());
    }

    public void RestartTimer() {
        timer.Restart();
    }

    public void PauseTimer() {
        timer.Stop();
    }

    public void ResumeTimer() {
        timer.Start();
    }

    public void AddTime(int time){
        addedTime += time;
    }

    public bool OutOfTime(){
        if (Convert.ToInt32(GetElapsedSeconds()) >= addedTime){
            return true;
        } else {
            return false;
        }
    }
}
