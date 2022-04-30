using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public event EventHandler TickingEventHandler;

    public static GameHandler instance;
    private RoundManager roundManager;
    private CanvasDisplayer canvasDisplayer;

    private bool isPlayingRound = true;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartRound();
    }

    private void Update()
    {
        TickingEventHandler?.Invoke(this, EventArgs.Empty);
    }

    public void StartRound() {
        roundManager = new RoundManager(TickingEventHandler);
    }
}
