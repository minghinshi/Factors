using UnityEngine;

public class InputHandler
{
    private PrimeInput primeInput = new PrimeInput();
    private Round round;

    public InputHandler(int maxPrime, Round round)
    {
        this.round = round;
        InitializeButtons(maxPrime);
    }

    private void InitializeButtons(int maxPrime)
    {
        PrimeButton[] primeButtons = new PrimeButtonHandler().GetNewButtons(maxPrime);
        foreach (PrimeButton button in primeButtons)
            button.PrimeButtonClick += OnPrimeButtonClick;

        ActionButton submitButton = new ActionButton(GameObject.Find("SubmitButton"));
        submitButton.GetButton().onClick.AddListener(CheckAnswer);

        ActionButton undoButton = new ActionButton(GameObject.Find("UndoButton"));
        undoButton.GetButton().onClick.AddListener(primeInput.DeletePrime);
    }

    private void OnPrimeButtonClick(object sender, int prime)
    {
        primeInput.AddPrime(prime);
    }

    private void CheckAnswer()
    {
        int[] enteredPrimes = primeInput.GetPrimes();
        if (enteredPrimes.Length == 0) return;
        round.MakeAttempt(enteredPrimes);
        primeInput.ClearPrimes();
    }
}
