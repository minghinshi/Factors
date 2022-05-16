using UnityEngine.UI;
using UnityEngine;
using System.Text;

public class RoundResults
{
    private int score;
    private float timeElapsed;

    private Text scoreText;
    private Text descriptionText;
    private Subround[] subrounds;

    public RoundResults(int score, float timeElapsed, Subround[] subround) {
        this.score = score;
        this.timeElapsed = timeElapsed;
        scoreText = GameObject.Find("FinalScoreText").GetComponent<Text>();
        descriptionText = GameObject.Find("ResultDescriptionText").GetComponent<Text>();

        DisplayResults();
        PanelSwitcher.instance.ShowResultPanel();
    }

    private int GetAnsweredNumbersCount()
    {
        return subrounds.Length - 1;
    }

    private int GetLargestNumber()
    {
        int largestNumber = 0;
        foreach (Subround subround in subrounds)
            if (subround.IsCleared() && subround.GetStartingNumber() > largestNumber)
                largestNumber = subround.GetStartingNumber();
        return largestNumber;
    }

    private int GetHighestScoringNumber()
    {
        Subround highestScoringSubround = subrounds[0];
        for (int i = 1; i < subrounds.Length; i++)
            if (subrounds[i].IsCleared() && subrounds[i].GetTotalScore() > highestScoringSubround.GetTotalScore())
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
