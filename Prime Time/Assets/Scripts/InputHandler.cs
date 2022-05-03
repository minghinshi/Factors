using System;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler instance;

    private PrimeButtonHandler primeButtonHandler;
    private NumberManager numberManager;
    private RoundDisplay roundDisplay;
    private AudioModule audioModule;

    private Stack<int> primesEntered;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        roundDisplay = RoundDisplay.instance;
        audioModule = AudioModule.instance;
    }

    public void Initialize(int maxPrime, NumberManager numberManager)
    {
        SetPrimeInputButtons(maxPrime);
        this.numberManager = numberManager;
        primesEntered = new Stack<int>();
        ShowPrimesEntered();
    }

    private void SetPrimeInputButtons(int maxPrime)
    {
        primeButtonHandler = new PrimeButtonHandler(this);
        primeButtonHandler.DeleteButtons();
        primeButtonHandler.GenerateButtons(maxPrime);
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
            roundDisplay.ShowCannotDelete();
        }
        audioModule.PlayClick();
    }

    public void CheckAnswer()
    {
        if (primesEntered.Count == 0) return;
        numberManager.CheckAnswer(primesEntered);
        ShowPrimesEntered();
    }

    public void ShowPrimesEntered()
    {
        roundDisplay.ShowPrimesSelected(primesEntered);
    }
}
