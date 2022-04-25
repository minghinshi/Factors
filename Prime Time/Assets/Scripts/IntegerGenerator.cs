using System.Collections.Generic;

public class IntegerGenerator
{
    private int maxPrime;
    private int minNumber;
    private int maxNumber;

    public IntegerGenerator(int maxPrime, int minNumber, int maxNumber)
    {
        this.maxPrime = maxPrime;
        this.minNumber = minNumber;
        this.maxNumber = maxNumber;
    }

    public HashSet<int> GetCompositeNumbers()
    {
        HashSet<int> output = new HashSet<int> { 1 };
        foreach (int prime in Helper.GetPrimes(maxPrime))
        {
            output = InsertMultiplesOf(output, prime);
        }
        output.RemoveWhere(x => x < minNumber);
        return output;
    }

    public HashSet<int> InsertMultiplesOf(HashSet<int> numbers, int prime)
    {
        HashSet<int> output = new HashSet<int>();
        HashSet<int> newNumbers = new HashSet<int>(numbers);
        while (newNumbers.Count != 0)
        {
            output.UnionWith(newNumbers);
            newNumbers = MultiplySetBy(newNumbers, prime);
        }
        return output;
    }

    public HashSet<int> MultiplySetBy(HashSet<int> numbers, int prime)
    {
        HashSet<int> output = new HashSet<int>();
        foreach (int number in numbers)
        {
            int multiple = number * prime;
            if (multiple <= maxNumber) output.Add(multiple);
        }
        return output;
    }
}
