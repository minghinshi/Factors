using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;
    private RoundDisplayHandler roundDisplay;
    private GameManager gameManager;

    private int score;
    private int scoreToAward;
    private int scoreAwardedForThisNumber;
    private float timeLeft = 60f;
    private float timeChange;
    private float timeElapsed;
    private float maxTimeThisRound;
    private bool isPerfectClear = true;

    private int currentNumber;
    private int answeredNumbers;
    private int largestNumber = 0;
    private int highestScore = 0;
    private int highestScoringNumber;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        roundDisplay = RoundDisplayHandler.instance;
        gameManager = GameManager.instance;
        maxTimeThisRound = timeLeft;
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
            gameManager.EndRound();
        roundDisplay.ShowTimeLeft(timeLeft, maxTimeThisRound);
    }

    //Start number
    public void SetNewNumber(int number)
    {
        currentNumber = number;
        scoreAwardedForThisNumber = 0;
        isPerfectClear = true;
    }

    public void FailPerfectClear()
    {
        isPerfectClear = false;
    }

    public void PunishWrongAnswer()
    {
        FailPerfectClear();
        timeChange -= 3f;
        roundDisplay.ShowComment("Incorrect!");
        roundDisplay.SetCommentColor(Helper.GetColorFromRGB(231, 76, 60));
    }

    public void AwardCorrectFactor(int factor)
    {
        scoreToAward += factor;
        scoreAwardedForThisNumber += factor;
    }

    //End number
    public void AwardClearNumber()
    {
        if (isPerfectClear)
            AwardPerfectClear();
        answeredNumbers++;
        CheckLargestNumber();
        CheckHighestScoring();
    }

    private void CheckLargestNumber() {
        if (currentNumber > largestNumber)
            largestNumber = currentNumber;
    }

    private void CheckHighestScoring() {
        if (scoreAwardedForThisNumber > highestScore)
        {
            highestScore = scoreAwardedForThisNumber;
            highestScoringNumber = currentNumber;
        }
    }

    public void AwardPerfectClear()
    {
        scoreToAward *= 2;
        scoreAwardedForThisNumber *= 2;
        timeChange += 3f;
        roundDisplay.ShowComment("Perfect!");
        roundDisplay.SetCommentColor(Helper.GetColorFromRGB(243, 156, 18));
    }

    public void ChangeScore(int delta)
    {
        if (delta == 0) return;
        score += delta;
        roundDisplay.ShowScore(score);
        roundDisplay.ShowScoreChange(delta);
    }

    public void ChangeTimeLeft(float delta)
    {
        if (delta == 0) return;
        timeLeft += delta;
        UpdateMaxTimeAchieved();
        roundDisplay.ShowTimeLeft(timeLeft, maxTimeThisRound);
        roundDisplay.ShowTimeChange(delta);
    }

    private void UpdateMaxTimeAchieved()
    {
        if (timeLeft > maxTimeThisRound)
            maxTimeThisRound = timeLeft;
    }

    public void UpdateStats()
    {
        ChangeScore(scoreToAward);
        ChangeTimeLeft(timeChange);
        scoreToAward = 0;
        timeChange = 0;
    }

    public int GetScore() {
        return score;
    }

    public int GetLargestNumber() {
        return largestNumber;
    }

    public int GetNumberOfAnsweredNumbers()
    {
        return answeredNumbers;
    }

    public int GetHighestScoringNumber() {
        return highestScoringNumber;
    }

    public float GetTimeElapsed() {
        return timeElapsed;
    }
}
