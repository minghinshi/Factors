using System;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager
{
    private NumberManager numberManager;
    private TimeManager timeManager;
    private ScoreManager scoreManager;
    private RoundDisplay roundDisplay;
    private ResultDisplay resultDisplay;

    private int maxPrime = 23;

    public RoundManager(EventHandler TickingEventHandler) {
        InitializeObjects(TickingEventHandler);
        ConnectEvents();
    }

    private void InitializeObjects(EventHandler TickingEventHandler) {
        roundDisplay = new RoundDisplay();
        resultDisplay = new ResultDisplay();
        numberManager = new NumberManager(maxPrime, roundDisplay);
        timeManager = new TimeManager(60f, TickingEventHandler, roundDisplay);
        scoreManager = new ScoreManager(roundDisplay);
    }

    private void ConnectEvents() {
        numberManager.FactorCheckedEventHandler += OnFactorChanged;
        numberManager.NumberClearedEventHandler += AwardClearNumber;
        timeManager.RoundEndingEventHandler += EndRound;
    }

    public void StartRound() {
        SetNewNumber();
    }

    public void EndRound(object sender, EventArgs e) {
        resultDisplay.DisplayResults(
            scoreManager.Score,
            timeManager.TimeElapsed,
            numberManager.AnsweredNumbers,
            numberManager.LargestNumber,
            scoreManager.HighestScoringNumber);
    }

    //Start number
    public void SetNewNumber()
    {
        numberManager.SetNewNumber();
        scoreManager.ResetNumberScore();
    }

    private void OnFactorChanged(object sender, FactorCheckedEventArgs args)
    {
        if (args.IsCorrect) AwardCorrectFactor(args.Factor);
        else PunishWrongAnswer();
    }

    public void AwardCorrectFactor(int factor) {
        scoreManager.AwardScore(factor);
    }

    public void PunishWrongAnswer()
    {
        timeManager.ChangeTimeLeftBy(-3f);
        roundDisplay.ShowIncorrect();
    }

    //End number
    public void AwardClearNumber(object sender, NumberClearedEventArgs args)
    {
        if (args.IsPerfectClear) AwardPerfectClear();
        scoreManager.CheckHighestScoring(args.Number);
        SetNewNumber();
    }

    public void AwardPerfectClear()
    {
        scoreManager.DoubleScore();
        timeManager.ChangeTimeLeftBy(3f);
        roundDisplay.ShowPerfect();
    }

    public void UpdateStats()
    {
        scoreManager.ApplyScoreChange();
        timeManager.ApplyTimeChange();
    }
}
