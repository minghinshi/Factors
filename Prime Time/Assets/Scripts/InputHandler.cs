using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler
{
    private AudioModule audioModule;
    private ButtonGenerator buttonGenerator;
    private NumberManager numberManager;
    private RoundDisplay roundDisplay;

    private Stack<int> primesEntered = new Stack<int>();

    public InputHandler(int maxPrime, NumberManager numberManager, RoundDisplay roundDisplay) {
        this.numberManager = numberManager;
        this.roundDisplay = roundDisplay;
        InitializeButtons(maxPrime);
    }

    private void InitializeButtons(int maxPrime) {
        GeneratePrimeInputButtoms(maxPrime);
        GameObject.Find("SubmitButton").GetComponent<Button>().onClick.AddListener(CheckAnswer);
        GameObject.Find("UndoButton").GetComponent<Button>().onClick.AddListener(DeletePrime);
    }

    private void GeneratePrimeInputButtoms(int maxPrime) {
        buttonGenerator = new ButtonGenerator(this);
        buttonGenerator.GenerateButtons(maxPrime);
    }

    public void AddPrime(int prime)
    {
        primesEntered.Push(prime);
        ShowPrimesEntered();
        audioModule.PlayClick();
    }

    public void DeletePrime()
    {
        try
        {
            primesEntered.Pop();
            ShowPrimesEntered();
        }
        catch (InvalidOperationException)
        {
            Debug.Log("No primes to delete!");
        }
        audioModule.PlayClick();
    }

    public void CheckAnswer()
    {
        numberManager.CheckAnswer(primesEntered);
        ShowPrimesEntered();
    }

    public void ShowPrimesEntered() {
        roundDisplay.ShowPrimesSelected(primesEntered);
    }
}
