public class CompositeNumber
{
    int number;

    public CompositeNumber(int number)
    {
        this.number = number;
    }

    public int GetValue()
    {
        return number;
    }

    public bool IsDivisibleBy(PrimeInput primeInput)
    {
        return number % primeInput.GetPrime() == 0;
    }

    public bool IsUnity()
    {
        return number == 1;
    }

    public CompositeNumber DivideBy(PrimeInput primeInput)
    {
        return IsDivisibleBy(primeInput) ? new CompositeNumber(number / primeInput.GetPrime()) : this;
    }

    public override string ToString()
    {
        return number.ToString();
    }

    public bool IsLargerThan(CompositeNumber other) {
        return number > other.GetValue();
    }
}
