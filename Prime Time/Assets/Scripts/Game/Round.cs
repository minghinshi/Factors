using System.Collections.Generic;

public class Round
{
    private TimeManager timeManager;
    private ScoreManager scoreManager;
    private RoundDisplay roundDisplay;
    private RoundSettings roundSettings;
    private AudioHandler audioModule;
    private NumberPool numberPool;
    private RoundResults roundResults;

    private List<Subround> subrounds = new List<Subround>();

    public Round(Game gameHandler)
    {
        InitializeObjects(gameHandler);
        StartNewSubround();
    }

    private void InitializeObjects(Game gameHandler)
    {
        roundSettings = RoundSettingsInput.instance.GetRoundSettings();

        roundDisplay = RoundDisplay.instance;
        timeManager = new TimeManager(60f, gameHandler, this);
        scoreManager = new ScoreManager();
        audioModule = AudioHandler.instance;
        numberPool = new NumberPool(roundSettings.MaxPrime, 64);
        new InputHandler(roundSettings.MaxPrime, this);
    }

    public void MakeAttempt(int[] primes)
    {
        FactoringAttempt factoringAttempt = GetCurrentSubround().MakeAttempt(primes);
        scoreManager.AwardScore(factoringAttempt.GetScore());
        if (factoringAttempt.HasWrongAnswers())
            PunishWrongAnswer(factoringAttempt.GetCountOfIncorrectPrimes());
        if (GetCurrentSubround().IsCleared())
            EndSubround();
        audioModule.PlayClick();
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

    private Subround GetCurrentSubround()
    {
        return subrounds[subrounds.Count - 1];
    }

    private void PunishWrongAnswer(int wrongAnswerCount)
    {
        timeManager.ChangeTimeLeftBy(-3f * wrongAnswerCount);
        roundDisplay.ShowIncorrect();
    }

    private void EndSubround()
    {
        if (GetCurrentSubround().IsPerfect())
            AwardPerfectClear();
        if (subrounds.Count % 20 == 0)
            numberPool.Expand();
        audioModule.PlayCorrect();
        StartNewSubround();
    }

    private void AwardPerfectClear()
    {
        timeManager.ChangeTimeLeftBy(3f);
        roundDisplay.ShowPerfect();
    }
}
