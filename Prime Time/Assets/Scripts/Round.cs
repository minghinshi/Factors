using System;

public class Round
{
    private NumberManager numberManager;
    private TimeManager timeManager;
    private ScoreManager scoreManager;
    private RoundDisplay roundDisplay;
    private ResultDisplay resultDisplay;
    private PanelSwitcher panelSwitcher;

    private int maxPrime = 23;
    private bool roundEnded = false;

    public Round(Game gameHandler)
    {
        InitializeObjects(gameHandler);
        ConnectEvents();
    }

    private void InitializeObjects(Game gameHandler)
    {
        roundDisplay = RoundDisplay.instance;
        resultDisplay = new ResultDisplay();
        numberManager = new NumberManager(maxPrime);
        timeManager = new TimeManager(60f, gameHandler, this);
        scoreManager = new ScoreManager();
        panelSwitcher = PanelSwitcher.instance;
    }

    private void ConnectEvents()
    {
        numberManager.FactorCheckedEventHandler += OnFactorChecked;
        numberManager.NumberCheckedEventHandler += UpdateStats;
        numberManager.NumberClearedEventHandler += AwardClearNumber;
    }

    public void StartRound()
    {
        numberManager.SetNewNumber();
    }

    public void EndRound()
    {
        if (roundEnded) return;
        resultDisplay.DisplayResults(scoreManager.Score, timeManager.TimeElapsed, numberManager.AnsweredNumbers, numberManager.LargestNumber, scoreManager.HighestScoringNumber);
        panelSwitcher.ShowResultPanel();
        roundEnded = true;
    }

    private void OnFactorChecked(object sender, FactorCheckedEventArgs args)
    {
        if (args.IsCorrect) AwardCorrectFactor(args.Factor);
        else PunishWrongAnswer();
    }

    public void AwardCorrectFactor(int factor)
    {
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
        scoreManager.ResetNumberScore();
    }

    public void AwardPerfectClear()
    {
        scoreManager.DoubleScore();
        timeManager.ChangeTimeLeftBy(3f);
        roundDisplay.ShowPerfect();
    }

    public void UpdateStats(object sender, EventArgs e)
    {
        scoreManager.ApplyScoreChange();
        timeManager.ApplyTimeChange();
    }
}
