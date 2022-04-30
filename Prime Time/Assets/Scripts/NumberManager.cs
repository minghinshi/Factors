using System;
using System.Collections.Generic;

//This class handles the big number at the middle of the screen.
public class NumberManager
{
    private AudioModule audioModule;
    private RoundDisplay roundDisplay;
    private NumberPool numberPool;
    private InputHandler inputHandler;

    private int currentNumber;
    private bool isPerfectClear = true;
    private int answeredNumbers;
    private int largestNumber = 0;

    public event EventHandler<FactorCheckedEventArgs> FactorCheckedEventHandler;
    public event EventHandler<NumberClearedEventArgs> NumberClearedEventHandler;

    public NumberManager(int maxPrime, RoundDisplay roundDisplay)
    {
        audioModule = AudioModule.instance;
        this.roundDisplay = roundDisplay;
        numberPool = new NumberPool(maxPrime, 64);
        inputHandler = new InputHandler(maxPrime, this, roundDisplay);
    }

    public void SetNewNumber()
    {
        SetNumber(numberPool.DrawNumber());
    }

    public void SetNumber(int number)
    {
        currentNumber = number;
        isPerfectClear = true;
        roundDisplay.ShowNumber(number);
    }

    public void CheckAnswer(Stack<int> primes)
    {
        ReduceNumber(primes);
        if (currentNumber == 1) ClearNumber();
        else NumberNotCleared();
        roundDisplay.ShowNumber(currentNumber);
    }

    public void ClearNumber()
    {
        InvokeNumberClearedEvent();
        if (answeredNumbers % 20 == 0) numberPool.Expand();
        audioModule.PlayCorrect();
        CheckLargestNumber();
        SetNewNumber();
    }

    private void InvokeNumberClearedEvent()
    {
        NumberClearedEventArgs args = new NumberClearedEventArgs
        {
            Number = currentNumber,
            IsPerfectClear = isPerfectClear
        };
        NumberClearedEventHandler?.Invoke(this, args);
    }

    public void NumberNotCleared()
    {
        isPerfectClear = false;
        audioModule.PlayClick();
    }

    public void ReduceNumber(Stack<int> primes)
    {
        while (primes.Count != 0) CheckFactor(primes.Pop());
    }

    public void CheckFactor(int prime)
    {
        bool isCorrect = (currentNumber % prime == 0);
        if (isCorrect) currentNumber /= prime;
        else isPerfectClear = false;
        InvokeFactorCheckedEvent(prime, isCorrect);
    }

    public void InvokeFactorCheckedEvent(int prime, bool isCorrect)
    {
        FactorCheckedEventArgs args = new FactorCheckedEventArgs
        {
            Factor = prime,
            IsCorrect = isCorrect
        };
        FactorCheckedEventHandler?.Invoke(this, args);
    }

    public void CheckLargestNumber()
    {
        if (currentNumber > largestNumber)
            largestNumber = currentNumber;
    }

    public int AnsweredNumbers { get => answeredNumbers; }

    public int LargestNumber { get => largestNumber; }
}

public class FactorCheckedEventArgs : EventArgs
{
    public int Factor { get; set; }
    public bool IsCorrect { get; set; }
}

public class NumberClearedEventArgs : EventArgs
{
    public int Number { get; set; }
    public bool IsPerfectClear { get; set; }
}
