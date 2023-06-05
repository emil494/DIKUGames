using System.Diagnostics;

namespace Breakout;

public class Timer {
    private Stopwatch timer;

    public Timer(){
        timer = new Stopwatch();
        timer.Start();
    }

    public double GetElapsedSeconds() {
        return timer.ElapsedMilliseconds / 1000.0;
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
}
