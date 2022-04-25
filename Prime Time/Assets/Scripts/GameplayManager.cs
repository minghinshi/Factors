using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    //Singleton
    public static GameplayManager instance;
    private RoundManager roundManager;
    private AudioModule audioModule;
    private RoundDisplayHandler roundDisplay;

    private int maxPrime = 23;
    private int currentNumber;
    private int maxNumberInPool = 64;
    private int totalNumbers;

    private HashSet<int> remainingNumbers;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        roundManager = GetComponent<RoundManager>();
        audioModule = AudioModule.instance;
        roundDisplay = RoundDisplayHandler.instance;
        StartRound();
    }

    private void StartRound() {
        ButtonGenerator.instance.GenerateButtons(maxPrime);
        remainingNumbers = new IntegerGenerator(maxPrime, 2, maxNumberInPool).GetCompositeNumbers();
        totalNumbers = remainingNumbers.Count;
        SetNewNumber();
    }

    public void SetNewNumber()
    {
        int seed = Random.Range(0, remainingNumbers.Count);
        int randomNumber = remainingNumbers.ElementAt(seed);
        SetNumber(randomNumber);
    }

    public void SetNumber(int number)
    {
        currentNumber = number;
        remainingNumbers.Remove(number);
        roundDisplay.DisplayNumber(number);
        roundManager.SetNewNumber(number);
    }

    public void ExpandNumberSet()
    {
        //TODO: use the existing int generator
        Debug.Log(totalNumbers);
        HashSet<int> newNumbers = new IntegerGenerator(maxPrime, maxNumberInPool + 1, 2 * maxNumberInPool).GetCompositeNumbers();
        maxNumberInPool *= 2;
        totalNumbers += newNumbers.Count;
        remainingNumbers.UnionWith(newNumbers);
        Debug.Log(totalNumbers);
    }

    public void CheckAnswer(Stack<int> primes)
    {
        ReduceNumber(primes);
        if (currentNumber == 1)
            ClearNumber();
        else
            FailClear();
        roundDisplay.DisplayNumber(currentNumber);
        roundManager.UpdateStats();
    }

    public void ClearNumber()
    {
        roundManager.AwardClearNumber();
        if (roundManager.GetNumberOfAnsweredNumbers() * 2 >= totalNumbers) ExpandNumberSet();
        audioModule.PlayCorrect();
        SetNewNumber();
    }

    public void FailClear() {
        roundManager.FailPerfectClear();
        audioModule.PlayClick();
    }

    public void ReduceNumber(Stack<int> primes)
    {
        while (primes.Count != 0)
            TryToDivideBy(primes.Pop());
    }

    public void TryToDivideBy(int prime)
    {
        if (currentNumber % prime == 0)
            DivideNumberBy(prime);
        else
            roundManager.PunishWrongAnswer();
    }

    public void DivideNumberBy(int prime)
    {
        currentNumber /= prime;
        roundManager.AwardCorrectFactor(prime);
    }
}
