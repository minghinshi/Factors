using System.Collections.Generic;

public class CurrentPrimeInputs
{
    private Stack<PrimeInput> enteredPrimes = new Stack<PrimeInput>();
    private RoundDisplay roundDisplay = RoundDisplay.instance;

    public CurrentPrimeInputs() {
        ShowEnteredPrimes();
    }

    public void AddPrime(int prime)
    {
        PrimeInput newInput = new PrimeInput(prime);
        enteredPrimes.Push(newInput);
        ShowEnteredPrimes();
    }

    public void DeletePrime()
    {
        if (StackIsEmpty())
            roundDisplay.ShowCannotDelete();
        else
        {
            enteredPrimes.Pop();
            ShowEnteredPrimes();
        }
    }

    public void ClearPrimes()
    {
        enteredPrimes.Clear();
        ShowEnteredPrimes();
    }

    public FinalPrimeInputs GetFinalizedAnswer() {
        return new FinalPrimeInputs(enteredPrimes.ToArray());
    }

    private void ShowEnteredPrimes()
    {
        roundDisplay.ShowEnteredPrimes(enteredPrimes);
    }

    private bool StackIsEmpty() {
        return enteredPrimes.Count == 0;
    }
}
