using UnityEngine;
using UnityEngine.UI;

public class ButtonGenerator : MonoBehaviour
{
    //Singleton
    public static ButtonGenerator instance;

    [SerializeField] private GameObject buttonPrefab;

    private void Awake()
    {
        instance = this;
    }

    public void GenerateButtons(int maxPrime)
    {
        foreach (int prime in Helper.GetPrimes(maxPrime)) GenerateButton(prime);
    }

    private void GenerateButton(int prime)
    {
        GameObject buttonObject = Instantiate(buttonPrefab, transform);
        Text buttonText = buttonObject.GetComponentInChildren<Text>();
        buttonText.text = prime.ToString();
        Button button = buttonObject.GetComponent<Button>();
        button.onClick.AddListener(delegate { PrimeInputModule.instance.AddPrime(prime); });
    }
}
