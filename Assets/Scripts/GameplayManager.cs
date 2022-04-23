using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameplayManager : MonoBehaviour
{
    //Singleton
    public static GameplayManager instance;

    private int maxPrime = 13;
    private int currentNumber;
    private int maxNumber = 64;
    private int answeredNumbers;
    private int totalNumbers;

    private Stack<int> primesEntered = new Stack<int>();
    private HashSet<int> remainingNumbers;
    private RoundManager roundManager;

    [SerializeField] private Text numberDisplay;
    [SerializeField] private Text primesDisplay;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        roundManager = GetComponent<RoundManager>();
        ButtonGenerator.instance.GenerateButtons(maxPrime);
        remainingNumbers = new IntegerGenerator(maxPrime, 2, maxNumber).GetCompositeNumbers();
        totalNumbers = remainingNumbers.Count;
        SetNewNumber();
    }

    public void ClearNumber() {
        roundManager.AwardClearNumber();
        answeredNumbers++;
        if (answeredNumbers * 2 >= totalNumbers) ExpandNumberSet();
        SetNewNumber();
    }

    public void SetNewNumber() {
        int seed = UnityEngine.Random.Range(0, remainingNumbers.Count);
        int randomNumber = remainingNumbers.ElementAt(seed);
        SetNumber(randomNumber);
    }

    public void SetNumber(int number) {
        currentNumber = number;
        remainingNumbers.Remove(number);
        DisplayCurrentNumber();
    }

    public void ExpandNumberSet() {
        //TODO: use the existing int generator
        Debug.Log(totalNumbers);
        HashSet<int> newNumbers = new IntegerGenerator(maxPrime, maxNumber + 1, 2 * maxNumber).GetCompositeNumbers();
        maxNumber *= 2;
        totalNumbers += newNumbers.Count;
        remainingNumbers.UnionWith(newNumbers);
        Debug.Log(totalNumbers);
    }

    public void AddPrime(int prime) {
        primesEntered.Push(prime);
        DisplayPrimesEntered();
    }

    public void DeletePrime() {
        try
        {
            primesEntered.Pop();
            DisplayPrimesEntered();
        }
        catch (InvalidOperationException)
        {
            Debug.Log("No primes to delete!");
        }
    }

    public void CheckAnswer() {
        ReduceNumber();
        if (currentNumber == 1)
            ClearNumber();
        else
            roundManager.FailPerfectClear();
        DisplayCurrentNumber();
        DisplayPrimesEntered();
        roundManager.UpdateStats();
    }

    public void ReduceNumber() {
        while (primesEntered.Count != 0)
            TryToDivideBy(primesEntered.Pop());
    }

    public void TryToDivideBy(int prime) {
        if (currentNumber % prime == 0)
        {
            currentNumber /= prime;
            roundManager.AwardCorrectFactor();
        }
        else
            roundManager.PunishWrongAnswer();
    }

    public List<int> GetSortedListOfPrimes() {
        List<int> output = new List<int>(primesEntered);
        output.Sort();
        return output;
    }

    public void DisplayCurrentNumber() {
        numberDisplay.text = currentNumber.ToString();
    }

    public void DisplayPrimesEntered() {
        List<int> sortedListOfPrimes = GetSortedListOfPrimes();
        primesDisplay.text = Helper.InsertStringBetweenListItems(sortedListOfPrimes, " กั ");
    }

    public void EndRound() { 
    
    }
}
