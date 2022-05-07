using System.Collections.Generic;
using UnityEngine;

public class CompositeNumber
{
    private bool hasBeenFactored = false;
    private Dictionary<int, int> primeFactorCounts;

    public CompositeNumber(int number, int maxPrime)
    {
        primeFactorCounts = GetPrimeFactors(number, maxPrime);
    }

    private Dictionary<int, int> GetPrimeFactors(int number, int maxPrime)
    {
        List<int> primes = Helper.GetPrimes(maxPrime);
        Dictionary<int, int> counts = new Dictionary<int, int>();
        foreach (int prime in primes) if(number % prime == 0)   //To test if it is at least divisible once
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

    public FactoringAttempt AttemptToFactor(int[] primes) {
        bool isFirstAttempt = !hasBeenFactored;
        hasBeenFactored = true;
        bool[] arePrimesCorrect = GetReduceNumberResult(primes);
        return new FactoringAttempt(isFirstAttempt, primes, arePrimesCorrect, GetNumber());
    }

    public bool[] GetReduceNumberResult(int[] primes)
    {
        bool[] canReduce = new bool[primes.Length];
        for (int i = 0; i < primes.Length; i++)
            canReduce[i] = ReduceNumber(primes[i]);
        return canReduce;
    }

    private bool ReduceNumber(int prime)
    {
        if (!primeFactorCounts.ContainsKey(prime)) return false;
        Debug.Log(primeFactorCounts[prime]);    //Just to see if -= works on dictionaries
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
