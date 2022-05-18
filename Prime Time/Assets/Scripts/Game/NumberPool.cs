using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NumberPool
{
    private int maxPrime;
    private int maxNumber;
    private HashSet<int> allNumbers;
    private HashSet<int> numbersInPool;

    public NumberPool(int maxPrime, int maxNumber)
    {
        this.maxPrime = maxPrime;
        this.maxNumber = maxNumber;
        allNumbers = GetCompositeNumbers();
        numbersInPool = new HashSet<int>(allNumbers);
    }

    public int DrawNumber()
    {
        int number = numbersInPool.ElementAt(Random.Range(0, numbersInPool.Count));
        numbersInPool.Remove(number);
        return number == 1 ? DrawNumber() : number;
    }

    public void Expand()
    {
        maxNumber *= 2;
        foreach (int prime in Helper.GetPrimes(maxPrime))
            allNumbers.UnionWith(MultiplySetBy(allNumbers, prime));
        HashSet<int> newNumbers = new HashSet<int>(allNumbers);
        newNumbers.RemoveWhere(x => x * 2 <= maxNumber);
        numbersInPool.UnionWith(newNumbers);
    }

    private HashSet<int> GetCompositeNumbers()
    {
        HashSet<int> output = new HashSet<int> { 1 };
        foreach (int prime in Helper.GetPrimes(maxPrime))
            output = InsertMultiplesOf(output, prime);
        return output;
    }

    private HashSet<int> InsertMultiplesOf(HashSet<int> numbers, int prime)
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

    private HashSet<int> MultiplySetBy(HashSet<int> numbers, int prime)
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
