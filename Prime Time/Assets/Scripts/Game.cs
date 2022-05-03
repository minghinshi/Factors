using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    public event EventHandler TickingEventHandler;

    private Round round;
    private PanelSwitcher panelSwitcher;

    private void Start()
    {
        panelSwitcher = PanelSwitcher.instance;
    }

    private void Update()
    {
        TickingEventHandler?.Invoke(this, EventArgs.Empty);
    }

    public void StartRound()
    {
        round = new Round(this);
        round.StartRound();
        panelSwitcher.ShowRoundPanel();
    }

    public void Retry()
    {
        StartRound();
    }
}
