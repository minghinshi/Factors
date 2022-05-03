public class RoundSettings
{
    private int maxPrime;

    public int MaxPrime { get => maxPrime; set => maxPrime = value; }

    public RoundSettings()
    {
        maxPrime = 7;
    }

    public RoundSettings(int maxPrime)
    {
        this.maxPrime = maxPrime;
    }

    public RoundSettings GetClone()
    {
        return new RoundSettings(maxPrime);
    }
}
