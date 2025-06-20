using Menu;
using System;
using VContainer;
using VContainer.Unity;

public class MenuEntryPoint : IStartable, IDisposable
{
    [Inject] private MenuButtons _menuButtons;

    public void Start()
    {
        _menuButtons.Initzialize();
    }

    public void Dispose()
    {
        _menuButtons.Dispose();
    }
}
