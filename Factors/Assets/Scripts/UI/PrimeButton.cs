using System;
using UnityEngine.UI;

public class PrimeButton : ActionButton
{
    private int prime;
    private Text primeDisplay;

    public event EventHandler<int> PrimeButtonClick;

    public void Initialize(int prime) {
        this.prime = prime;

        primeDisplay = GetComponentInChildren<Text>();
        primeDisplay.text = prime.ToString();
    }

    protected override void OnButtonClick()
    {
        base.OnButtonClick();
        PrimeButtonClick?.Invoke(this, prime);
    }
}