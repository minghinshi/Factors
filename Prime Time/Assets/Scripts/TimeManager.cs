using System;
using UnityEngine;

public class TimeManager
{
    private Round round;
    private RoundDisplay roundDisplay;

    private float timeLeft;
    private float timeChange;
    private float timeElapsed;
    private float maxTimeThisRound;

    public TimeManager(float timeAllowed, Game gameHandler, Round round)
    {
        roundDisplay = RoundDisplay.instance;
        this.round = round;
        gameHandler.TickingEventHandler += Tick;
        TimeLeft = timeAllowed;
        maxTimeThisRound = timeAllowed;
    }

    public void Tick(object sender, EventArgs e)
    {
        timeElapsed += Time.deltaTime;
        TimeLeft -= Time.deltaTime;
    }

    public void ChangeTimeLeftBy(float delta)
    {
        timeChange += delta;
    }

    public void ApplyTimeChange()
    {
        if (timeChange == 0) return;
        roundDisplay.ShowTimeChange(timeChange);
        TimeLeft += timeChange;
        timeChange = 0;
    }

    private void UpdateMaxTimeAchieved()
    {
        if (TimeLeft > maxTimeThisRound)
            maxTimeThisRound = TimeLeft;
    }

    public float TimeElapsed { get => timeElapsed; }
    public float TimeLeft
    {
        get => timeLeft;
        set
        {
            timeLeft = value;
            UpdateMaxTimeAchieved();
            roundDisplay.ShowTimeLeft(value, maxTimeThisRound);
            if (timeLeft <= 0) round.EndRound();
        }
    }
}
