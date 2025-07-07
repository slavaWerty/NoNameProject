using Menu;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

public class MenuLifetimeScope : LifetimeScope
{
    [Header("Property")]
    [SerializeField] private Button _play;
    [SerializeField] private Button _olinePlay;
    [SerializeField] private Button _settings;
    [SerializeField] private Button _quit;
    [SerializeField] private Button _help;

    protected override void Configure(IContainerBuilder builder)
    {
        ButtonsRegister(builder);

        builder.RegisterEntryPoint<MenuEntryPoint>();
    }

    private void ButtonsRegister(IContainerBuilder builder)
    {
        var menuButtons = new MenuButtons(_play, _olinePlay, _settings, _quit, _help);
        builder.RegisterInstance(menuButtons);
    }
}
