using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;

    private NumberManager numberManager;
    private TimeManager timeManager;
    private ScoreManager scoreManager;
    private InputHandler inputHandler;

    private RoundDisplay roundDisplay;
    private ResultDisplay resultDisplay;

    private int maxPrime = 23;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        roundDisplay = RoundDisplay.instance;
        resultDisplay = ResultDisplay.instance;

        numberManager = new NumberManager();
        timeManager = new TimeManager(60f);
        scoreManager = new ScoreManager();
        inputHandler = new InputHandler(numberManager, maxPrime);
    }

    private void Update()
    {
        timeManager.Tick();
        
    }

    public void StartRound() {
        numberManager.SetPoolOfNumbers(maxPrime);
        SetNewNumber();
    }

    public void EndRound() {
        resultDisplay.DisplayResults(
            scoreManager.Score,
            timeManager.TimeElapsed,
            numberManager.AnsweredNumbers,
            numberManager.LargestNumber,
            scoreManager.HighestScoringNumber);
    }

    //Start number
    public void SetNewNumber()
    {
        numberManager.SetNewNumber();
        scoreManager.ResetNumberScore();
    }

    public void PunishWrongAnswer()
    {
        timeManager.ChangeTimeLeftBy(-3f);
    }

    //End number
    public void ClearNumber()
    {
        if (numberManager.IsPerfectClear)
            AwardPerfectClear();
        numberManager.CheckLargestNumber();
        scoreManager.CheckHighestScoring(numberManager.GetNumber);
        SetNewNumber();
    }

    public void AwardPerfectClear()
    {
        scoreManager.DoubleScore();
        timeManager.ChangeTimeLeftBy(3f);
        roundDisplay.ShowPerfect();
    }

    public void UpdateStats()
    {
        scoreManager.ApplyScoreChange();
        timeManager.ApplyTimeChange();
    }
}
