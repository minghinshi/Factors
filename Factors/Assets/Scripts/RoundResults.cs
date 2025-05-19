using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class RoundResults
{
    private int score;
    private float timeElapsed;

    private Text scoreText;
    private Text descriptionText;
    private Subround[] subrounds;

    public RoundResults(int score, float timeElapsed, Subround[] subrounds)
    {
        this.score = score;
        this.timeElapsed = timeElapsed;
        this.subrounds = subrounds;

        scoreText = GameObject.Find("FinalScoreText").GetComponent<Text>();
        descriptionText = GameObject.Find("ResultDescriptionText").GetComponent<Text>();

        DisplayResults();
        PanelSwitcher.instance.ShowResultPanel();
    }

    private int GetAnsweredNumbersCount()
    {
        return subrounds.Length - 1;
    }

    private CompositeNumber GetLargestNumber()
    {
        CompositeNumber largestNumber = subrounds[0].GetStartingNumber();
        foreach (Subround subround in subrounds)
        {
            CompositeNumber number = subround.GetStartingNumber();
            if (subround.IsCleared() && number.IsLargerThan(largestNumber))
                largestNumber = number;
        }
        return largestNumber;
    }

    private CompositeNumber GetHighestScoringNumber()
    {
        Subround highestScoringSubround = subrounds[0];
        for (int i = 1; i < subrounds.Length; i++)
            if (subrounds[i].IsCleared() && subrounds[i].GetScore() > highestScoringSubround.GetScore())
                highestScoringSubround = subrounds[i];
        return highestScoringSubround.GetStartingNumber();
    }

    private void DisplayResults()
    {
        scoreText.text = score.ToString();
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Game lasted ").AppendFormat("{0:F1}s", timeElapsed).AppendLine()
            .Append(GetAnsweredNumbersCount()).Append(" numbers factored").AppendLine()
            .Append("Largest number - ").Append(GetLargestNumber()).AppendLine()
            .Append("Highest scoring number - ").Append(GetHighestScoringNumber());
        descriptionText.text = stringBuilder.ToString();
    }
}
