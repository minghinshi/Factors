using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler
{
    private Stack<int> primesEntered = new Stack<int>();
    private AudioModule audioModule;
    private ButtonGenerator buttonGenerator;
    private NumberManager numberManager;
    [SerializeField] private Text primesDisplay;

    public InputHandler(NumberManager numberManager, int maxPrime) {
        buttonGenerator = new ButtonGenerator(this);
        buttonGenerator.GenerateButtons(maxPrime);
        this.numberManager = numberManager;
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
        numberManager.CheckAnswer(primesEntered);
        primesDisplay.text = "";
    }
}
