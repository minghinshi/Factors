public class AttemptPerformance
{
    private Subround subround;
    private FinalPrimeInputs answer;

    private RoundDisplay roundDisplay;
    private ScoreManager scoreManager;
    private TimeManager timeManager;

    public AttemptPerformance(Round round) {
        subround = round.GetCurrentSubround();
        answer = subround.GetLatestAttempt().GetPrimeInputs();

        roundDisplay = RoundDisplay.instance;
        scoreManager = round.ScoreManager;
        timeManager = round.TimeManager;
    }

    public void Evaluate() {
        scoreManager.AwardScore(answer);
        if (answer.HasWrongAnswers())
            OnAnswerIncorrect();
        if (subround.IsPerfect())
            OnAnswerPerfect();
    }

    private void OnAnswerIncorrect() {
        timeManager.PunishWrongAnswer(answer);
        roundDisplay.ShowIncorrect();
    }

    private void OnAnswerPerfect() {
        timeManager.ChangeTimeLeftBy(3f);
        roundDisplay.ShowPerfect();
    }
}
