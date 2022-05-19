using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    public event EventHandler TickingEventHandler;

    private Round round;
    private InputHandler inputHandler;
    private PanelSwitcher panelSwitcher;

    private void Start()
    {
        inputHandler = new InputHandler();
        panelSwitcher = PanelSwitcher.instance;
    }

    private void Update()
    {
        TickingEventHandler?.Invoke(this, EventArgs.Empty);
    }

    public void StartRound()
    {
        round = new Round(this);
        inputHandler.SetUpInputFor(round);
        panelSwitcher.ShowRoundPanel();
    }

    public void Retry()
    {
        StartRound();
    }
}
