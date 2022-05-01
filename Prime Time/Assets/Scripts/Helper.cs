using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class Helper
{
    public static HashSet<int> GetPrimes(int maxPrime)
    {
        HashSet<int> output = new HashSet<int>();
        Debug.Log("Finding primes...");
        bool[] isPrime = new bool[maxPrime + 1];
        for (int i = 0; i <= maxPrime; i++)
            isPrime[i] = true;
        for (int p = 2; p * p <= maxPrime; p++)
        {
            if (isPrime[p])
            {
                for (int i = p * p; i <= maxPrime; i += p)
                    isPrime[i] = false;
            }
        }
        for (int i = 2; i <= maxPrime; i++)
        {
            if (isPrime[i])
            {
                output.Add(i);
            }
        }
        return output;
    }

    public static string InsertStringBetweenListItems<T>(List<T> list, string filler)
    {
        StringBuilder displayTextBuilder = new StringBuilder();
        for (int i = 0; i < list.Count; i++)
        {
            displayTextBuilder.Append(list[i]);
            if (i != list.Count - 1)
            {
                displayTextBuilder.Append(filler);
            }
        }
        return displayTextBuilder.ToString();
    }

    public static Color GetColorFromRGB(int r, int g, int b)
    {
        return new Color(r / 255f, g / 255f, b / 255f);
    }
}
