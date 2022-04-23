using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;

    private int score;
    private int scoreToAward;

    private float timeLeft = 10f;
    private float timeChange;
    private float maxTimeThisRound = 10f;

    private bool isPerfectClear = true;

    [SerializeField] private Text scoreDisplay;
    [SerializeField] private Text timeDisplay;
    [SerializeField] private Slider timerBar;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        ChangeTimeLeft(-Time.deltaTime);
    }

    public void ResetPerfectClear()
    {
        isPerfectClear = true;
    }

    public void FailPerfectClear()
    {
        isPerfectClear = false;
    }

    public void PunishWrongAnswer()
    {
        Debug.Log("Incorrect!");
        FailPerfectClear();
        timeChange -= 2.5f;
    }

    public void AwardCorrectFactor()
    {
        Debug.Log("Factor is correct.");
        scoreToAward++;
    }

    public void AwardClearNumber()
    {
        Debug.Log("Correct!");
        if (isPerfectClear)
        {
            scoreToAward *= 2;
            timeChange += 5f;
        }
        else
        {
            timeChange += 2.5f;
        }
        ResetPerfectClear();
    }

    public void ChangeScore(int delta)
    {
        score += delta;
        scoreDisplay.text = score.ToString();
    }

    public void ChangeTimeLeft(float delta)
    {
        timeLeft += delta;
        if (timeLeft > maxTimeThisRound)
            maxTimeThisRound = timeLeft;
        timeDisplay.text = timeLeft.ToString("F1");
        timerBar.value = timeLeft / maxTimeThisRound;
    }

    public void UpdateStats()
    {
        ChangeScore(scoreToAward);
        ChangeTimeLeft(timeChange);
        scoreToAward = 0;
        timeChange = 0;
    }

}
