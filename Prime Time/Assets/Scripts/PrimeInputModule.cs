using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrimeInputModule : MonoBehaviour
{
    public static PrimeInputModule instance;

    private Stack<int> primesEntered = new Stack<int>();
    private GameplayManager gameplayManager;
    private AudioModule audioModule;
    [SerializeField] private Text primesDisplay;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameplayManager = GameplayManager.instance;
        audioModule = AudioModule.instance;
    }

    public void AddPrime(int prime)
    {
        primesEntered.Push(prime);
        DisplayPrimesEntered();
        audioModule.PlayClick();
    }

    public void DeletePrime()
    {
        try
        {
            primesEntered.Pop();
            DisplayPrimesEntered();
        }
        catch (InvalidOperationException)
        {
            Debug.Log("No primes to delete!");
        }
        audioModule.PlayClick();
    }

    public List<int> GetSortedListOfPrimes()
    {
        List<int> output = new List<int>(primesEntered);
        output.Sort();
        return output;
    }

    public void DisplayPrimesEntered()
    {
        List<int> sortedListOfPrimes = GetSortedListOfPrimes();
        primesDisplay.text = Helper.InsertStringBetweenListItems(sortedListOfPrimes, " กั ");
    }

    public void CheckAnswer()
    {
        gameplayManager.CheckAnswer(primesEntered);
        primesDisplay.text = "";
    }
}
