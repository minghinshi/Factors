using UnityEngine;
using UnityEngine.UI;

public class RoundSettingsInput : MonoBehaviour
{
    public static RoundSettingsInput instance;

    private int[] listOfPrimes;

    private RoundSettings roundSettings = new RoundSettings();

    [SerializeField] private Text maxPrimeText;
    [SerializeField] private Slider maxPrimeSlider;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        listOfPrimes = Helper.GetPrimes(100).ToArray();
    }

    public void SetMaxPrime()
    {
        int sliderValue = Mathf.RoundToInt(maxPrimeSlider.value);
        int maxPrime = listOfPrimes[sliderValue];
        roundSettings.MaxPrime = maxPrime;
        maxPrimeText.text = maxPrime.ToString();
    }

    public RoundSettings GetRoundSettings()
    {
        return roundSettings.GetClone();
    }
}