using System.Collections.Generic;

public class Round
{
    private TimeManager timeManager;
    private ScoreManager scoreManager;
    private RoundSettings roundSettings;
    private AudioHandler audioModule;
    private NumberPool numberPool;
    private RoundResults roundResults;
    private CurrentPrimeInputs primeInput;

    private List<Subround> subrounds = new List<Subround>();

    public ScoreManager ScoreManager { get => scoreManager; }
    public TimeManager TimeManager { get => timeManager; }
    public CurrentPrimeInputs PrimeInput { get => primeInput; }

    public Round(Game gameHandler)
    {
        InitializeObjects(gameHandler);
        StartNewSubround();
    }

    private void InitializeObjects(Game gameHandler)
    {
        roundSettings = RoundSettingsInput.instance.GetRoundSettings();

        timeManager = new TimeManager(60f, gameHandler, this);
        scoreManager = new ScoreManager();
        audioModule = AudioHandler.instance;
        numberPool = new NumberPool(roundSettings.PrimeRange, 64);
        primeInput = new CurrentPrimeInputs();
    }

    public PrimeRange GetPrimeRange() {
        return roundSettings.PrimeRange;
    }

    public void MakeAttempt()
    {
        GetCurrentSubround().MakeAttempt(primeInput);
        primeInput.ClearPrimes();
        AttemptPerformance performance = new AttemptPerformance(this);
        performance.Evaluate();
        if (GetCurrentSubround().IsCleared())
            EndSubround();
    }

    public void EndRound()
    {
        if (roundResults == null)
            roundResults = new RoundResults(scoreManager.Score, timeManager.TimeElapsed, subrounds.ToArray());
    }

    private void StartNewSubround()
    {
        CompositeNumber startingNumber = numberPool.DrawNumber();
        subrounds.Add(new Subround(startingNumber));
    }

    public Subround GetCurrentSubround()
    {
        return subrounds[subrounds.Count - 1];
    }

    private void EndSubround()
    {
        if (subrounds.Count % 20 == 0)
            numberPool.Expand();
        audioModule.PlayCorrect();
        StartNewSubround();
    }
}