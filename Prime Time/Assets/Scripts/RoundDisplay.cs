using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoundDisplay
{
    private Text numberText;
    private Text scoreText;
    private Text timeLeftText;
    private Text scoreChangeText;
    private Text timeChangeText;
    private Text commentText;
    private Text primesSelectedText;
    private Slider timerBar;

    private VisibilityModule scoreChangeVisibilityModule;
    private VisibilityModule timeChangeVisibilityModule;
    private VisibilityModule commentVisibilityModule;

    public RoundDisplay() {
        FindTexts();
        FindVisibilityModules();
    }

    private void FindTexts() {
        numberText = GameObject.Find("NumberText").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        timeLeftText = GameObject.Find("TimeLeftText").GetComponent<Text>();
        scoreChangeText = GameObject.Find("ScoreChangeText").GetComponent<Text>();
        timeChangeText = GameObject.Find("TimeChangeText").GetComponent<Text>();
        commentText = GameObject.Find("CommentText").GetComponent<Text>();
        primesSelectedText = GameObject.Find("PrimesSelectedText").GetComponent<Text>();
    }

    private void FindVisibilityModules() {
        scoreChangeVisibilityModule = scoreChangeText.GetComponent<VisibilityModule>();
        timeChangeVisibilityModule = timeChangeText.GetComponent<VisibilityModule>();
        commentVisibilityModule = commentText.GetComponent<VisibilityModule>();
    }

    public void ShowNumber(int number)
    {
        numberText.text = number.ToString();
    }

    public void ShowScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void ShowScoreChange(int delta)
    {
        scoreChangeText.text = "+" + delta.ToString();
        scoreChangeVisibilityModule.FadeOut();
    }

    public void ShowTimeLeft(float timeLeft, float maxTime)
    {
        timeLeftText.text = timeLeft.ToString("F1");
        timerBar.value = timeLeft / maxTime;
    }

    public void ShowTimeChange(float delta)
    {
        timeChangeText.text = (delta >= 0 ? "+" : "") + delta.ToString("F1");
        timeChangeVisibilityModule.FadeOut();
    }

    public void ShowComment(string comment)
    {
        commentText.text = comment;
        commentVisibilityModule.FadeOut();
    }

    public void SetCommentColor(Color color)
    {
        commentText.color = color;
    }

    public void ShowPerfect() {
        ShowComment("Perfect!");
        SetCommentColor(Helper.GetColorFromRGB(243, 156, 18));
    }

    public void ShowIncorrect() {
        ShowComment("Incorrect!");
        SetCommentColor(Helper.GetColorFromRGB(231, 76, 60));
    }

    public void ShowPrimesSelected(Stack<int> primes) {
        List<int> sortedListOfPrimes = new List<int>(primes);
        sortedListOfPrimes.Sort();
        primesSelectedText.text = Helper.InsertStringBetweenListItems(sortedListOfPrimes, " กั ");
    }
}
