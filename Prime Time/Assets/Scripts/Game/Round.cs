using System.Collections.Generic;

public class Round
{
    private TimeManager timeManager;
    private ScoreManager scoreManager;
    private RoundSettings roundSettings;
    private AudioHandler audioModule; //Used to indicate correct answer
    private NumberPool numberPool;
    private RoundResults roundResults;
    private PrimeInput primeInput;

    private List<Subround> subrounds = new List<Subround>();

    public ScoreManager ScoreManager { get => scoreManager; }
    public TimeManager TimeManager { get => timeManager; }
    public PrimeInput PrimeInput { get => primeInput; }

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
        numberPool = new NumberPool(roundSettings.MaxPrime, 64);
        primeInput = new PrimeInput();
    }

    public int GetMaxPrime() {
        return roundSettings.MaxPrime;
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
        int startingNumber = numberPool.DrawNumber();
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
