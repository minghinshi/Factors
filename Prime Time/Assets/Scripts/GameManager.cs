using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private ResultHandler resultHandler;

    [SerializeField] private VisibilityModule gameplayVisibilityModule;
    [SerializeField] private VisibilityModule resultsVisibilityModule;

    private bool isPlayingRound = true;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        resultHandler = ResultHandler.instance;
    }

    public void EndRound() {
        if (isPlayingRound)
        {
            isPlayingRound = false;
            gameplayVisibilityModule.FadeOut();
            resultsVisibilityModule.FadeIn();
            resultHandler.DisplayResults();
        }
    }
}
