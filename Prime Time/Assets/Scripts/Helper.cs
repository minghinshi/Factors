using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class Helper
{
    public static List<int> GetPrimes(int maxPrime)
    {
        List<int> output = new List<int>();
        bool[] isPrime = new bool[maxPrime + 1];
        for (int i = 0; i <= maxPrime; i++)
            isPrime[i] = true;
        for (int p = 2; p * p <= maxPrime; p++)
            if (isPrime[p])
                for (int i = p * p; i <= maxPrime; i += p)
                    isPrime[i] = false;
        for (int i = 2; i <= maxPrime; i++)
            if (isPrime[i])
                output.Add(i);
        return output;
    }

    public static string InsertStringBetweenListItems<T>(List<T> list, string filler)
    {
        StringBuilder displayTextBuilder = new StringBuilder();
        for (int i = 0; i < list.Count; i++)
        {
            displayTextBuilder.Append(list[i]);
            if (i != list.Count - 1) displayTextBuilder.Append(filler);
        }
        return displayTextBuilder.ToString();
    }

    public static Color GetColorFromRGB(int r, int g, int b)
    {
        return new Color(r / 255f, g / 255f, b / 255f);
    }

    public static int Pow(int x, int y)
    {
        if (y < 0) throw new System.Exception("Integer power function cannot handle negative powers.");
        int result = 1;
        for (int i = 0; i < y; i++) result *= x;
        return result;
    }
}
