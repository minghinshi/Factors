using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;


public class ResultDisplay : MonoBehaviour
{
    public static ResultDisplay instance;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text descriptionText;

    private void Awake()
    {
        instance = this;
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
