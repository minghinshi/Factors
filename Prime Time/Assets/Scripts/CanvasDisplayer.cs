public class CanvasDisplayer
{
    private VisibilityModule current;

    public void SwitchModule(VisibilityModule module)
    {
        if (module != current)
        {
            current = module;
            current.FadeOut();
            module.FadeIn();
        }
    }
}
