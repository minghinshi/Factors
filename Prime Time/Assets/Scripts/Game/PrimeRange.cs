using System.Collections.Generic;

public class PrimeRange
{
    private int maximumValue;
    private int[] primesInRange;

    public PrimeRange(int maximumValue) {
        this.maximumValue = maximumValue;
    }

    public int[] GetPrimesInRange() {
        if (primesInRange == null) CalculatePrimesInRange();
        return primesInRange;
    }

    public int GetPrimeAt(int index) {
        return GetPrimesInRange()[index];
    }

    private void CalculatePrimesInRange()
    {
        List<int> output = new List<int>();
        bool[] isPrime = new bool[maximumValue + 1];
        for (int i = 0; i <= maximumValue; i++)
            isPrime[i] = true;
        for (int p = 2; p * p <= maximumValue; p++)
            if (isPrime[p])
                for (int i = p * p; i <= maximumValue; i += p)
                    isPrime[i] = false;
        for (int i = 2; i <= maximumValue; i++)
            if (isPrime[i])
                output.Add(i);
        primesInRange = output.ToArray();
    }
}
