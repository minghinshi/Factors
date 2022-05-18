using System.Collections.Generic;

public class Subround
{
    private List<int> numberSequence = new List<int>();
    private List<FactoringAttempt> attemptHistory = new List<FactoringAttempt>();
    private RoundDisplay roundDisplay = RoundDisplay.instance;

    public List<FactoringAttempt> AttemptHistory { get => attemptHistory; }

    public Subround(int startingNumber)
    {
        AddNewNumber(startingNumber);
    }

    public FactoringAttempt MakeAttempt(int[] primes)
    {
        FactoringAttempt newAttempt = new FactoringAttempt(GetCurrentNumber(), primes);
        attemptHistory.Add(newAttempt);
        AddNewNumber(newAttempt.NewNumber);
        return newAttempt;
    }

    public bool IsCleared()
    {
        return GetCurrentNumber() == 1;
    }

    public bool IsPerfect()
    {
        return GetAttemptCount() == 1 && numberSequence[1] == 1 && attemptHistory[0].IsAllCorrect();
    }

    public int GetStartingNumber()
    {
        return numberSequence[0];
    }

    public int GetCurrentNumber()
    {
        return numberSequence[numberSequence.Count - 1];
    }

    public int GetTotalScore()
    {
        int score = 0;
        foreach (FactoringAttempt attempt in attemptHistory)
            score += attempt.GetScore();
        return score;
    }

    private int GetAttemptCount()
    {
        return attemptHistory.Count;
    }

    private void AddNewNumber(int number)
    {
        numberSequence.Add(number);
        roundDisplay.ShowNumber(number);
    }
}
