using System;
using UnityEngine;

public class TimeManager
{
    private RoundDisplay roundDisplay;

    private float timeLeft;
    private float timeChange;
    private float timeElapsed;
    private float maxTimeThisRound;

    private bool roundEnded = false;

    public event EventHandler RoundEndingEventHandler;

    public TimeManager(float timeAllowed, EventHandler TickingEventHandler, RoundDisplay roundDisplay) {
        this.roundDisplay = roundDisplay;
        TickingEventHandler += Tick;
        TimeLeft = timeAllowed;
        maxTimeThisRound = timeAllowed;
    }

    public void Tick(object sender, EventArgs e) {
        timeElapsed += Time.deltaTime;
        TimeLeft -= Time.deltaTime;
    }

    public void ChangeTimeLeftBy(float delta) {
        timeChange += delta;
    }

    public void ApplyTimeChange() {
        if (timeChange == 0) return;
        TimeLeft += timeChange;
        timeChange = 0;
    }

    private void UpdateMaxTimeAchieved()
    {
        if (TimeLeft > maxTimeThisRound)
            maxTimeThisRound = TimeLeft;
    }

    public float TimeElapsed { get => timeElapsed; }
    public float TimeLeft { 
        get => timeLeft;
        set {
            timeLeft = value;
            UpdateMaxTimeAchieved();
            roundDisplay.ShowTimeLeft(TimeLeft, maxTimeThisRound);
            if (timeLeft <= 0 && !roundEnded) EndRound();
        }
    }

    private void EndRound() {
        RoundEndingEventHandler?.Invoke(this, EventArgs.Empty);
        roundEnded = true;
    }
}
