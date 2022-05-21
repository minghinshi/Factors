public class Attempt
{
    private FinalPrimeInputs primeInputs;
    private CompositeNumber newNumber;

    public Attempt(FinalPrimeInputs primeInputs, CompositeNumber currentNumber)
    {
        this.primeInputs = primeInputs;
        newNumber = primeInputs.CheckAgainst(currentNumber);
        RoundDisplay.instance.ShowNumber(newNumber);
    }

    public FinalPrimeInputs GetPrimeInputs() {
        return primeInputs;
    }

    public CompositeNumber GetNewNumber()
    {
        return newNumber;
    }

    public bool IsPerfect()
    {
        return primeInputs.IsAllCorrect() && newNumber.IsUnity();
    }
}
