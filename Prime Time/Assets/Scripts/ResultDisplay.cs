using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class ResultDisplay
{
    private Text scoreText;
    private Text descriptionText;

    public ResultDisplay() {
        scoreText = GameObject.Find("FinalScoreText").GetComponent<Text>();
        descriptionText = GameObject.Find("ResultDescriptionText").GetComponent<Text>();
    }

    public void DisplayResults(int score, float timeElapsed, int answeredNumbers, int largestNumber, int highestScoringNumber) {
        scoreText.text = score.ToString();
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Game lasted ").AppendFormat("0.0s", timeElapsed).AppendLine()
            .Append(answeredNumbers).Append(" numbers factored").AppendLine()
            .Append("Largest number - ").Append(largestNumber).AppendLine()
            .Append("Highest scoring number - ").Append(highestScoringNumber);
        descriptionText.text = stringBuilder.ToString();
    }
}
