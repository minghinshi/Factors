using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager
{
    private int score;
    private int scoreToAward;
    private int scoreAwardedForThisNumber;
    private int highestScore;
    private int highestScoringNumber;

    public void ResetNumberScore() {
        scoreAwardedForThisNumber = 0;
    }

    public void ApplyScoreChange() {
        if (scoreToAward == 0) return;
        score += scoreToAward;
        scoreToAward = 0;
    }

    public void AwardScore(int delta) {
        scoreToAward += delta;
        scoreAwardedForThisNumber += delta;
    }

    public void DoubleScore() {
        AwardScore(scoreToAward);
    }

    public void CheckHighestScoring(int number)
    {
        if (scoreAwardedForThisNumber > highestScore)
        {
            highestScore = scoreAwardedForThisNumber;
            highestScoringNumber = number;
        }
    }

    public int Score { get => score; }

    public int HighestScoringNumber { get => highestScoringNumber; }
}
