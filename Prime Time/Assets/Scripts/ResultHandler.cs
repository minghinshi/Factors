using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultHandler : MonoBehaviour
{
    public static ResultHandler instance;
    private RoundManager roundManager;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text timeElapsedText;
    [SerializeField] private Text numbersFactoredText;
    [SerializeField] private Text largestNumberText;
    [SerializeField] private Text highestScoringText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        roundManager = RoundManager.instance;
    }

    public void DisplayResults() {
        scoreText.text = roundManager.GetScore().ToString();
        timeElapsedText.text = "Game lasted " + roundManager.GetTimeElapsed().ToString("F1") + "s";
        numbersFactoredText.text = roundManager.GetNumberOfAnsweredNumbers().ToString() + " numbers factored";
        largestNumberText.text = "Largest number - " + roundManager.GetLargestNumber().ToString();
        highestScoringText.text = "Highest scoring number - " + roundManager.GetHighestScoringNumber().ToString();
    }
}
