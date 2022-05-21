using UnityEngine;
using UnityEngine.UI;

public class RoundSettingsInput : MonoBehaviour
{
    public static RoundSettingsInput instance;

    private PrimeRange possibleMaxPrimes = new PrimeRange(100);
    private RoundSettings roundSettings = new RoundSettings();

    [SerializeField] private Text maxPrimeText;
    [SerializeField] private Slider maxPrimeSlider;

    private void Awake()
    {
        instance = this;
    }

    public void SetMaxPrime()
    {
        int sliderValue = Mathf.RoundToInt(maxPrimeSlider.value);
        int selectedMaxPrime = possibleMaxPrimes.GetPrimeAt(sliderValue);
        roundSettings.PrimeRange = new PrimeRange(selectedMaxPrime);
        maxPrimeText.text = selectedMaxPrime.ToString();
    }

    public RoundSettings GetRoundSettings()
    {
        return roundSettings.GetClone();
    }
}