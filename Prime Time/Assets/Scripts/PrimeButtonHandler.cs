using UnityEngine;
using UnityEngine.UI;

public class PrimeButtonHandler
{
    private GameObject buttonPrefab = (GameObject)Resources.Load("Prefabs/PrimeButton");
    private Transform buttonContainer;
    private InputHandler inputHandler;

    public PrimeButtonHandler(InputHandler inputHandler)
    {
        this.inputHandler = inputHandler;
        buttonContainer = GameObject.Find("PrimeSelector").transform;
    }

    public void DeleteButtons() {
        foreach (Transform child in buttonContainer) Object.Destroy(child.gameObject);
    }

    public void GenerateButtons(int maxPrime)
    {
        foreach (int prime in Helper.GetPrimes(maxPrime)) GenerateButton(prime);
    }

    private void GenerateButton(int prime)
    {
        GameObject buttonObject = Object.Instantiate(buttonPrefab, buttonContainer);
        Text buttonText = buttonObject.GetComponentInChildren<Text>();
        buttonText.text = prime.ToString();
        Button button = buttonObject.GetComponent<Button>();
        button.onClick.AddListener(delegate { inputHandler.AddPrime(prime); });
    }
}
