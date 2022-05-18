using UnityEngine;
using UnityEngine.UI;

public class ActionButton
{
    private Button button;
    private AudioHandler audioHandler = AudioHandler.instance;

    public ActionButton(GameObject buttonObject)
    {
        button = buttonObject.GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    public Button GetButton()
    {
        return button;
    }

    protected virtual void OnButtonClick()
    {
        audioHandler.PlayClick();
    }
}
