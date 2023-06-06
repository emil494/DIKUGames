using System.Diagnostics;
using System;

namespace Breakout;

/// <summary>
/// A basic timer
/// </summary>
public class Timer {
    private int addedTime;
    private Stopwatch timer;

    public Timer(int startTime){
        timer = new Stopwatch();
        timer.Start();
        addedTime = startTime;
    }

    /// <summary>
    /// Calculates passed time into seconds
    /// </summary>
    /// <returns> Passed time in seconds as a double </returns>
    public double GetElapsedSeconds() {
        return timer.ElapsedMilliseconds / 1000.0;
    }

    /// <summary>
    /// Calculates the remaning time of the timer
    /// </summary>
    /// <returns> The remaning time as an int </returns>
    public int RemaningTime(){
        return addedTime - Convert.ToInt32(GetElapsedSeconds());
    }

    /// <summary>
    /// Restats the timer
    /// </summary>
    public void RestartTimer() {
        timer.Restart();
    }

    /// <summary>
    /// Pauses the timer
    /// </summary>
    public void PauseTimer() {
        timer.Stop();
    }

    /// <summary>
    /// Resumes the timer
    /// </summary>
    public void ResumeTimer() {
        timer.Start();
    }

    /// <summary>
    /// Adds additional time to the timer
    /// </summary>
    /// <param name="time"></param>
    public void AddTime(int time){
        addedTime += time;
    }

    /// <summary>
    /// Checks if the timer has run out
    /// </summary>
    /// <returns> True if the timer has run out, false otherwise </returns>
    public bool OutOfTime(){
        if (Convert.ToInt32(GetElapsedSeconds()) >= addedTime){
            return true;
        } else {
            return false;
        }
    }
}
