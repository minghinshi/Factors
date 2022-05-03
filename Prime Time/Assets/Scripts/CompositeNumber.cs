using System.Collections.Generic;
using UnityEngine;

public class CompositeNumber
{
    private Dictionary<int, int> primeFactorCounts;

    public CompositeNumber(int number, int maxPrime)
    {
        primeFactorCounts = GetPrimeFactors(number, maxPrime);
    }

    private Dictionary<int, int> GetPrimeFactors(int number, int maxPrime)
    {
        List<int> primes = Helper.GetPrimes(maxPrime);
        Dictionary<int, int> counts = new Dictionary<int, int>();
        foreach (int prime in primes)
            counts.Add(prime, CountFactors(number, prime));
        return counts;
    }

    private int CountFactors(int number, int prime)
    {
        int count = 0;
        while (number % prime == 0)
        {
            number /= prime;
            count++;
        }
        return count;
    }

    public bool[] ReduceNumber(int[] primes)
    {
        bool[] canReduce = new bool[primes.Length];
        for (int i = 0; i < primes.Length; i++)
            canReduce[i] = ReduceNumber(primes[i]);
        return canReduce;
    }

    private bool ReduceNumber(int prime)
    {
        if (!primeFactorCounts.ContainsKey(prime)) return false;
        //Just to see if -= works on dictionaries
        Debug.Log(primeFactorCounts[prime]);
        primeFactorCounts[prime] -= 1;
        Debug.Log(primeFactorCounts[prime]);
        if (primeFactorCounts[prime] == 0) primeFactorCounts.Remove(prime);
        return true;
    }

    public int GetNumber()
    {
        int number = 1;
        foreach (KeyValuePair<int, int> primeCountPair in primeFactorCounts)
            number *= Helper.Pow(primeCountPair.Key, primeCountPair.Value);
        return number;
    }
}
