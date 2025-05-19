public class RoundSettings
{
    private PrimeRange primeRange;

    public PrimeRange PrimeRange { get => primeRange; set => primeRange = value; }

    public RoundSettings()
    {
        primeRange = new PrimeRange(7);
    }

    private RoundSettings(PrimeRange primeRange)
    {
        this.primeRange = primeRange;
    }

    public RoundSettings GetClone()
    {
        return new RoundSettings(primeRange);
    }
}
