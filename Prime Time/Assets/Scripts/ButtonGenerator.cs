using UnityEngine;
using UnityEngine.UI;

public class ButtonGenerator
{
    [SerializeField] private GameObject buttonPrefab;
    private InputHandler inputHandler;

    public ButtonGenerator(InputHandler inputHandler) {
        this.inputHandler = inputHandler;
    }

    public void GenerateButtons(int maxPrime)
    {
        foreach (int prime in Helper.GetPrimes(maxPrime)) GenerateButton(prime);
    }

    private void GenerateButton(int prime)
    {
        GameObject buttonObject = Object.Instantiate(buttonPrefab, GameObject.Find("PrimeSelector").transform);
        Text buttonText = buttonObject.GetComponentInChildren<Text>();
        buttonText.text = prime.ToString();
        Button button = buttonObject.GetComponent<Button>();
        button.onClick.AddListener(delegate { inputHandler.AddPrime(prime); });
    }
}
