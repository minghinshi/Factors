using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;
    private RoundManager roundManager;
    private ResultDisplay resultDisplay;

    [SerializeField] private VisibilityModule gameplayVisibilityModule;
    [SerializeField] private VisibilityModule resultsVisibilityModule;

    private bool isPlayingRound = true;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        resultDisplay = ResultDisplay.instance;
    }

    public void StartRound() { 
        
    }

    public void EndRound() {
        if (isPlayingRound)
        {
            isPlayingRound = false;
            gameplayVisibilityModule.FadeOut();
            resultsVisibilityModule.FadeIn();
        }
    }
}
