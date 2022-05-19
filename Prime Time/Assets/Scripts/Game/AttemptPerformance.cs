public class AttemptPerformance
{
    private Subround subround;
    private FactoringAttempt factoringAttempt;

    private RoundDisplay roundDisplay;
    private ScoreManager scoreManager;
    private TimeManager timeManager;

    public AttemptPerformance(Round round) {
        subround = round.GetCurrentSubround();
        factoringAttempt = subround.GetLatestAttempt();

        roundDisplay = RoundDisplay.instance;
        scoreManager = round.ScoreManager;
        timeManager = round.TimeManager;
    }

    public void Evaluate() {
        scoreManager.AwardScore(factoringAttempt.GetScore());
        if (factoringAttempt.HasWrongAnswers())
            OnAnswerIncorrect();
        if (subround.IsPerfect())
            OnAnswerPerfect();
    }

    private void OnAnswerIncorrect() {
        timeManager.ChangeTimeLeftBy(factoringAttempt.GetTimePenalty());
        roundDisplay.ShowIncorrect();
    }

    private void OnAnswerPerfect() {
        timeManager.ChangeTimeLeftBy(3f);
        roundDisplay.ShowPerfect();
    }
}
