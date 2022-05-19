using System.Collections.Generic;

public class PrimeInput
{
    private Stack<int> enteredPrimes = new Stack<int>();
    private RoundDisplay roundDisplay = RoundDisplay.instance;

    public void AddPrime(int prime)
    {
        enteredPrimes.Push(prime);
        ShowEnteredPrimes();
    }

    public void DeletePrime()
    {
        if (enteredPrimes.Count == 0)
            roundDisplay.ShowCannotDelete();
        else
        {
            enteredPrimes.Pop();
            ShowEnteredPrimes();
        }
    }

    public int[] GetInput()
    {
        return enteredPrimes.ToArray();
    }

    public void ClearPrimes()
    {
        enteredPrimes.Clear();
        ShowEnteredPrimes();
    }

    private void ShowEnteredPrimes()
    {
        roundDisplay.ShowEnteredPrimes(enteredPrimes);
    }
}
