using System;
using UnityEngine;
using UnityEngine.UI;

public class PrimeButton : ActionButton
{
    private int prime;
    private Text primeDisplay;

    public event EventHandler<int> PrimeButtonClick;

    public PrimeButton(int prime, GameObject buttonObject) : base(buttonObject)
    {
        this.prime = prime;

        primeDisplay = buttonObject.GetComponentInChildren<Text>();
        primeDisplay.text = prime.ToString();
    }

    protected override void OnButtonClick()
    {
        base.OnButtonClick();
        PrimeButtonClick?.Invoke(this, prime);
    }
}