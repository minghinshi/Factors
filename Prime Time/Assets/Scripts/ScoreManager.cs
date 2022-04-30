using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager
{
    private RoundDisplay roundDisplay;

    private int score;
    private int scoreToAward;
    private int scoreAwardedForThisNumber;
    private int highestScore;
    private int highestScoringNumber;

    public ScoreManager(RoundDisplay roundDisplay) {
        this.roundDisplay = roundDisplay;
    }

    public void ResetNumberScore() {
        scoreAwardedForThisNumber = 0;
    }

    public void ApplyScoreChange() {
        if (scoreToAward == 0) return;
        roundDisplay.ShowScoreChange(scoreToAward);
        Score += scoreToAward;
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

    public int HighestScoringNumber { get => highestScoringNumber; }
    public int Score {
        get => score;
        set { 
            score = value;
            roundDisplay.ShowScore(score);
        }
    }
}
