using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    public static PanelSwitcher instance;

    private VisibilityModule current;
    [SerializeField] private VisibilityModule mainMenuPanel;
    [SerializeField] private VisibilityModule roundPanel;
    [SerializeField] private VisibilityModule resultPanel;
    [SerializeField] private VisibilityModule roundSettingsPanel;

    private void Awake()
    {
        instance = this;
        current = mainMenuPanel;
    }

    public void SwitchModule(VisibilityModule module)
    {
        if (module != current)
        {
            if (current != null) current.SetInvisibleImmediately();
            current = module;
            module.SetVisibleImmediately();
        }
    }

    public void ShowRoundPanel()
    {
        SwitchModule(roundPanel);
    }

    public void ShowResultPanel()
    {
        SwitchModule(resultPanel);
    }

    public void ShowMainMenuPanel()
    {
        SwitchModule(mainMenuPanel);
    }

    public void ShowRoundSettingsPanel()
    {
        SwitchModule(roundSettingsPanel);
    }
}
