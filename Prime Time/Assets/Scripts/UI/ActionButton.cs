using UnityEngine;
using UnityEngine.UI;
using System;

public class ActionButton : MonoBehaviour
{
    private Button button;
    private AudioHandler audioHandler;

    public event EventHandler ActionButtonClick;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        audioHandler = AudioHandler.instance;
    }

    protected virtual void OnButtonClick()
    {
        audioHandler.PlayClick();
        ActionButtonClick?.Invoke(this, EventArgs.Empty);
    }
}
