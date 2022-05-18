public class FactoringAttempt
{
    int originalNumber;
    int newNumber;
    int[] enteredPrimes;
    bool[] arePrimesCorrect;

    public int NewNumber { get => newNumber; }
    public int[] EnteredPrimes { get => enteredPrimes; }
    public bool[] ArePrimesCorrect { get => arePrimesCorrect; }

    public FactoringAttempt(int number, int[] primes)
    {
        originalNumber = number;
        newNumber = number;
        enteredPrimes = primes;
        arePrimesCorrect = new bool[primes.Length];

        ReduceNumber();
    }

    public int GetCountOfCorrectPrimes()
    {
        int count = 0;
        foreach (bool correct in arePrimesCorrect)
            if (correct) count++;
        return count;
    }

    public int GetCountOfIncorrectPrimes()
    {
        return enteredPrimes.Length - GetCountOfCorrectPrimes();
    }

    public bool IsAllCorrect()
    {
        return GetCountOfIncorrectPrimes() == 0;
    }

    public bool HasWrongAnswers()
    {
        return !IsAllCorrect();
    }

    public int GetScore()
    {
        int score = 0;
        for (int i = 0; i < EnteredPrimes.Length; i++)
            if (ArePrimesCorrect[i]) score += EnteredPrimes[i];
        score *= GetCountOfCorrectPrimes();
        return score;
    }

    public float GetTimePenalty(float penaltyPerWrongAnswer)
    {
        return penaltyPerWrongAnswer * GetCountOfIncorrectPrimes();
    }

    private void ReduceNumber()
    {
        for (int i = 0; i < enteredPrimes.Length; i++)
        {
            int prime = enteredPrimes[i];
            arePrimesCorrect[i] = newNumber % prime == 0;
            if (arePrimesCorrect[i]) newNumber /= prime;
        }
    }
}
