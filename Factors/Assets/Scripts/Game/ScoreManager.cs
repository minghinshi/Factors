public class ScoreManager
{
    private int score;
    private RoundDisplay roundDisplay;

    public int Score
    {
        get => score;
        set
        {
            score = value;
            roundDisplay.ShowScore(score);
        }
    }

    public ScoreManager()
    {
        roundDisplay = RoundDisplay.instance;
        Score = 0;
    }

    public void AwardScore(FinalPrimeInputs answer) {
        AwardScore(answer.GetScore());
    }

    private void AwardScore(int delta)
    {
        if (delta == 0) return;
        Score += delta;
        roundDisplay.ShowScoreChange(delta);
    }
}
