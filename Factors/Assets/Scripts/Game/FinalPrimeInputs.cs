public class FinalPrimeInputs
{
    private readonly PrimeInput[] primeInputs;

    public FinalPrimeInputs(PrimeInput[] primeInputs) {
        this.primeInputs = primeInputs;
    }

    public CompositeNumber CheckAgainst(CompositeNumber currentNumber)
    {
        CompositeNumber newNumber = currentNumber;
        foreach (PrimeInput primeInput in primeInputs)
        {
            primeInput.Check(newNumber);
            newNumber = newNumber.DivideBy(primeInput);
        }
        return newNumber;
    }

    public int GetCountOfCorrectPrimes()
    {
        int count = 0;
        foreach (PrimeInput primeInput in primeInputs)
            if (primeInput.IsCorrect()) count++;
        return count;
    }

    public int GetCountOfIncorrectPrimes() {
        return primeInputs.Length - GetCountOfCorrectPrimes();
    }

    public bool IsAllCorrect() {
        return GetCountOfIncorrectPrimes() == 0;
    }

    public bool HasWrongAnswers() {
        return !IsAllCorrect();
    }

    public int GetScore() {
        int score = 0;
        foreach (PrimeInput primeInput in primeInputs)
            score += primeInput.GetScore();
        score *= GetCountOfCorrectPrimes();
        return score;
    }

    public float GetTimePenalty() {
        return GetCountOfIncorrectPrimes() * -3f;
    }
}
