using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundDisplayHandler : MonoBehaviour
{
    public static RoundDisplayHandler instance;

    [SerializeField] private Text numberDisplay;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timeLeftText;
    [SerializeField] private Text scoreChangeText;
    [SerializeField] private Text timeChangeText;
    [SerializeField] private Text commentText;
    [SerializeField] private Slider timerBar;

    private VisibilityModule scoreChangeVisibilityModule;
    private VisibilityModule timeChangeVisibilityModule;
    private VisibilityModule commentVisibilityModule;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        scoreChangeVisibilityModule = scoreChangeText.GetComponent<VisibilityModule>();
        timeChangeVisibilityModule = timeChangeText.GetComponent<VisibilityModule>();
        commentVisibilityModule = commentText.GetComponent<VisibilityModule>();
    }

    public void DisplayNumber(int number)
    {
        numberDisplay.text = number.ToString();
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
}
