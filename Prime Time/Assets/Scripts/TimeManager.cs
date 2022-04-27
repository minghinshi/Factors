using UnityEngine;

public class TimeManager
{
    private float timeLeft;
    private float timeChange;
    private float timeElapsed;
    private float maxTimeThisRound;

    public TimeManager(float timeAllowed) {
        timeLeft = timeAllowed;
        maxTimeThisRound = timeAllowed;
    }

    public void Tick() {
        timeElapsed += Time.deltaTime;
        timeLeft -= Time.deltaTime;
        RoundDisplay.instance.ShowTimeLeft(timeLeft, maxTimeThisRound);
    }

    public void ChangeTimeLeftBy(float delta) {
        timeChange += delta;
    }

    public void ApplyTimeChange() {
        if (timeChange == 0) return;
        timeLeft += timeChange;
        timeChange = 0;
        UpdateMaxTimeAchieved();
    }

    private void UpdateMaxTimeAchieved()
    {
        if (timeLeft > maxTimeThisRound)
            maxTimeThisRound = timeLeft;
    }

    public float TimeElapsed { get => timeElapsed; }

    public bool IsRoundEnded() {
        return timeLeft < 0;
    }
}
