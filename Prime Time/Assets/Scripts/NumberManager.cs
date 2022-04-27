using System.Collections.Generic;

//This class handles the big number at the middle of the screen.
public class NumberManager
{
    private AudioModule audioModule;
    private RoundDisplay roundDisplay;

    private int currentNumber;
    private bool isPerfectClear = true;
    private int answeredNumbers;
    private int largestNumber = 0;
    private NumberPool numberPool;

    public void SetPoolOfNumbers(int maxPrime)
    {
        numberPool = new NumberPool(maxPrime, 64);
    }

    public void SetNewNumber()
    {
        SetNumber(numberPool.DrawNumber());
    }

    public void SetNumber(int number)
    {
        currentNumber = number;
        isPerfectClear = true;
        roundDisplay.DisplayNumber(number);
    }

    public void CheckAnswer(Stack<int> primes)
    {
        ReduceNumber(primes);
        if (currentNumber == 1)
            ClearNumber();
        else
            FailClear();
        roundDisplay.DisplayNumber(currentNumber);
    }

    public void ClearNumber()
    {
        if (answeredNumbers % 20 == 0) numberPool.Expand();
        audioModule.PlayCorrect();
        SetNewNumber();
    }

    public void FailClear()
    {
        isPerfectClear = false;
        audioModule.PlayClick();
    }

    public void ReduceNumber(Stack<int> primes)
    {
        while (primes.Count != 0)
            TryToDivideBy(primes.Pop());
    }

    public bool TryToDivideBy(int prime)
    {
        if (currentNumber % prime == 0)
        {
            currentNumber /= prime;
            return true;
        }
        else
        {
            isPerfectClear = false;
            return false;
        }
    }

    public void CheckLargestNumber()
    {
        if (currentNumber > largestNumber)
            largestNumber = currentNumber;
    }

    public int GetNumber { get => currentNumber; }

    public int AnsweredNumbers { get => answeredNumbers; }

    public int LargestNumber { get => largestNumber; }

    public bool IsPerfectClear { get => isPerfectClear; }
}
