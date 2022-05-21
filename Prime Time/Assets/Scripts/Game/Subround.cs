using System;
using System.Collections.Generic;

public class Subround
{
    private CompositeNumber startingNumber;
    private List<Attempt> attempts = new List<Attempt>();

    public Subround(CompositeNumber startingNumber)
    {
        this.startingNumber = startingNumber;
        RoundDisplay.instance.ShowNumber(startingNumber);
    }

    public Attempt MakeAttempt(CurrentPrimeInputs primeInputs) {
        FinalPrimeInputs finalAnswer = primeInputs.GetFinalizedAnswer();
        Attempt attempt = new Attempt(finalAnswer, GetCurrentNumber());
        attempts.Add(attempt);
        return attempt;
    }

    public Attempt GetLatestAttempt() {
        if (attempts.Count == 0) return null;
        return attempts[attempts.Count - 1];
    }

    public bool IsCleared()
    {
        return GetCurrentNumber().IsUnity();
    }

    public bool IsPerfect()
    {
        return attempts[0].IsPerfect();
    }

    public CompositeNumber GetStartingNumber()
    {
        return startingNumber;
    }

    public int GetScore()
    {
        int score = 0;
        foreach (Attempt attempt in attempts)
            score += attempt.GetPrimeInputs().GetScore();
        return score;
    }

    private CompositeNumber GetCurrentNumber()
    {
        if (attempts.Count == 0) return startingNumber;
        else return GetLatestAttempt().GetNewNumber();
    }
}