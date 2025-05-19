using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoundDisplay : MonoBehaviour
{
    public static RoundDisplay instance;

    [SerializeField] private Text numberText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timeLeftText;
    [SerializeField] private Text scoreChangeText;
    [SerializeField] private Text timeChangeText;
    [SerializeField] private Text commentText;
    [SerializeField] private Text primesSelectedText;
    [SerializeField] private Slider timerBar;

    [SerializeField] private VisibilityModule scoreChangeVisibilityModule;
    [SerializeField] private VisibilityModule timeChangeVisibilityModule;
    [SerializeField] private VisibilityModule commentVisibilityModule;

    private void Awake()
    {
        instance = this;
    }

    public void ShowNumber(CompositeNumber compositeNumber)
    {
        numberText.text = compositeNumber.ToString();
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

    public void ShowPerfect()
    {
        ShowComment("Perfect!");
        SetCommentColor(Helper.GetColorFromRGB(243, 156, 18));
    }

    public void ShowIncorrect()
    {
        ShowComment("Incorrect!");
        SetCommentColor(Helper.GetColorFromRGB(231, 76, 60));
    }

    public void ShowCannotDelete()
    {
        ShowComment("Nothing to delete.");
        SetCommentColor(Helper.GetColorFromRGB(44, 62, 80));
    }

    public void ShowEnteredPrimes(Stack<PrimeInput> primes)
    {
        List<int> sortedListOfPrimes = new List<PrimeInput>(primes).ConvertAll(x => x.GetPrime());
        sortedListOfPrimes.Sort();
        primesSelectedText.text = Helper.Join(sortedListOfPrimes, " * ");
    }
}
